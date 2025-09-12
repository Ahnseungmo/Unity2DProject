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
            var rb = previewProjectile.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = true;
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
        if (!ready) return;
        isDragging = true;
        dragStartScreen = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || previewProjectile == null) return;

        Vector2 mouseScreen = eventData.position;
        Vector2 dragVec = (dragStartScreen - mouseScreen);
        if (dragVec.magnitude > maxDragDistance) dragVec = dragVec.normalized * maxDragDistance;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(dragStartScreen - dragVec);
        worldPos.z = 0;
        previewProjectile.transform.position = worldPos;

        if (previewLine != null)
        {
            previewLine.enabled = true;
            previewLine.SetPosition(0, throwPoint.position);
            previewLine.SetPosition(1, worldPos);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
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
        var proj = projObj.GetComponent<Projectile>();
        proj.Init(preparedWeapon, power, launchDir);

        // 플레이어 A화면 던지기 애니메이션 트리거
        BattleManager.Instance.OnPlayerFired(preparedWeapon);

        // preview cleanup
        if (previewProjectile != null) Destroy(previewProjectile);
        if (previewLine != null) Destroy(previewLine.gameObject);

        // 준비 무기 무효화
        preparedWeapon = null;
    }
}
