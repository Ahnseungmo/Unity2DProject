using UnityEngine;

public class BattleSceneInitalizer : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        GameObject playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
//        BattleManager.Instance.player = player;
    }
}
