using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public List<WeaponTemplate> weaponTemplates;
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
            print("MapScene ¿Ãµø");
            GameObject obj = new GameObject();
            obj.name = "Inventory";
            Inventory inventroy = obj.AddComponent<Inventory>();
            inventroy.weaponTemplates = weaponTemplates;

            DataManager.Get.player.Init("Player", 100, 5);
            DataManager.Get.stagePos = new Vector2Int(0,-1);

            SceneManager.LoadScene("MapScene");
        }
    }
}
