using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public Grid grid;                // Grid 컴포넌트 참조
    public GameObject prefabToSpawn; // 생성할 오브젝트 프리팹

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭한 위치를 월드 좌표로 변환
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // 2D 기준으로 Z는 0으로 고정

            // Grid 셀 좌표로 변환
            Vector3Int cellPos = grid.WorldToCell(mouseWorldPos);
            print(cellPos);

            // 셀의 중앙 좌표 계산
            Vector3 spawnPos = grid.GetCellCenterWorld(cellPos);
            print(spawnPos);

            // 오브젝트 생성
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
