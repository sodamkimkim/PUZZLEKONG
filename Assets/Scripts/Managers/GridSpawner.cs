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
        Factor factor = new Factor();
        if (gridGo.AddComponent<Grid>())
        {
            GameObject pzPartPrefab = Resources.Load<GameObject>(Path.PuzzlePartPrefab);
            for (int i = 0; i < GridRowsCols[0, 0]; i++)
            {
                for (int j = 0; j < GridRowsCols[0, 1]; j++)
                { 
                    GameObject pzPartGo = Instantiate(pzPartPrefab, Vector3.zero, Quaternion.identity, gridGo.transform);
                    pzPartGo.transform.localPosition = new Vector3(i+i*0.1f, -(j+j*0.1f), 0f);
                    switch (ThemeManager.ETheme)
                    {
                        case Enum.eTheme.Grey:
                            pzPartGo.GetComponent<SpriteRenderer>().color = factor.Grey2;
                            break;
                        case Enum.eTheme.Green:
                            pzPartGo.GetComponent<SpriteRenderer>().color = factor.Green2;
                            break; 
                        case Enum.eTheme.LightPurple:
                            pzPartGo.GetComponent<SpriteRenderer>().color = factor.LightPurple2;
                            break;
                        case Enum.eTheme.LightBlue:
                            pzPartGo.GetComponent<SpriteRenderer>().color = factor.LightBlue2;
                            break;
                        case Enum.eTheme.Pink:
                            pzPartGo.GetComponent<SpriteRenderer>().color = factor.Pink2;
                            break;
                        case Enum.eTheme.Mint:
                            pzPartGo.GetComponent<SpriteRenderer>().color = factor.Grey2;
                            break;
                    }
                }
            }
        }

        Util util = new Util();
        util.SetPivotToChildCenter(gridGo.transform);
        gridGo.tag = "Grid";
        gridGo.transform.position = factor.PosGridSpawn;
        gridGo.transform.localScale =Factor.ScalePuzzleNormal;
    }
} // end of class
