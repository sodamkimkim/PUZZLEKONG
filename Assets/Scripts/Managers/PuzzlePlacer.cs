using UnityEngine;

public class PuzzlePlacer : MonoBehaviour
{
    private GridManager _gridManager = null;
    private PuzzleManager _puzzleManager = null;
    private void Awake()
    {
        _gridManager = this.GetComponent<GridManager>();
        _puzzleManager = this.GetComponent<PuzzleManager>();
    }
    private void Start()
    {
        //// ������ (1,1)��ġ�� ���� �� �ִ��� Ȯ��
        //if (CanPlacePuzzle(_gridManager.GridRowsCols, _puzzleManager.PuzzleArr[0], 1, 1))
        //{
        //    Debug.Log("������ ���� �� �ֽ��ϴ�.");
        //    PlacePuzzle(_gridManager.GridRowsCols, _puzzleManager.PuzzleArr[0], 1, 1);
        //}
    }

    /// <summary>
    /// ������ (row, col)��ġ�� ���� �� �ִ� �� Ȯ��
    /// </summary>
    /// <returns></returns>
    private bool CanPlacePuzzle(int[,] grid, int[,] puzzle, int row, int col)
    {
        int puzzleRows = puzzle.GetLength(0); //4
        int puzzleCols = puzzle.GetLength(1); //4

        // ������ �׸��带 ������� Ȯ��
        if (row + puzzleRows > grid.GetLength(0) || col + puzzleCols > grid.GetLength(1))
            return false;

        // ������ �ٸ� ����� ��ġ���� Ȯ��
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
    /// ������ �׸��忡 ��ġ
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
        Debug.Log("������ ���������� ��ġ�Ǿ����ϴ�.");
    }
} // end of class