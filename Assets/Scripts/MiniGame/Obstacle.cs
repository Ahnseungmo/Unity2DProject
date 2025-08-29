using UnityEngine;

public class Obstacle : MonoBehaviour
{
    ObjectDestruction ObjectDestruction;
    SpriteRenderer Sr;
    int MaxDurability = 1;
    int durability = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectDestruction = GetComponent<ObjectDestruction>();
        Sr = GetComponent<SpriteRenderer>();
        //        Sr.material.GetFloat("value");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(durability == MaxDurability)
            ObjectDestruction.Break();
        else
        {
            durability++;
            Sr.material.SetFloat("_value", (float)durability / (float)MaxDurability);
        }
    }
}
