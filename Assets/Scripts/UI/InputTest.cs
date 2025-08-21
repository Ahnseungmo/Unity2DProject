using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTest : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public int index = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Ŭ����" + index);
    }
    public void OnDrag(PointerEventData eventData)
    {
        print("�巡��" + index);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        print("�巡�� ��" + index);
    }
}
