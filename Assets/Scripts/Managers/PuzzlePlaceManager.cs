using UnityEngine;

/// <summary>
/// : �÷��̾ ������ ���� Place ���ִ� Ŭ����
/// - Placable check �� �����ͼ� ���� ���� �� Place 
/// 
/// (TODO) 1. GameOver F ���� üũ
/// (TODO) 2. ������ ���� Ȱ��ȭ���� ���� üũ
/// (TODO) 3. ������ ���� �׸��� �� ��ġ ���� üũ
/// (TODO) 4. completeManager ȣ�� - completable check 
/// (TODO) 5. ������ ���� Placable ��ġ�� drop
/// (TODO) 6. completeManager ȣ�� - complete => grid update���ִ� Ŭ���� ȣ��
/// </summary>
public class PuzzlePlaceManager : MonoBehaviour
{
    private PuzzlePlacableChecker _puzzlePlacableChecker = null;
    private GridManager _gridManager = null;
    private PuzzleManager _puzzleManager = null;

    private void Awake()
    {
        _puzzlePlacableChecker = this.GetComponent<PuzzlePlacableChecker>();
        _gridManager = this.GetComponentInChildren<GridManager>();
        _puzzleManager = this.GetComponentInChildren<PuzzleManager>();
    }
    private void Start()
    {
        _gridManager.Iniit(CheckPlacable_AllRemaingPuzzles);
        _puzzleManager.Iniit(CheckPlacable_AllRemaingPuzzles);
    }
    /// <summary>
    /// PuzzlePlacableChecker ȣ���Ͽ� ��ü ���� ���� CheckPlacable �˻�
    /// </summary>
    public void CheckPlacable_AllRemaingPuzzles()
    {
        _puzzlePlacableChecker.CheckPlacable_AllRemainingPuzzles(_gridManager.Grid.Data, _puzzleManager.PuzzleGoArr);
    }
    public bool CheckPlacable_TouchingGo(GameObject touchingGo)
    {
        if (touchingGo == null) return false;

        Puzzle puzzle = touchingGo.GetComponent<Puzzle>();
        if (puzzle == null) return false; 

        // 1. Grid�� �ִ��� üũ
        if (_puzzlePlacableChecker.CheckPuzzleInGrid() == false) return false;

        // 2. �׸��� ���� �ش� ������ ���� ������ �ִ��� üũ 
        if (_puzzlePlacableChecker.CheckPlacable(_gridManager.Grid.Data, puzzle) == false) return false;

        return true;
    }
    /// <summary>
    /// Touching Go�� CheckPlacable == true �� �� Drop���� ���� ���� �ۼ�
    /// (TODO) 6. completeManager ȣ�� - complete => grid update���ִ� Ŭ���� ȣ��
    /// </summary>
    public void PlacePuzzle()
    {
        // TODO 
        Debug.Log("Puzzle Place Process");
    }
    //public bool CanPlacePuzzle(int[,] puzzle)
    //{
    //    int puzzleRows = puzzle.GetLength(0); //4
    //    int puzzleCols = puzzle.GetLength(1); //4
    //                                          // ������ �׸��带 ������� Ȯ��
    //    if (row + puzzleRows > grid.GetLength(0) || col + puzzleCols > grid.GetLength(1))
    //        return false;
    //    // ������ �ٸ� ����� ��ġ���� Ȯ��
    //    for (int i = 0; i < puzzleRows; i++)
    //    {
    //        for (int j = 0; j < puzzleCols; j++)
    //        {
    //            if (puzzle[i, j] == 1 && grid[row + i, col + j] != 0)
    //                return false;
    //        }
    //    }

    //    return false;
    //}

    ///// <summary>
    ///// ������ �׸��忡 ��ġ
    ///// </summary>
    ///// <param name=""></param>
    ///// <returns></returns>
    //public void PlacePuzzle(int[,] grid, int[,] puzzle, int row, int col)
    //{
    //    int puzzleRows = puzzle.GetLength(0);
    //    int puzzleCols = puzzle.GetLength(1);
    //    for (int i = 0; i < puzzleRows; i++)
    //    {
    //        for (int j = 0; j < puzzleCols; j++)
    //        {
    //            if (puzzle[i, j] == 1)
    //                grid[row + i, col + j] = 1;
    //        }
    //    }
    //    Debug.Log("������ ���������� ��ġ�Ǿ����ϴ�.");
    //}

} // end of class