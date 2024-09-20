using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    // 그리드 크기
    public int[,] GridRowsCols = { { 9, 9 } };
    public GameObject[,] GridSpriteArr = new GameObject[9, 9];
    private void Start()
    {
        // grid 생성함수 호출
        SpawnGrid();
    }
    public void SpawnGrid()
    {
        GameObject gridGo = new GameObject("Grid");

        if (gridGo.AddComponent<Grid>())
        {
            GameObject pzPartPrefab = Resources.Load<GameObject>(Path.PuzzlePartPrefab);
            for (int i = 0; i < GridRowsCols[0, 0]; i++)
            {
                for (int j = 0; j < GridRowsCols[0, 1]; j++)
                {
                    // TODO - Sprite 생성
                    GameObject pzPartGo = Instantiate(pzPartPrefab, Vector3.zero, Quaternion.identity, gridGo.transform);
                    pzPartGo.transform.localPosition = new Vector3(i+i*0.1f, -(j+j*0.1f), 0f); 
                  
                    
                }
            }
        }

        Util util = new Util();
        util.SetPivotToChildCenter(gridGo.transform);
        gridGo.tag = "Grid";
        gridGo.transform.position = new Vector3(0f, 1f, 2f);
  
        gridGo.transform.localScale =Factor.PZNormal; 
    }
} // end of class
