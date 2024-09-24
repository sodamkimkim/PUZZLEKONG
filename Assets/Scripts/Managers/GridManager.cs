using System.Collections.Generic;
using System.Collections;
using UnityEngine;
[DefaultExecutionOrder(-9)]
public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Transform _gridParentTr = null;

    #region GridGo관리
    private GameObject _gridGo { get; set; }
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
    public int[,] NowGridArr = null;
    public Dictionary<string, SpriteRenderer> SprDic = new Dictionary<string, SpriteRenderer>();
    #endregion

    #region Grid Color
    public Color Color_Nothing { get; private set; }
    public Color Color_Placable { get; private set; }
    public Color Color_PZIn { get; private set; }
    #endregion

    private void Start()
    {
        InitializeGridColor();
        IsGridGoReady = SpawnGridGo(_gridGo, GridArrayResource.GridArrArr[1]);
        Debug.Log(SprDic.Count);
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

    public bool SpawnGridGo(GameObject _gridGo, int[,] gridArr)
    {
        NowGridArr = gridArr;
        _gridGo = new GameObject("Grid");
        _gridGo.transform.parent = _gridParentTr;

        if (_gridGo.AddComponent<Grid>())
        {
            GameObject pzPartPrefab = Resources.Load<GameObject>(Path.PuzzlePartPrefab);

            for (int i = 0; i < NowGridArr.GetLength(0); i++)
            {
                for (int j = 0; j < NowGridArr.GetLength(1); j++)
                {
                    GameObject gridPartGo = Instantiate(pzPartPrefab, Vector3.zero, Quaternion.identity, _gridGo.transform);
                    gridPartGo.name = $"GridPart_{i}_{j}";
                    gridPartGo.transform.localPosition = new Vector3(j + j * 0.1f, -(i + i * 0.1f), 0f);
                    gridPartGo.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);
                    SpriteRenderer spr = gridPartGo.GetComponent<SpriteRenderer>();
                    Util.AddDictionary(SprDic, gridPartGo.name, spr);
                    SetGridColor(spr, NowGridArr[i, j]);
                    GridPart gridPart = gridPartGo.AddComponent<GridPart>();
                    gridPart.HasPuzzle = NowGridArr[i, j] == 1 ? true : false;
                    gridPart.IdxRow = i;
                    gridPart.IdxCol = j;
                }
            }
        }

        Util.SetPivotToChildCenter(_gridGo.transform);
        _gridGo.tag = "Grid";
        _gridGo.transform.position = Factor.PosGridSpawn;
        _gridGo.transform.localScale = Factor.ScalePuzzleNormal;
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
