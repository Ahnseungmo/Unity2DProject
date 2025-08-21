using UnityEngine;
using UnityEngine.UI;

public class MapNodeUI : MonoBehaviour
{
    public int id;
    public int floor;
    public Vector2 position;

    public void Init(int id, int floor, Vector2 pos)
    {
        this.id = id;
        this.floor = floor;
        this.position = pos;
    }
}
