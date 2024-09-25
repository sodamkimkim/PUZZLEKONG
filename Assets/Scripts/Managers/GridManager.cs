using System.Collections.Generic;
using System.Collections;
using UnityEngine;
[DefaultExecutionOrder(-9)]
public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Transform _gridParentTr = null;

    #region GridGo관리
    private Grid _grid;
    public Grid Grid { get => _grid; private set => _grid = value; }
    private static bool _isGridReady = false;
    public static bool IsGridGoReady
    {
        get => _isGridReady;
        private set
        {
            _isGridReady = value;
        }
    }
    #endregion

    #region Grid 배열, part관리
    public Dictionary<string, SpriteRenderer> SprDic = new Dictionary<string, SpriteRenderer>();
    #endregion

    #region Grid Color
    public Color Color_Nothing { get; private set; }
    public Color Color_Placable { get; private set; }
    public Color Color_PZIn { get; private set; }
    #endregion

    public delegate void CheckPlacable();
    private CheckPlacable _checkPlacableCallback { get; set; }

    public void Iniit(CheckPlacable checkPlacableCallback)
    {
        _checkPlacableCallback = checkPlacableCallback;
    }

    private void Start()
    {
        LazyStart();
    }
    private void LazyStart()
    {
        InitializeGridColor();
        GameObject pzPartPrefab = Resources.Load<GameObject>(Path.PuzzlePartPrefab);
        IsGridGoReady = SpawnGridGo(GridArrayRepository.GridArrArr[1], pzPartPrefab);
        //    Debug.Log(SprDic.Count);
    }
    private void InitializeGridColor()
    {
        switch (ThemeManager.ETheme)
        {
            case Enum.eTheme.Grey:
                Color_Nothing = Factor.Grey2;
                Color_Placable = Factor.Grey3;
                Color_PZIn = Factor.Grey4;
                break;
            case Enum.eTheme.Green:
                Color_Nothing = Factor.Green2;
                Color_Placable = Factor.Green3;
                Color_PZIn = Factor.Green4;
                break;
            case Enum.eTheme.LightPurple:
                Color_Nothing = Factor.LightPurple2;
                Color_Placable = Factor.LightPurple3;
                Color_PZIn = Factor.LightPurple4;
                break;
            case Enum.eTheme.LightBlue:
                Color_Nothing = Factor.LightBlue2;
                Color_Placable = Factor.LightBlue3;
                Color_PZIn = Factor.LightBlue4;
                break;
            case Enum.eTheme.Pink:
                Color_Nothing = Factor.Pink2;
                Color_Placable = Factor.Pink3;
                Color_PZIn = Factor.Pink4;
                break;
            case Enum.eTheme.Mint:
                Color_Nothing = Factor.BGColorDefault;
                Color_Placable = Factor.Grey3;
                Color_PZIn = Factor.Grey4;
                break;
        }
    }

    public bool SpawnGridGo(int[,] gridArr, GameObject pzPartPrefab)
    {
        int rowCnt = gridArr.GetLength(0);
        int colCnt = gridArr.GetLength(1);
      GameObject  gridGo = new GameObject("Grid");
        gridGo.transform.parent = _gridParentTr;

        if (gridGo.AddComponent<Grid>())
        {
            Grid grid = gridGo.GetComponent<Grid>();
            grid.Data = gridArr;
            Grid = grid;

       
            for (int i = 0; i < rowCnt; i++)
            {
                for (int j = 0; j < colCnt; j++)
                {
                    GameObject gridPartGo = Instantiate(pzPartPrefab, Vector3.zero, Quaternion.identity, gridGo.transform);
                    gridPartGo.name = $"GridPart_{i}_{j}";
                    gridPartGo.transform.localPosition = new Vector3(j + j * 0.1f, -(i + i * 0.1f), 0f);
                    gridPartGo.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);
                    SpriteRenderer spr = gridPartGo.GetComponent<SpriteRenderer>();
                    Util.AddDictionary(SprDic, gridPartGo.name, spr);
                    SetGridColor(spr, gridArr[i, j]);
                    GridPart gridPart = gridPartGo.AddComponent<GridPart>();
                    gridPart.HasPuzzle = gridArr[i, j] == 1 ? true : false;
                    gridPart.IdxRow = i;
                    gridPart.IdxCol = j;
                }
            }
        }

        Util.SetPivotToChildCenter(gridGo.transform);
        gridGo.tag = "Grid";
        gridGo.transform.position = Factor.PosGridSpawn;
        gridGo.transform.localScale = Factor.ScalePuzzleNormal;

        // BoxCollider2D
        BoxCollider2D collider =  gridGo.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(rowCnt * 1.1f, colCnt * 1.1f);
        return true;
    }

    /// <summary>
    /// gridPartValue 0 || 1 값에 따라 color 변경
    /// Theme 따라 0 => 각 color2값, 1 => 각 color4값
    /// Placable => color3값
    /// </summary>
    /// <param name="spr"></param>
    /// <param name="gridPartValue"></param>
    private void SetGridColor(SpriteRenderer spr, int gridPartValue)
    {
        spr.color = gridPartValue == 1 ? Color_PZIn : Color_Nothing;
    }
} // end of class
