using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SlingshotController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    private LineRenderer lineRenderer;
    private Vector3 dragStartPos;
    private bool isDragging = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStartPos.z = 0;
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0;
            lineRenderer.SetPosition(0, launchPoint.position);
            lineRenderer.SetPosition(1, currentPos);
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragEndPos.z = 0;
            Vector3 force = (dragStartPos - dragEndPos) * 5f;
            ShootProjectile(force);
            isDragging = false;
            lineRenderer.positionCount = 0;
        }
    }

    void ShootProjectile(Vector3 force)
    {
        GameObject proj = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}