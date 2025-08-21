using UnityEngine;
using UnityEngine.UI;

public class UILineConnector : MonoBehaviour
{
    public Image lineImage;

    public void Connect(RectTransform from, RectTransform to)
    {
        Vector2 start = from.anchoredPosition;
        Vector2 end = to.anchoredPosition;

        Vector2 dir = end - start;
        float distance = dir.magnitude;

        RectTransform rt = lineImage.rectTransform;
        rt.sizeDelta = new Vector2(distance, rt.sizeDelta.y);
        rt.anchoredPosition = (start + end) / 2;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rt.rotation = Quaternion.Euler(0, 0, angle);
    }
}
