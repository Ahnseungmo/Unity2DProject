using UnityEngine;
using UnityEngine.InputSystem;

public class WorldClickHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputAction clickAction;
    [SerializeField] private InputAction pointerPositionAction;

    private void OnEnable()
    {
        clickAction.Enable();
        pointerPositionAction.Enable();
        clickAction.performed += OnClick;
    }

    private void OnDisable()
    {
        clickAction.performed -= OnClick;
        clickAction.Disable();
        pointerPositionAction.Disable();
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = pointerPositionAction.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("클릭한 오브젝트: " + hit.collider.name);
            // 여기서 노드 선택 로직 호출
        }
    }
}
