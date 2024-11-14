#define DEBUGING

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// : ���� ���� �� ���� �� & ���� ������ ���� Placable Check 
/// (TODO) 1. selectPuzzle�׸��� ���������� check => T|F
/// (TODO) 2. �� ���� Placable == false => ��Ȱ��ȭ : Ȱ��ȭ
/// (DONE) 3. ��� Placable == false => GameOver
/// - check�� Ư���ð� �ݺ��� �ƴ϶� �̺�Ʈ ����
///   ��
///   1. puzzle 3�� ���� �������� ��(PuzzleManager),
///   2. ���� �׸��忡 ��� ���� �� (complete�ǰų� �ȵǰų� ��� gridArr�� refresh �� ) (GridManager) 
///   
/// return ) 
/// - Grid���� �� T|F
/// - �� ���� Placable T|F & �� ���� Placable Grid�ڸ� ���򺯰�
/// - GameOver���� T|F
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
    /// Callback �ż���
    ///   1. puzzle 3�� ���� �������� ��(PuzzleManager),
    ///   2. ���� �׸��忡 ��� ���� �� (complete�ǰų� �ȵǰų� ��� gridArr�� refresh �� ) (GridManager) 
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
            // # �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ 

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
            else // # �ش� �迭 �� Puzzle Go ����
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
            isGameOver = false; // # GameOver�� �ƴ� 

        if (isGameOver)
            _gameOverCallback?.Invoke();
        else
            return;
    }
    public int SetPuzzlesActiveAndCountActive(Grid grid, GameObject[] puzzleGoArr)
    {
        if (grid == null || puzzleGoArr == null) return 0;

        int activePuzzleCnt = 0;
        foreach (GameObject go in puzzleGoArr)
        {
            if (go == null) continue;
            // # �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ 

            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                if (CheckPlacableThisPuzzle(grid, puzzle) == 0)
                {
                    puzzle.ActiveSelf = false;
                }
                else
                {
                    puzzle.ActiveSelf = true;
                    activePuzzleCnt++;
                }
            }
            else // # �ش� �迭 �� Puzzle Go ����
                continue;
        }
        return activePuzzleCnt;
    }
    public void CheckStageCompleteOrGameOver(Grid grid, GameObject[] puzzleGoArr)
    {
        if (grid == null || puzzleGoArr == null) return;

        int remainingPuzzleCnt = 0;
        int gameOverCheckCnt = 0;
        foreach (GameObject go in puzzleGoArr)
        {
            if (go == null) continue;
            // # �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ 

            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                if (CheckPlacableThisPuzzle(grid, puzzle) == 0)
                    gameOverCheckCnt++;

                remainingPuzzleCnt++;
            }
            else // # �ش� �迭 �� Puzzle Go ����
                continue;
        }

        if (remainingPuzzleCnt == 0)
        {
            _stageCompleteCallback?.Invoke();
        }
        else if (remainingPuzzleCnt != 0 && remainingPuzzleCnt == gameOverCheckCnt)
        {
            _gameOverCallback?.Invoke();
        }
    }

    /// <summary>
    /// ��� Grid Data �˻��Ͽ� �� ������ �׸��忡 ���� �ڸ��� �ִ��� ���� �˻�
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    /// <returns></returns>
    public int CheckPlacableThisPuzzle(Grid grid, Puzzle puzzle)
    {
        int cnt = 0;
        // # �׸��� ��� idx üũ
        for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
            for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
            {
                // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� => �ش� Grid���������ʱ� �ε��� List�� ���
                if (CheckMappingGridInspectionAreaAndPuzzle(grid, puzzle, grIdxR, grIdxC))
                    cnt++;
            }
        //   Debug.Log($"{puzzle.name} placable cnt: {cnt}");
        return cnt;
    }

    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
    {
        if (puzzle == false) return false;

        // # �˻��� Grid���� ���� (GridInspectionArea)
        int[] idxRangeR = new int[2] { grIdxR, grIdxR + puzzle.LastIdx_rc[0] };
        int[] idxRangeC = new int[2] { grIdxC, grIdxC + puzzle.LastIdx_rc[1] };

        // # grid idx�� ���� ������ ���� ���ϸ� return
        if (idxRangeR[1] > grid.Data.GetLength(0) - 1) return false;
        if (idxRangeC[1] > grid.Data.GetLength(1) - 1) return false;


        // # �ش� Grid������ ���� ���� �������� �˻�
        bool isPlacable = true;
        int puzzleIdxR = 0;
        for (int r = idxRangeR[0]; r <= idxRangeR[1]; r++)
        {
            int puzzleIdxC = 0;
            for (int c = idxRangeC[0]; c <= idxRangeC[1]; c++)
            {
                // #  
                //if (grid.Data[r, c] == Factor.HasPuzzle) // grid�� �ش� �ε����� ������ �������� ��
                //{
                //    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == Factor.HasNoPuzzle) // => ok 
                //        isPlacable &= true;
                //    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no 
                //        isPlacable &= false;
                //}
                //else // grid.Data[i, j] == 0 || 2  // grid�� �ش� �ε����� ������ �������� ���� ��
                //{
                //    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == Factor.HasPuzzle) // => ok 
                //        isPlacable &= true;  // ����ֱ�?
                //    else // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) => ok 
                //        isPlacable &= true;
                //}
                if (grid.Data[r, c] == Factor.HasNoPuzzle) // grid�� �ش� �ε����� ������ �������� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == Factor.HasPuzzle) // => ok 
                        isPlacable &= true;  // ����ֱ�?
                    else // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) => ok 
                        isPlacable &= true;
                }
                else // grid.Data[i, j] == 0 || 2  // grid�� �ش� �ε����� ������ �������� ���� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == Factor.HasNoPuzzle) // => ok 
                        isPlacable &= true;
                    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no 
                        isPlacable &= false;
                }



                puzzleIdxC++;
            }
            puzzleIdxR++;
        }
        return isPlacable;
    }

    public bool MarkPlacable(Grid grid, Puzzle touchingPZ)
    {
        if (grid == null || touchingPZ == null)
            return false;

        MarkPlacableReset(grid);
        bool placable = true;
        // ��� Pzpart triggered Gridpart ==0 �̸� placable on
        foreach (PZPart pzpart in touchingPZ.ChildPZPartList)
        {
            string triggered = pzpart.TriggeredGridPartIdxStr;
            if (triggered != string.Empty && grid.ChildGridPartDic.ContainsKey(triggered))
            {
                GridPart gp = grid.ChildGridPartDic[triggered];
                if (gp.Data != Factor.HasPuzzle)
                    placable &= true;
                else
                    placable &= false;
            }
            else
                placable &= false;
        }
        if (placable)
        {
            SetPlacableGridData(grid, touchingPZ, Factor.HasNoPuzzle, Factor.Placable);
            return true;
        }
        else return false;
    }
    public void MarkPlacableReset(Grid grid)
    {
        foreach (KeyValuePair<string, GridPart> kvp in grid.ChildGridPartDic)
        {
            if (kvp.Value.Data == Factor.Placable)
            {
                kvp.Value.Data = Factor.HasNoPuzzle;
                kvp.Value.SetGridPartColor();
            }
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
                {
                    grid.ChildGridPartDic[triggered].Data = toData;
                    grid.ChildGridPartDic[triggered].SetGridPartColor();
                }
        }
    }

    /// <summary>
    /// Pz place�� �õ��ϰ�, place�Ǹ� return isPlacePzSuccess = true;
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
            SetPlacableGridData(grid, touchingPZ, Factor.Placable, Factor.HasPuzzle);
            DestroyImmediate(touchingPZ.gameObject);
        }

        MarkPlacableReset(grid);
        return isPlacePZSucess;
    }
} // end of class 