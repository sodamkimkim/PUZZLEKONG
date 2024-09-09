using UnityEngine;

public class PuzzlePlacer : MonoBehaviour
{
    public int[,] Grid;
    public int[,] Puzzle;

    // 그리드 크기
    public int GridRows = 5;
    public int GridCols = 5;

    private void Start()
    {
        Grid = new int[GridRows, GridCols];
        Puzzle = new int[,] {
            { 1, 1, 1},
            { 0, 1, 0 },
            { 0, 1, 0 }
        };

        // 퍼즐을 (1,1)위치에 놓을 수 있는지 확인
        if (CanPlacePuzzle(Grid, Puzzle, 1, 1))
        {
            Debug.Log("퍼즐을 놓을 수 있습니다.");
            PlacePuzzle(Grid, Puzzle, 1, 1);
        }
    }

    /// <summary>
    /// 퍼즐을 (row, col)위치에 놓을 수 있는 지 확인
    /// </summary>
    /// <returns></returns>
    private bool CanPlacePuzzle(int[,] grid, int[,] puzzle, int row, int col)
    {
        int puzzleRows = puzzle.GetLength(0); // 3
        int puzzleCols = puzzle.GetLength(1); // 3

        // 퍼즐이 그리드를 벗어나는지 확인
        if (row + puzzleRows > grid.GetLength(0) || col + puzzleCols > grid.GetLength(1))
            return false;

        // 퍼즐이 다른 퍼즐과 겹치는지 확인
        for (int i = 0; i < puzzleRows; i++)
        {
            for (int j = 0; j < puzzleCols; j++)
            {
                if (puzzle[i, j] == 1 && grid[row + i, col + j] != 0)
                    return false;
            }
        }

        return false;
    }
    /// <summary>
    /// 퍼즐을 그리드에 배치
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    private void PlacePuzzle(int[,] grid, int[,] puzzle, int row, int col)
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