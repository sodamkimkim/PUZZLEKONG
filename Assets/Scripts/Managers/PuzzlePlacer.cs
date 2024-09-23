using UnityEngine;

public class PuzzlePlacer : MonoBehaviour
{
    private GridSpawner _gridSpawner = null;
    private PuzzleSpawner _puzzleSpawner = null;
    private void Awake()
    {
        _gridSpawner = this.GetComponent<GridSpawner>();
        _puzzleSpawner = this.GetComponent<PuzzleSpawner>();
    }
    private void Start()
    {
        //// 퍼즐을 (1,1)위치에 놓을 수 있는지 확인
        //if (CanPlacePuzzle(_gridManager.GridRowsCols, _puzzleManager.PuzzleArr[0], 1, 1))
        //{
        //    Debug.Log("퍼즐을 놓을 수 있습니다.");
        //    PlacePuzzle(_gridManager.GridRowsCols, _puzzleManager.PuzzleArr[0], 1, 1);
        //} 
        LazyStart();
    }

    private void LazyStart()
    {
        if (GridSpawner.IsGridGoReady && _puzzleSpawner.HasPuzzlCheck() != 0)
        {
            Debug.Log("LazyStart"); 
        }
    }
    /// <summary>
    /// 퍼즐을 (row, col)위치에 놓을 수 있는 지 확인
    /// </summary>
    /// <returns></returns>
    public bool CanPlacePuzzle( int[,] puzzle )
    {
        //int puzzleRows = puzzle.GetLength(0); //4
        //int puzzleCols = puzzle.GetLength(1); //4

        //// 퍼즐이 그리드를 벗어나는지 확인
        //if (row + puzzleRows > grid.GetLength(0) || col + puzzleCols > grid.GetLength(1))
        //    return false;

        //// 퍼즐이 다른 퍼즐과 겹치는지 확인
        //for (int i = 0; i < puzzleRows; i++)
        //{
        //    for (int j = 0; j < puzzleCols; j++)
        //    {
        //        if (puzzle[i, j] == 1 && grid[row + i, col + j] != 0)
        //            return false;
        //    }
        //}

        return false;
    }
    /// <summary>
    /// 퍼즐을 그리드에 배치
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public void PlacePuzzle(int[,] grid, int[,] puzzle, int row, int col)
    {
        int puzzleRows = puzzle.GetLength(0);
        int puzzleCols = puzzle.GetLength(1);

        for (int i = 0; i < puzzleRows; i++)
        {
            for (int j = 0; j < puzzleCols; j++)
            {
                if (puzzle[i, j] == 1)
                    grid[row + i, col + j] = 1;
            }
        }
        Debug.Log("퍼즐이 성공적으로 배치되었습니다.");
    }
} // end of class