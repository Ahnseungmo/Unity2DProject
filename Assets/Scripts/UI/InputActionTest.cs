using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputActionTest : MonoBehaviour
{
    public InputSystemUIInputModule action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action = GetComponent<InputSystemUIInputModule>();
    }

    // Update is called once per frame
    void Update()
    {
        if (action.leftClick)
        {
            print("Å¬¸¯");
            print(action.point);
        }


    }

    private void MouseClick()
    {

    }
}
