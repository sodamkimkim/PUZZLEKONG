using UnityEngine;

/// <summary>
/// : 퍼즐 세개 중 남은 것 & 현재 선택한 퍼즐 Placable Check 
/// (TODO) 1. selectPuzzle그리드 영역내인지 check => T|F
/// (TODO) 2. 각 퍼즐 Placable == false => 비활성화 : 활성화
/// (DONE) 3. 모두 Placable == false => GameOver
/// - check는 특정시간 반복이 아니라 이벤트 종속
///   ㄴ
///   1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
///   
/// return ) 
/// - Grid영역 내 T|F
/// - 각 퍼즐 Placable T|F & 각 퍼즐 Placable Grid자리 색깔변경
/// - GameOver여부 T|F
/// 
/// </summary>
public class PuzzlePlacableChecker : MonoBehaviour
{
    /// <summary>
    /// Callback 매서드
    ///   1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
    ///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable_AllRemainingPuzzles(Grid grid, GameObject[] puzzleGoArr)
    {// TODO 
        if (IsCheckGameOver(grid, puzzleGoArr))
            Debug.Log("GameOver");
    }

    /// <summary>
    ///  - 배열내 모든 Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsCheckGameOver(Grid grid, GameObject[] puzzleGoArr)
    {
        int puzzleCnt = 0;
        int gameOverCheckNum = 0;

        foreach (GameObject go in puzzleGoArr)
        {
            // 해당 배열 내 Puzzle Go가 있고 Placable이면 true반환
            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                puzzleCnt++;

                if (CheckPlacableInThisGrid(grid, puzzle))
                    gameOverCheckNum++;
            }
            else // 해당 배열 내 Puzzle Go 없음
                continue;
        }

        if (puzzleCnt == 0)
        {
            // TODO
            Debug.Log("StageComplete");
            return false;
        }
        else if (puzzleCnt != 0 && puzzleCnt == gameOverCheckNum)
            return true; // GameOver 
        else
            return false; //GameOver는 아님 
    }


    /// <summary>
    /// 해당 Grid에 PUZZLE을 넣을 수 있는지 확인
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    /// <returns></returns>
    private bool CheckPlacableInThisGrid(Grid grid, Puzzle puzzle)
    {
        if (!puzzle.IsInGrid) return false;
        // TODO 

        return false;
    }
    /// <summary>
    /// puzzle의 현재 위치가 placable한지 판별하고 mark
    /// </summary>
    /// <param name="isPZMoving"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void MarkPlacable(bool isPZMoving, Grid grid, Puzzle puzzle)
    {
        if (isPZMoving)
        {
            grid.BackupData = grid.Data;

            // 퍼즐이 다른 퍼즐과 겹치는지 확인
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
            {
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    // Grid안인지 check
                    int lastRowIdx = grIdxR + puzzle.LastIdx_rc[0];
                    int lastColIdx = grIdxC + puzzle.LastIdx_rc[1];
                    if (lastRowIdx > grid.Data.GetLength(0) - 1 || lastColIdx > grid.Data.GetLength(1) - 1)
                    {
                        Debug.Log($"CheckIdx : {lastRowIdx}, {lastColIdx}");
                        continue;
                    }
                    else
                    {
                        if (grid.Data[grIdxR, grIdxC] == 0)
                        {
                            grid.SetGridPartDataRange(grIdxR, grIdxC, grIdxR, grIdxC, 0, 2); // 그리드 내부면 idx 색상변경
                            CanPlacePuzzle(grid, grIdxR, grIdxC, puzzle);
                        }
                    }
                }
            }
        }
        else
            grid.Data = grid.BackupData;

    }

    /// <summary>
    /// Grid 영역 내 Placable한 Idx && puzzle == 1이면서 grid[r,c] !=1 찾는 매서드
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridIdxR"></param>
    /// <param name="gridIdxC"></param>
    /// <param name="puzzle"></param>
    private void CanPlacePuzzle(Grid grid, int gridIdxR, int gridIdxC, Puzzle puzzle)
    {
        //int endIdxR = gridIdxR + puzzle.GetRealLength_rc[0];
        //int endIdxC = gridIdxC + puzzle.GetRealLength_rc[1];

        //// grid[r,c] + 퍼즐 영역 내 모든 데이터가 0이여야함
        //if (grid.CheckPlacable(gridIdxR, gridIdxC, endIdxR, endIdxC))
        //    grid.SetGridPartDataRange(gridIdxR, gridIdxC, endIdxR, endIdxC, 2, 3);
    }
} // end of class 