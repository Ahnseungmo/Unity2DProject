using UnityEngine;

public class NodeAnimation : MonoBehaviour
{
    private NodePrefab nodePrefab;

    private Vector3 originalScale;
    public float scaleSpeed = 0.3f;     // 스케일 변화 속도
    public float scaleAmount = 0.1f;  // 얼마나 커졌다 작아질지
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        nodePrefab = GetComponent<NodePrefab>();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (nodePrefab.clickAble)
        {
            float scaleOffset = Mathf.PingPong(Time.time * scaleSpeed, scaleAmount);
            transform.localScale = originalScale + new Vector3(scaleOffset, scaleOffset, 0);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
