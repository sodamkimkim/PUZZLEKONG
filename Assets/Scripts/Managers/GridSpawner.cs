using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _gridParentTr = null;
    public int[,] GridRowsCols = { { 9, 9 } };
    public GameObject[,] GridSpriteArr = new GameObject[9, 9];
    private static bool _isGridReady = false;
    public static bool IsGridReady {
        get =>  _isGridReady;  
        private set {
            _isGridReady = value;
        } }
    private void Start()
    {
        // grid 생성함수 호출
        IsGridReady =  SpawnGrid();
    }
    public bool SpawnGrid()
    {
        GameObject gridGo = new GameObject("Grid");
        gridGo.transform.parent = _gridParentTr;

        if (gridGo.AddComponent<Grid>())
        {
            GameObject pzPartPrefab = Resources.Load<GameObject>(new Path().PuzzlePartPrefab);
            for (int i = 0; i < GridRowsCols[0, 0]; i++)
            {
                for (int j = 0; j < GridRowsCols[0, 1]; j++)
                { 
                    GameObject pzPartGo = Instantiate(pzPartPrefab, Vector3.zero, Quaternion.identity, gridGo.transform);
                    pzPartGo.transform.localPosition = new Vector3(i+i*0.1f, -(j+j*0.1f), 0f);
                    switch (ThemeManager.ETheme)
                    {
                        case Enum.eTheme.Grey:
                            pzPartGo.GetComponent<SpriteRenderer>().color = Factor.Grey2;
                            break;
                        case Enum.eTheme.Green:
                            pzPartGo.GetComponent<SpriteRenderer>().color = Factor.Green2;
                            break; 
                        case Enum.eTheme.LightPurple:
                            pzPartGo.GetComponent<SpriteRenderer>().color = Factor.LightPurple2;
                            break;
                        case Enum.eTheme.LightBlue:
                            pzPartGo.GetComponent<SpriteRenderer>().color = Factor.LightBlue2;
                            break;
                        case Enum.eTheme.Pink:
                            pzPartGo.GetComponent<SpriteRenderer>().color = Factor.Pink2;
                            break;
                        case Enum.eTheme.Mint:
                            pzPartGo.GetComponent<SpriteRenderer>().color = Factor.Grey2;
                            break;
                    }
                }
            }
        }

        Util util = new Util();
        util.SetPivotToChildCenter(gridGo.transform);
        gridGo.tag = "Grid";
        gridGo.transform.position = Factor.PosGridSpawn;
        gridGo.transform.localScale =Factor.ScalePuzzleNormal;
        return true;
    }
} // end of class
