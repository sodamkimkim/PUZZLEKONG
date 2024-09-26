using UnityEngine;

/// <summary>
/// : 플레이어가 선택한 퍼즐 Place 해주는 클래스
/// - Placable check 값 가져와서 조건 맞을 떄 Place 
/// 
/// (TODO) 1. GameOver F 여부 체크
/// (TODO) 2. 선택한 퍼즐 활성화상태 여부 체크
/// (TODO) 3. 선택한 퍼즐 그리드 내 위치 여부 체크
/// (TODO) 4. completeManager 호출 - completable check 
/// (TODO) 5. 선택한 퍼즐 Placable 위치에 drop
/// (TODO) 6. completeManager 호출 - complete => grid update해주는 클래스 호출
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
    /// PuzzlePlacableChecker 호출하여 전체 남은 퍼즐 CheckPlacable 검사
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

        // 1. Grid에 있는지 체크
        if (_puzzlePlacableChecker.CheckPuzzleInGrid() == false) return false;

        // 2. 그리드 내에 해당 퍼즐을 놓을 공간이 있는지 체크 
        if (_puzzlePlacableChecker.CheckPlacable(_gridManager.Grid.Data, puzzle) == false) return false;

        return true;
    }
    /// <summary>
    /// Touching Go가 CheckPlacable == true 된 후 Drop했을 때의 로직 작성
    /// (TODO) 6. completeManager 호출 - complete => grid update해주는 클래스 호출
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
    //                                          // 퍼즐이 그리드를 벗어나는지 확인
    //    if (row + puzzleRows > grid.GetLength(0) || col + puzzleCols > grid.GetLength(1))
    //        return false;
    //    // 퍼즐이 다른 퍼즐과 겹치는지 확인
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
    ///// 퍼즐을 그리드에 배치
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
    //    Debug.Log("퍼즐이 성공적으로 배치되었습니다.");
    //}

} // end of class