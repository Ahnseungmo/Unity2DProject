using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public Grid grid;                // Grid ������Ʈ ����
    public GameObject prefabToSpawn; // ������ ������Ʈ ������

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // 2D �������� Z�� 0���� ����

            // Grid �� ��ǥ�� ��ȯ
            Vector3Int cellPos = grid.WorldToCell(mouseWorldPos);
            print(cellPos);

            // ���� �߾� ��ǥ ���
            Vector3 spawnPos = grid.GetCellCenterWorld(cellPos);
            print(spawnPos);

            // ������Ʈ ����
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
