using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Touchscreen.current.primaryTouch.press.isPressed || EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.gameObject.SetActive(false);
            print("dfas");
            SceneManager.LoadScene("MapScene");
        }
    }
}
