using System.Collections;
using TMPro;
using UnityEngine;

public class DamageFont : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    int damage;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();

        textMeshPro.text = damage.ToString();

        Color color;
        if (damage < 0) color = Color.green;
        else if (damage < 20) color = Color.yellow;
        else if (damage < 40) color = Color.magenta;
        else color = Color.red;
        textMeshPro.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void PrintDamage(int damage)
    {

        this.damage = damage;
        //        PrintScreen();
        StartCoroutine(PrintScreen());
    

    }
    IEnumerator PrintScreen()
    {     
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
    
}
