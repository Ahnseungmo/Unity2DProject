using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Character character;

    public Image HpBarImage;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
//        HpBar = gameObject.GetComponentInChildren<Image>();
        SetUp(character);
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            if(character.gameObject.activeSelf == false) gameObject.SetActive(false);

            transform.position = character.transform.position + offset;
            HpBarImage.fillAmount = (float)character.currentHp / (float)character.maxHp;
        }
    }

    private void SetUp(Character character)
    {
        this.character = character;
//        gameObject.transform.position = new Vector3(-9.6f, 75, 0);
    }
}
