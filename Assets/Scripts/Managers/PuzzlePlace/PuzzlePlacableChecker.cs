#define DEBUGING

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    #region delegate
    public delegate void GameOver();
    private GameOver _gameOverCallback;
    public delegate void StageComplete();
    private StageComplete _stageCompleteCallback;
    #endregion

    public void Init(GameOver gameOverCallback, StageComplete stageComplete)
    {
        _gameOverCallback = gameOverCallback;
        _stageCompleteCallback = stageComplete;
    }

    /// <summary>
    /// Callback 매서드
    ///   1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
    ///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacableAllRemainingPuzzles(Grid grid, GameObject[] puzzleGoArr)
    {
        if (grid == null || puzzleGoArr == null) return;

        bool isGameOver = true;
        int remainingPuzzleCnt = 0;
        int gameOverCheckCnt = 0;
        foreach (GameObject go in puzzleGoArr)
        {
            if (go == null) continue;
            // # 해당 배열 내 Puzzle Go가 있고 Placable이면 true반환 

            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                if (CheckPlacableThisPuzzle(grid, puzzle) == 0)
                {
                    puzzle.ActiveSelf = false;
                    gameOverCheckCnt++;
                }
                else
                    puzzle.ActiveSelf = true;

                remainingPuzzleCnt++;
            }
            else // # 해당 배열 내 Puzzle Go 없음
                continue;
        }

        if (remainingPuzzleCnt == 0)
        {
            _stageCompleteCallback?.Invoke();
            isGameOver = false;
        }
        else if (remainingPuzzleCnt != 0 && remainingPuzzleCnt == gameOverCheckCnt)
            isGameOver = true; // # GameOver 
        else
            isGameOver = false; // # GameOver는 아님 

        if (isGameOver)
            _gameOverCallback?.Invoke();
        else
            return;
    }

    public int CheckPlacableThisPuzzle(Grid grid, Puzzle puzzle)
    {
        int cnt = 0;
        // # 그리드 모든 idx 체크
        for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
            for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
            {
                // # Grid-Puzzle영역 Puzzle매핑 검사 완료 후 true라면 => 해당 Grid영역가장초기 인덱스 List에 담기
                if (CheckMappingGridInspectionAreaAndPuzzle(grid, puzzle, grIdxR, grIdxC))
                    cnt++;
            }
        Debug.Log($"{puzzle.name} placable cnt: {cnt}");
        return cnt;
    }

    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
    {
        if (puzzle == false) return false;

        // # 검사할 Grid영역 설정 (GridInspectionArea)
        int[] idxRangeR = new int[2] { grIdxR, grIdxR + puzzle.LastIdx_rc[0] };
        int[] idxRangeC = new int[2] { grIdxC, grIdxC + puzzle.LastIdx_rc[1] };

        // # grid idx가 퍼즐 영역을 담지 못하면 return
        if (idxRangeR[1] > grid.Data.GetLength(0) - 1) return false;
        if (idxRangeC[1] > grid.Data.GetLength(1) - 1) return false;


        // # 해당 Grid영역에 퍼즐 매핑 가능한지 검사
        bool isPlacable = true;
        int puzzleIdxR = 0;
        for (int r = idxRangeR[0]; r <= idxRangeR[1]; r++)
        {
            int puzzleIdxC = 0;
            for (int c = idxRangeC[0]; c <= idxRangeC[1]; c++)
            {
                // #  
                if (grid.Data[r, c] == 1) // grid의 해당 인덱스에 퍼즐이 놓여있을 떄
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) // => ok 
                        isPlacable &= true;
                    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no 
                        isPlacable &= false;
                }
                else // grid.Data[i, j] == 0 || 2  // grid의 해당 인덱스에 퍼즐이 놓여있지 않을 때
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) // => ok 
                        isPlacable &= true;  // 담아주기?
                    else // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) => ok 
                        isPlacable &= true;
                }
                puzzleIdxC++;
            }
            puzzleIdxR++;
        }
        return isPlacable;
    }

    public void MarkPlacable(Grid grid, Puzzle touchingPZ)
    {
        if (grid == null || touchingPZ == null)
            return;

        MarkPlacableReset(grid);
        bool placable = true;
        // 모든 Pzpart triggered Gridpart ==0 이면 placable on
        foreach (PZPart pzpart in touchingPZ.ChildPZPartList)
        {
            string triggered = pzpart.TriggeredGridPartIdxStr;
            if (triggered != string.Empty && grid.ChildGridPartDic.ContainsKey(triggered))
            {
                GridPart gp = grid.ChildGridPartDic[triggered];
                if (gp.Data != 1)
                    placable &= true;
                else
                    placable &= false;
            }
            else
                placable &= false;
        }
        if (placable)
            SetPlacableGridData(grid, touchingPZ, 0, 2);
    }
    public void MarkPlacableReset(Grid grid)
    {
        foreach (KeyValuePair<string, GridPart> kvp in grid.ChildGridPartDic)
        { 
            if (kvp.Value.Data == 2)
                kvp.Value.Data = 0;
        }
    }
    private void SetPlacableGridData(Grid grid, Puzzle touchingPZ, int beforeData, int toData)
    {
        if (grid == null || touchingPZ == null) return;
        foreach (PZPart pzpart in touchingPZ.ChildPZPartList)
        {
            string triggered = pzpart.TriggeredGridPartIdxStr;
            if (grid.ChildGridPartDic.ContainsKey(triggered))
                if (grid.ChildGridPartDic[triggered].Data == beforeData)
                    grid.ChildGridPartDic[triggered].Data = toData;
        }
    }

    /// <summary>
    /// Pz place를 시도하고, place되면 return isPlacePzSuccess = true;
    /// </summary>
    /// <returns></returns>
    public bool PuzzlePlace(Grid grid, Puzzle touchingPZ)
    {
        if (grid == null || touchingPZ == null) return false;

        bool isPlacePZSucess = true;

        foreach (PZPart pzpart in touchingPZ.ChildPZPartList)
        {
            string triggered = pzpart.TriggeredGridPartIdxStr;
            if (triggered != string.Empty && grid.ChildGridPartDic.ContainsKey(triggered))
            {
                GridPart gp = grid.ChildGridPartDic[triggered];
                if (gp.Data == 2)
                    isPlacePZSucess &= true;
                else
                    isPlacePZSucess &= false;
            }
            else
                isPlacePZSucess &= false;
        }
        if (isPlacePZSucess)
        {
            SetPlacableGridData(grid, touchingPZ, 2, 1);
            DestroyImmediate(touchingPZ.gameObject);
        }

        return isPlacePZSucess;
    }
} // end of class 