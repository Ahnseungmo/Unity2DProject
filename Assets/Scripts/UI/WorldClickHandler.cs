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
            Debug.Log("Ŭ���� ������Ʈ: " + hit.collider.name);
            // ���⼭ ��� ���� ���� ȣ��
        }
    }
}
