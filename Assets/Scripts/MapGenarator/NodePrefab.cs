using UnityEngine;
using UnityEngine.UIElements;

public class NodePrefab : MonoBehaviour
{
    private MapNodeComponent nodeComponent;

    public bool clickAble = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodeComponent = GetComponent<MapNodeComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClickAbleChirdren()
    {
        foreach(var node in nodeComponent.Children)
        {
//            node.Children.gameobject
        }
    }
}
