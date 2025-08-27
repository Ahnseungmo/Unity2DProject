using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dragStartPos;
    private bool isDragging = false;

    [SerializeField] private float power = 10f;
    [SerializeField] private float maxDrag = 2f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

    }
    private void OnMouseDown()
    {

        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        isDragging = true;
        rb.linearVelocity = Vector2.zero;

    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dragVector = currentPos - dragStartPos;

        dragVector = Vector2.ClampMagnitude(dragVector, maxDrag);

        Vector2 newPos = dragStartPos + dragVector;
        transform.position = newPos;
        Vector2 launchDir = dragStartPos - newPos;

        float angle = Mathf.Atan2(launchDir.y, launchDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + -90);
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;
        rb.bodyType = RigidbodyType2D.Dynamic;


        Vector2 launchDir = dragStartPos - (Vector2)transform.position;

        rb.AddForce(launchDir * power, ForceMode2D.Impulse);
      
    }
}
