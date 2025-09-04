using Unity.VisualScripting;
using UnityEngine;

public class PosTest : MonoBehaviour
{

    Rigidbody2D rg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rg.bodyType = RigidbodyType2D.Dynamic;
            float speed = 1000;
            gameObject.transform.parent = null;
            rg.AddTorque(speed);
            rg.angularDamping = 0;

        }
        print(rg.angularVelocity);
    }
}
