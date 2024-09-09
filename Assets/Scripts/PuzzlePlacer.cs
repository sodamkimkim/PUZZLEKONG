using UnityEngine;

public class PuzzlePlacer : MonoBehaviour
{
    public int[,] Grid;
    public int[,] Puzzle;

    // �׸��� ũ��
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

        // ������ (1,1)��ġ�� ���� �� �ִ��� Ȯ��
        if (CanPlacePuzzle(Grid, Puzzle, 1, 1))
        {
            Debug.Log("������ ���� �� �ֽ��ϴ�.");
            PlacePuzzle(Grid, Puzzle, 1, 1);
        }
    }

    /// <summary>
    /// ������ (row, col)��ġ�� ���� �� �ִ� �� Ȯ��
    /// </summary>
    /// <returns></returns>
    private bool CanPlacePuzzle(int[,] grid, int[,] puzzle, int row, int col)
    {
        int puzzleRows = puzzle.GetLength(0); // 3
        int puzzleCols = puzzle.GetLength(1); // 3

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