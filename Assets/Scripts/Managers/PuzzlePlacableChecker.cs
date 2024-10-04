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

    private string _pzNameBackupStr = string.Empty;
    private Dictionary<string, List<IdxRCStruct>> _cntTempDic = new Dictionary<string, List<IdxRCStruct>>();
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
        if (grid == null || puzzleGoArr[0] == null) return;

        bool isGameOver = true;
        int remainingPuzzleCnt = 0;
        int gameOverCheckNum = 0;
        foreach (GameObject go in puzzleGoArr)
        {
            // # �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ
            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                remainingPuzzleCnt++;

                if (!CheckPlacable(grid, puzzle))
                    gameOverCheckNum++;
            }
            else // # �ش� �迭 �� Puzzle Go ����
                continue;
        }

        if (remainingPuzzleCnt == 0)
        {
            _stageCompleteCallback?.Invoke();
            isGameOver = false;
        }
        else if (remainingPuzzleCnt != 0 && remainingPuzzleCnt == gameOverCheckNum)
            isGameOver = true; // # GameOver 
        else
            isGameOver = false; // # GameOver�� �ƴ� 

        if (!isGameOver)
            _gameOverCallback?.Invoke();
    }

    /// <summary>
    /// �ش� Grid�� �ش� PUZZLE�� ���� �� �ִ��� Ȯ��
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    /// <returns></returns>
    private bool CheckPlacable(Grid grid, Puzzle puzzle)
    {
        if (!puzzle.IsInGrid) return false;

        _cntTempDic.Clear();
        GetPlacableIdxs(ref _cntTempDic, true, grid, puzzle);

        if (_cntTempDic.Count > 0) return true;
        else return false;
    }

    /// <summary> 
    /// ������ placable�� ��� ���� (0,0)�� ��� grid �ε��� Dic�� ��� 
    /// </summary>
    /// <param name="needFunction"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void GetPlacableIdxs(ref Dictionary<string, List<IdxRCStruct>> idxDic, bool needFunction, Grid grid, Puzzle puzzle)
    {
        if (needFunction)
        {
            if (_pzNameBackupStr == puzzle.name) return;

            idxDic.Clear();
            _pzNameBackupStr = puzzle.name;
            grid.BackupData = grid.Data;

            // # �׸��� ��� idx üũ
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    List<IdxRCStruct> gridPartsIdxList = new List<IdxRCStruct>();
                    gridPartsIdxList.Clear();
                    bool isPlacable = CheckMappingGridInspectionAreaAndPuzzle(grid, puzzle, grIdxR, grIdxC, ref gridPartsIdxList);
                    // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� => �ش� Grid���������ʱ� �ε��� List�� ���
                    if (isPlacable)
                    {
                        Util.CheckAndAddDictionary(idxDic, new IdxRCStruct(grIdxR, grIdxC).ToString(), gridPartsIdxList);

#if DEBUGING
                        MarkAllPlacableIdx(idxDic, grid);
#endif
                    }
                }
        }
        else
        {
            idxDic.Clear();
            _pzNameBackupStr = string.Empty;
            grid.Data = grid.BackupData;
        }
    }

    /// <summary>
    /// �ش� "grididx + ���� ���� idx ����"(named "GridInspectionArea")�� 
    /// "�������Ͱ� 1�̰� �׸��嵥���Ͱ� !=1"(named "������ΰ˻�") �̸� 
    ///  1. GridInspectionArea Upper-left �ε��� List�� ���
    ///  2. �ش��ε����� key������ �ϴ� Dictionary�� gridPartIdx ����
    /// </summary> 
    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC, ref List<IdxRCStruct> gridPartsIdxList)
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
        for (int i = idxRangeR[0]; i <= idxRangeR[1]; i++)
        {
            int puzzleIdxC = 0;
            for (int j = idxRangeC[0]; j <= idxRangeC[1]; j++)
            {
                // # �� �ȿ��� griddata !=1 && puzzledata ==1�� ��  ���� ������ŭ  List�� ����
                if (grid.Data[i, j] == 1) // grid�� �ش� �ε����� ������ �������� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) // => ok 
                        isPlacable &= true;
                    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no 
                        isPlacable &= false;
                }
                else // grid.Data[i, j] == 0 || 2  // grid�� �ش� �ε����� ������ �������� ���� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) // => ok
                    {
                        isPlacable &= true;
                        gridPartsIdxList.Add(new IdxRCStruct(i, j));
                    }
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
    private void GetNearestPlacableIdx(ref Dictionary<string, List<IdxRCStruct>> idxDic, Puzzle touchingPZ)
    {

        // MarkPlacableIdx()
    }

    /// <summary>
    /// Placable�� ���� �� TouchingGo�� ���� ����� ���� ���� ����
    /// </summary>
    private void MarkPlacableIdx()
    {

    }
    /// <summary>
    /// Placable�� ��� ���� ���� ����
    /// </summary>
    private void MarkAllPlacableIdx(Dictionary<string, List<IdxRCStruct>> dic, Grid grid)
    {
        // # Placable�� ��� ���� ���� ����
        foreach (KeyValuePair<string, List<IdxRCStruct>> kvp in dic)
            foreach (IdxRCStruct idx in kvp.Value)
                grid.SetGridPartData(idx.IdxR, idx.IdxC, 2);
    }

} // end of class 