using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChracterPickUpTest : MonoBehaviour, IPointerClickHandler
{
    public int maxhp = 100;
    public int hp = 100;

    public GameObject LHandObject;//보조 장비
    public GameObject RHandObject;//주 무기

    private GameObject LHand;
    private GameObject RHand;
    private GameObject instantiatedLHand;
    private GameObject instantiatedRHand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        LHand = FindChildByName(gameObject.transform, "L_Weapon").gameObject;
        RHand = FindChildByName(gameObject.transform, "R_Weapon").gameObject;

        if (LHandObject != null)
        {
            instantiatedLHand = Instantiate(LHandObject, LHand.transform);
            /*
            var rb = instantiatedLHand.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            */
            Rigidbody2D rb = instantiatedLHand.GetComponent<Rigidbody2D>();
            rb.simulated = false;
            instantiatedLHand.GetComponent<SpriteRenderer>().sortingOrder = LHand.GetComponent<SpriteRenderer>().sortingOrder;
            instantiatedLHand.layer = gameObject.layer;
        }

        if (RHandObject != null)
        {
            instantiatedRHand = Instantiate(RHandObject, RHand.transform);
            var rb = instantiatedRHand.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = false;
            instantiatedRHand.GetComponent<SpriteRenderer>().sortingOrder = RHand.GetComponent<SpriteRenderer>().sortingOrder;
            instantiatedRHand.layer = gameObject.layer;
        }

    }

    // Update is called once per frame
    void Update()
    { 
    }

    Transform FindChildByName(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            // 재귀적으로 자식의 자식도 검사
            Transform found = FindChildByName(child, childName);
            if (found != null)
                return found;
        }

        // 못 찾았을 경우
        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("드랍");
        if (instantiatedLHand != null)
        {
            instantiatedLHand.transform.parent = null;
            instantiatedLHand.GetComponent<SpriteRenderer>().sortingOrder = 0;

        }


        if (instantiatedRHand != null)
        {
            instantiatedRHand.transform.parent = null;
            instantiatedRHand.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

    }
}
