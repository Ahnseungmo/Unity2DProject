using Unity.VisualScripting;
using UnityEngine;

public class MapCharacter : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    public Animator animator;
    bool moving = false;
    float moveTime = 0;
    Vector3 movePos;
    Vector3 startPos;
    NodePrefab targetNode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        var node = MapGenerator.Get.AllNodes.Find(s => s.Pos == MapGenerator.Get.StartPos);
        print(node);
        print(MapGenerator.Get.StartPos);
        Vector2 pos = MapGenerator.Get.StartPos;
        transform.position = new Vector3(pos.x,pos.y,0);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Move();
        }
    }

    public void SetPos(Vector2Int pos)
    {
        var node = MapGenerator.Get.AllNodes.Find(s => s.Pos == pos);
        transform.position = node.mapNodeComponent.transform.position;
        /*
        foreach (var child in node.Children)
        {
            child.mapNodeComponent.gameObject.GetComponent<NodePrefab>().clickAble = true;
        }
        */
    }
    public void MoveToPosition(NodePrefab node) {
        targetNode = node;
        animator.SetBool("1_Move",true);
        moving = true;
        movePos = node.gameObject.transform.position;
        startPos = transform.position;
    }
    private void Move()
    {
      
        Vector2 pos = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * speed);
        Vector3 nScale = transform.localScale;
        if (pos.x > transform.position.x) nScale.x = -1;
        else nScale.x = 1;
        transform.localScale = nScale;
        transform.position = pos;
        print(Vector2.Distance(transform.position, movePos));
        if (Vector2.Distance(transform.position,movePos) < 0.1f)
        {
            moving = false;
            animator.SetBool("1_Move", false);
            SetPos(new Vector2Int((int)movePos.x,(int)movePos.y));
            print(DataManager.Get.stagePos);


            targetNode.NodeStart();
        }
    }

}
