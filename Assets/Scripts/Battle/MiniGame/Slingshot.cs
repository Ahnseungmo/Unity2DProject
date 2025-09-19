using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Slingshot은 EventSystem UI Input Module이 전달하는 포인터 이벤트로 동작합니다.
/// BattleManager에서 Prepare(WeaponTemplate)을 호출하면 해당 무기로 발사를 준비합니다.
/// </summary>
public class Slingshot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform throwPoint;                 // world-space 시작위치 (씬에서 빈 오브젝트로 배치)
    public float maxDragDistance = 2.5f;
    public float shootPower = 6f;
    public LineRenderer lineRendererPrefab;      // (선택) 궤적선용 프리팹

    private WeaponTemplate preparedWeapon;
    private GameObject previewProjectile;
    private LineRenderer previewLine;
    private Vector2 dragStartScreen;
    private bool ready = false;
    private bool isDragging = false;

    public void Prepare(WeaponTemplate weapon)
    {
        preparedWeapon = weapon;
        ready = weapon != null;

        // preview 생성 (선택)
        if (previewProjectile != null) Destroy(previewProjectile);
        if (preparedWeapon != null && preparedWeapon.projectilePrefab != null)
        {
            previewProjectile = Instantiate(preparedWeapon.projectilePrefab, throwPoint.position, Quaternion.identity);
            Destroy(previewProjectile.GetComponent<Projectile>()); 
            var rb = previewProjectile.GetComponent<Rigidbody2D>();
            previewProjectile.GetComponent<Collider2D>().enabled = false;
            if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;
            // LineRenderer
            if (lineRendererPrefab != null)
            {
                previewLine = Instantiate(lineRendererPrefab);
                previewLine.positionCount = 2;
                previewLine.enabled = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("DragDown");
        if (!ready) return;
        isDragging = true;
        dragStartScreen = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {/*
        if (!isDragging || previewProjectile == null) return;

        Vector2 mouseScreen = eventData.position;
        Vector2 dragVec = (dragStartScreen - mouseScreen);
        if (dragVec.magnitude > maxDragDistance)
            dragVec = dragVec.normalized * maxDragDistance;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(dragStartScreen - dragVec);
        worldPos.z = 0;
        previewProjectile.transform.position = worldPos;

        Vector2 dir = (throwPoint.position - worldPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        previewProjectile.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

        if (previewLine != null)
        {
            previewLine.enabled = true;
            previewLine.SetPosition(0, throwPoint.position);
            previewLine.SetPosition(1, worldPos);
        }
        */
        if (!isDragging || previewProjectile == null) return;

        Vector2 mouseScreen = eventData.position;
        Vector2 dragVec = (dragStartScreen - mouseScreen);

        if (dragVec.magnitude > maxDragDistance)
            dragVec = dragVec.normalized * maxDragDistance;

        // 위치 계산: throwPoint에서 드래그 방향으로 이동
        Vector3 worldDrag = Camera.main.ScreenToWorldPoint(dragVec);
        Vector3 worldPos = throwPoint.position + (Vector3)(-dragVec.normalized * dragVec.magnitude * 0.01f); // 0.01은 화면→월드 변환 감도 조절
        worldPos.z = 0;

        previewProjectile.transform.position = throwPoint.position; // 위치는 항상 throwPoint로 고정
                                                                    // or 원하면 dragVec 만큼 이동시키기

        // 회전 계산: 드래그 방향의 반대 방향을 바라보게
        Vector2 aimDir = dragVec.normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        previewProjectile.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 90도는 프리팹 방향 보정

        // 라인 렌더러 업데이트
        if (previewLine != null)
        {
            previewLine.enabled = true;
            previewLine.SetPosition(0, throwPoint.position);
            previewLine.SetPosition(1, throwPoint.position - (Vector3)dragVec.normalized * Mathf.Min(dragVec.magnitude, maxDragDistance) * 0.01f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        /*
        print("DragUp");

        if (!isDragging || preparedWeapon == null) return;
        isDragging = false;
        ready = false;

        Vector2 mouseScreen = eventData.position;
        Vector2 dragVec = (dragStartScreen - mouseScreen);
        if (dragVec.magnitude > maxDragDistance) dragVec = dragVec.normalized * maxDragDistance;

        Vector2 launchDir = dragVec.normalized;
        float power = dragVec.magnitude * shootPower;

        // spawn actual projectile
        var projObj = Instantiate(preparedWeapon.projectilePrefab, throwPoint.position, Quaternion.identity);
        projObj.transform.rotation = previewProjectile.transform.rotation;
        var proj = projObj.GetComponent<Projectile>();
        proj.Init(preparedWeapon, power, launchDir);

        // 플레이어 A화면 던지기 애니메이션 트리거
        BattleManager.Instance.OnPlayerFired(preparedWeapon);

        // preview cleanup
        if (previewProjectile != null) Destroy(previewProjectile);
        if (previewLine != null) Destroy(previewLine.gameObject);

        // 준비 무기 무효화
        preparedWeapon = null;
        */
        if (!isDragging || preparedWeapon == null) return;
        isDragging = false;
        ready = false;

        // 월드 좌표로 드래그 계산
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(dragStartScreen);
        Vector3 worldEnd = Camera.main.ScreenToWorldPoint(eventData.position);
        worldStart.z = worldEnd.z = 0;

        Vector2 dragVec = worldStart - worldEnd;

        // 거리 제한
        if (dragVec.magnitude > maxDragDistance)
            dragVec = dragVec.normalized * maxDragDistance;

        Vector2 launchDir = dragVec.normalized;
        float power = dragVec.magnitude * shootPower;

        // 발사체 생성
        var projObj = Instantiate(preparedWeapon.projectilePrefab, throwPoint.position, Quaternion.identity);
        projObj.transform.rotation = previewProjectile.transform.rotation;
        var proj = projObj.GetComponent<Projectile>();
        proj.Init(preparedWeapon, power, launchDir);

        BattleManager.Instance.OnPlayerFired(preparedWeapon);

        // 클린업
        if (previewProjectile != null) Destroy(previewProjectile);
        if (previewLine != null) Destroy(previewLine.gameObject);
        preparedWeapon = null;
    }
}
