using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("References")]
    public Inventory inventory;
    public Slingshot slingshot;         // Scene에서 연결
    public Transform throwPoint;        // B 화면 던지는 기준(슬링샷에도 연결 가능)

    [Header("Prefabs & Slots")]
    public GameObject playerPrefab;
    public Transform playerSlot;
    public GameObject monsterPrefabA;
    public GameObject monsterPrefabB;
    public List<Transform> spawnSlotsA; // 0..3
    public List<Transform> spawnSlotsB; // 0..3

    [Header("Runtime")]
    public Player player;
    public List<Monster> monstersA = new List<Monster>();
    public List<Monster> monstersB = new List<Monster>();

    // 현재 턴의 준비된 무기 (ReadyNextShot에서 세팅)
    private WeaponTemplate currentWeapon;

    // 샷이 끝나기 전 대기 플래그
    private bool waitingForNextShot = false;

    void Awake()
    {
        Instance = this;
    }

    public void StartBattle(StageData stageData)
    {
        monstersA.Clear();
        monstersB.Clear();

        SpawnPlayer();
        SpawnMonsters(stageData);

        StartPlayerTurn();
    }

    void SpawnPlayer()
    {
        if (player != null)
        {
            player.transform.SetParent(playerSlot, false);
            player.transform.localPosition = Vector3.zero;
            return;
        }

        if (playerPrefab == null || playerSlot == null)
        {
            Debug.LogError("playerPrefab or playerSlot not assigned");
            return;
        }

        GameObject pgo = Instantiate(playerPrefab, playerSlot.position, Quaternion.identity, playerSlot);
        player = pgo.GetComponent<Player>();
        if (player == null) Debug.LogError("playerPrefab has no Player component");
        player.Init("Player", 100, 5);
    }

    void SpawnMonsters(StageData data)
    {
        int count = Mathf.Min(data.monsters.Count, 4);

        for (int i = 0; i < count; i++)
        {
            var mData = data.monsters[i];
            Monster mA = SpawnInA(mData, i);
            Monster mB = SpawnInB(mData, i);

            if (mA != null && mB != null)
            {
                mA.linkedMonster = mB;
                mB.linkedMonster = mA;

                monstersA.Add(mA);
                monstersB.Add(mB);
            }
        }
    }

    Monster SpawnInA(MonsterData data, int index)
    {
        if (monsterPrefabA == null) { Debug.LogError("monsterPrefabA missing"); return null; }
        if (index >= spawnSlotsA.Count || spawnSlotsA[index] == null) { Debug.LogError($"spawnSlotsA[{index}] missing"); return null; }

        GameObject go = Instantiate(monsterPrefabA, spawnSlotsA[index].position, Quaternion.identity, spawnSlotsA[index]);
        Monster m = go.GetComponent<Monster>();
        if (m == null) Debug.LogError("monsterPrefabA missing Monster script");
        else m.Init(data.monsterId, data.hp, data.attack);
        return m;
    }

    Monster SpawnInB(MonsterData data, int index)
    {
        if (monsterPrefabB == null) { Debug.LogError("monsterPrefabB missing"); return null; }
        if (index >= spawnSlotsB.Count || spawnSlotsB[index] == null) { Debug.LogError($"spawnSlotsB[{index}] missing"); return null; }

        GameObject go = Instantiate(monsterPrefabB, spawnSlotsB[index].position, Quaternion.identity, spawnSlotsB[index]);
//        go.GetComponent<Rigidbody2D>().simulated = false;
        go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            
        ChangeLayersRecursively(go.transform, spawnSlotsB[index].gameObject.layer);

        Monster m = go.GetComponent<Monster>();
        if (m == null) Debug.LogError("monsterPrefabB missing Monster script");
        else m.Init(data.monsterId, data.hp, data.attack);
        return m;
    }

    // 플레이어 턴 시작: 인벤토리 리필 -> 첫 무기 준비
    public void StartPlayerTurn()
    {
        inventory.RefillWeapons();
        ReadyNextShot();
    }

    // 준비: 인벤토리에서 다음 무기 꺼내서 슬링샷에 전달 (발사는 슬링샷에서)
    public void ReadyNextShot()
    {
        if (waitingForNextShot) return; // 이미 대기중이면 중복 방지

        if (!inventory.HasWeapons())
        {
            EndPlayerTurn();
            return;
        }

        currentWeapon = inventory.GetNextWeapon();
        // slingshot은 Scene에서 연결되어야 함
        slingshot.Prepare(currentWeapon);
    }

    // Slingshot이 실제 발사했을 때 호출
    public void OnPlayerFired(WeaponTemplate weapon)
    {
        // 플레이어 A화면 던지기 애니메이션 재생
        player.PlayThrowAnimation();

        // (현재무기는 이미 GetNextWeapon으로 큐에서 꺼내져 있음)
        // 이후 샷이 정지되었을 때 BattleManager.OnProjectileStopped()가 호출되어
        // 1초 뒤 ReadyNextShot이 호출된다.
    }

    // 발사한 투사체가 멈추거나 충돌로 끝났을 때 Projectile에서 호출
    public void OnProjectileStopped()
    {
        if (waitingForNextShot) return;
        StartCoroutine(WaitAndReadyNext());
    }

    IEnumerator WaitAndReadyNext()
    {
        waitingForNextShot = true;
        yield return new WaitForSeconds(1f);
        waitingForNextShot = false;
        ReadyNextShot();
    }

    // 모든 무기 소모 -> 몬스터 턴으로 전환
    public void EndPlayerTurn()
    {
        StartCoroutine(MonstersAttackPhase());
    }

    IEnumerator MonstersAttackPhase()
    {
        foreach (var m in monstersA)
        {
            if (m != null && m.IsAlive)
            {
                m.DoAttack(player);
                yield return new WaitForSeconds(0.8f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartPlayerTurn();
    }

    private void ChangeLayersRecursively(Transform trans, string layerName)
    {
        // 현재 오브젝트의 레이어를 변경
        trans.gameObject.layer = LayerMask.NameToLayer(layerName);

        // 모든 자식 오브젝트에 대해 재귀적으로 함수 호출
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, layerName);
        }
    }
    private void ChangeLayersRecursively(Transform trans, int layer)
    {
        // 현재 오브젝트의 레이어를 변경
        trans.gameObject.layer = layer;

        // 모든 자식 오브젝트에 대해 재귀적으로 함수 호출
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, layer);
        }
    }
}
