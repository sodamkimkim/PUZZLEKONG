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
        if (grid == null || puzzleGoArr[0] == null|| puzzleGoArr[1] == null || puzzleGoArr[2] == null) return;

        bool isGameOver = true;
        int remainingPuzzleCnt = 0;
        int gameOverCheckCnt = 0;
        foreach (GameObject go in puzzleGoArr)
        {
            // # �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ
            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                remainingPuzzleCnt++;

                if (CheckPlacable(grid, puzzle) == 0)
                    gameOverCheckCnt++;
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
        {
            _gameOverCallback?.Invoke(); 
        }
    }
 

    /// <summary> 
    /// ������ placable�� ��� ���� (0,0)�� ��� grid �ε��� Dic�� ��� 
    /// </summary>
    /// <param name="needFunction"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public int CheckPlacable(Grid grid, Puzzle puzzle)
    {
      int  cnt = 0;
        // # �׸��� ��� idx üũ
        for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
            for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
            {
                // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� => �ش� Grid���������ʱ� �ε��� List�� ���
                if (CheckMappingGridInspectionAreaAndPuzzle(grid, puzzle, grIdxR, grIdxC))
                    cnt++;
            }
        Debug.Log($"{puzzle.name} placable cnt: {cnt}");
        return cnt;
    }

    /// <summary>
    /// �ش� "grididx + ���� ���� idx ����"(named "GridInspectionArea")�� 
    /// "�������Ͱ� 1�̰� �׸��嵥���Ͱ� !=1"(named "�����g�ΰ˻�") �̸� 
    ///  1. GridInspectionArea Upper-left �ε��� List�� ���
    ///  2. �ش��ε����� key������ �ϴ� Dictionary�� gridPartIdx ����
    /// </summary> 
    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
    {
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
                // # �� �ȿ��� griddata !=1 && puzzledata ==1�� ��  ���� ������ŭ  List�� ����
                if (grid.Data[r, c] == 1) // grid�� �ش� �ε����� ������ �������� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) // => ok 
                        isPlacable &= true;
                    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no 
                        isPlacable &= false;
                }
                else // grid.Data[i, j] == 0 || 2  // grid�� �ش� �ε����� ������ �������� ���� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) // => ok 
                        isPlacable &= true;  // ����ֱ�?
                    else // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) => ok 
                        isPlacable &= true;
                }
                puzzleIdxC++;
            }
            puzzleIdxR++;
        }
        return isPlacable;
    }

    /// <summary>
    /// - PuzzleTouchingGo �浹 �˻� ����� �޾Ƽ�
    /// - ���ÿ� �������� �ε����ٸ� ���� ó�� �ε��� �� ���� �Ҽ� GridPart�� �ڷᱸ���� �����
    /// </summary>
    public void GetTriggeredPlacableIdx(Grid grid, Puzzle touchingPZ)
    {
        if (grid == null || touchingPZ == null)
            return;

        MarkPlacableIdxReset(grid);
        bool placable = true;
        // ��� Pzpart triggered Gridpart ==0 �̸� placable on
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
        {
            foreach (PZPart pzpart in touchingPZ.ChildPZPartList)
            {
                string triggered = pzpart.TriggeredGridPartIdxStr;
                if (grid.ChildGridPartDic.ContainsKey(triggered))
                    grid.ChildGridPartDic[triggered].Data = 2;
            }
        }
    }
    public void MarkPlacableIdxReset(Grid grid)
    {
        foreach (KeyValuePair<string, GridPart> kvp in grid.ChildGridPartDic)
        {
            if (kvp.Value.Data == 2)
                kvp.Value.Data = 0;
        }
    }
} // end of class 