using UnityEngine;

public class Obstacle : MonoBehaviour
{
    ObjectDestruction ObjectDestruction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectDestruction = GetComponent<ObjectDestruction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ObjectDestruction.Break();
    }
}
