using System.Collections.Generic;
using System.Collections;
using UnityEngine;
[DefaultExecutionOrder(-9)]
public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Transform _gridParentTr = null;

    #region Hidden Private Variables
    private Grid _grid;
    private static bool _isGridReady = false;
    #endregion

    #region GridGo°ü¸®
    public Grid Grid { get => _grid; private set => _grid = value; }
    public bool IsGridGoReady
    {
        get => _isGridReady;
        private set
        {
            _isGridReady = value;
        }
    }
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
        GameObject pzPartPrefab = Resources.Load<GameObject>(Path.PuzzlePartPrefab);
        IsGridGoReady = SpawnGridGo(GridArrayRepository.GridArrArr[1], pzPartPrefab);
     //   Debug.LogError(Util.ConvertDoubleArrayToString(GridArrayRepository.GridArrArr[1])); 
    }


    public bool SpawnGridGo(int[,] gridArr, GameObject pzPartPrefab)
    {
        int rowCnt = gridArr.GetLength(0);
        int colCnt = gridArr.GetLength(1);
        GameObject gridGo = new GameObject("Grid");
        gridGo.transform.parent = _gridParentTr;

        Grid grid = Util.CheckAndAddComponent<Grid>(gridGo);
        Grid = grid;

        for (int r = 0; r < rowCnt; r++)
        {
            for (int c = 0; c < colCnt; c++)
            {
                GameObject gridPartGo = Instantiate(pzPartPrefab, Vector3.zero, Quaternion.identity, gridGo.transform);
                gridPartGo.name = $"GridPart_{r}_{c}"; // GridPart_{r}_{c}
                gridPartGo.transform.localPosition = new Vector3(c + c * 0.1f, -(r + r * 0.1f), 0f);
                gridPartGo.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);

                // GridPart
                GridPart gridPart = Util.CheckAndAddComponent<GridPart>(gridPartGo);
                gridPart.ParentGrid = grid;
                gridPart.Spr = gridPart.GetComponent<SpriteRenderer>();
                Util.AddDictionary<GridPart>(grid.ChildGridPartDic, gridPart.name, gridPart);
              
            }
        }

        grid.Data = gridArr;
        Util.SetPivotToChildCenter(gridGo.transform);
        gridGo.tag = "Grid";
        gridGo.transform.position = Factor.PosGridSpawn;
        gridGo.transform.localScale = Factor.ScalePuzzleNormal;


        //// Rigidbody
        //Rigidbody2D rigidbody2D = gridGo.AddComponent<Rigidbody2D>();
        //rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        // BoxCollider2D
        BoxCollider2D collider = gridGo.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(rowCnt * 1.1f, colCnt * 1.1f);
        return true;
    }


} // end of class
