using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public List<WeaponTemplate> weaponTemplates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isWaitingForInput = false;

    void Start()
    {
        isWaitingForInput = true;
    }

    void Update()
    {
        if (!isWaitingForInput) return;

        bool isTouchPressed = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed;
        bool isPointerOver = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

        if (
            Keyboard.current.anyKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame ||
            isTouchPressed
        // isPointerOver 생략 or false 여야 함
        )
        {
            isWaitingForInput = false;

            // UI 이벤트 방지
            if (EventSystem.current != null)
                EventSystem.current.gameObject.SetActive(false);

            print("MapScene 이동");

            GameObject obj = new GameObject();
            obj.name = "Inventory";
            Inventory inventroy = obj.AddComponent<Inventory>();
            inventroy.weaponTemplates = weaponTemplates;

            DataManager.Get.player.Init("Player", 100, 5);
            DataManager.Get.stagePos = new Vector2Int(0, -1);

            SceneManager.LoadScene("MapScene");
        }
    }

}
