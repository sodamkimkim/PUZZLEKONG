using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _gridParentTr = null;
    public bool SpawnGridGo(int[,] gridArr,  Grid grid)
    { 
        GameObject pzPartPrefab = Resources.Load<GameObject>(Path.PuzzlePartPrefab);

        int rowCnt = gridArr.GetLength(0);
        int colCnt = gridArr.GetLength(1);
        GameObject gridGo = new GameObject("Grid");
        gridGo.transform.parent = _gridParentTr;

        grid = Util.CheckAndAddComponent<Grid>(gridGo); 

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
