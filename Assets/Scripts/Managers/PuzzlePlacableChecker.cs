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

    private string _pzNameBackupStr1 = string.Empty;
    private string _pzNameBackupStr2 = string.Empty;
    private Dictionary<string, Dictionary<string, IdxRCStruct>> _cntTempDic = new Dictionary<string, Dictionary<string, IdxRCStruct>>();
    private Dictionary<string, IdxRCStruct> _triggeredIdxDicBackup = new Dictionary<string, IdxRCStruct>();
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

                if (!CheckPlacableThisPuzzle(grid, puzzle))
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
    private bool CheckPlacableThisPuzzle(Grid grid, Puzzle puzzle)
    {
        if (!puzzle.IsInGrid) return false;

        _cntTempDic.Clear();
        GetPlacableIdxs(ref _cntTempDic, false, grid, puzzle);

        if (_cntTempDic.Count > 0) return true;
        else return false;
    }

    /// <summary> 
    /// ������ placable�� ��� ���� (0,0)�� ��� grid �ε��� Dic�� ��� 
    /// </summary>
    /// <param name="exitInitializeNow"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void GetPlacableIdxs(ref Dictionary<string, Dictionary<string, IdxRCStruct>> idxDic, bool exitInitializeNow, Grid grid, Puzzle puzzle)
    {
        if (!exitInitializeNow)
        {
            if (_pzNameBackupStr1 == puzzle.name) return;

            idxDic.Clear();
            _pzNameBackupStr1 = puzzle.name;
            //     grid.BackupData = grid.Data;

            // # �׸��� ��� idx üũ
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    Dictionary<string, IdxRCStruct> gridPartsIdxList = new Dictionary<string, IdxRCStruct>();
                    gridPartsIdxList.Clear();
                    bool isPlacable = CheckMappingGridInspectionAreaAndPuzzle(grid, puzzle, grIdxR, grIdxC, ref gridPartsIdxList);

                    // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� => �ش� Grid���������ʱ� �ε��� List�� ���
                    if (isPlacable)
                        Util.CheckAndAddDictionary(idxDic, new IdxRCStruct(grIdxR, grIdxC).ToString(), gridPartsIdxList);
                }
        }
        else // ExitInitialize == true
        {
            idxDic.Clear();
            _pzNameBackupStr1 = string.Empty;
            //  grid.Data = grid.BackupData;
        }
    }

    /// <summary>
    /// �ش� "grididx + ���� ���� idx ����"(named "GridInspectionArea")�� 
    /// "�������Ͱ� 1�̰� �׸��嵥���Ͱ� !=1"(named "������ΰ˻�") �̸� 
    ///  1. GridInspectionArea Upper-left �ε��� List�� ���
    ///  2. �ش��ε����� key������ �ϴ� Dictionary�� gridPartIdx ����
    /// </summary> 
    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC, ref Dictionary<string, IdxRCStruct> gridPartsIdxDic)
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
                        IdxRCStruct idxRCStruct = new IdxRCStruct(i, j);
                        Util.CheckAndAddDictionary<IdxRCStruct>(gridPartsIdxDic, idxRCStruct.ToString(), idxRCStruct);
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
    public void GetTriggeredPlacableIdx(Dictionary<string, Dictionary<string, IdxRCStruct>> idxsDic,
       ref Dictionary<string, IdxRCStruct> triggeredIdxDic, bool exitInitializeNow,
       Grid grid, Puzzle touchingPZ)
    {
        if (idxsDic == null || idxsDic.Count == 0)
        {
            Debug.Log(idxsDic);
        }
        if (grid == null || touchingPZ == null)
        {
            Debug.Log(grid.ToString());
            Debug.Log(touchingPZ.ToString());
            return;
        }

        if (!exitInitializeNow)
        {
            //        Debug.Log(Util.ConvertDoubleArrayToString(grid.Data));
            if (_pzNameBackupStr2 == touchingPZ.name) return;

            grid.BackupData = grid.Data;
            _pzNameBackupStr1 = touchingPZ.name;
            triggeredIdxDic.Clear();


            // # puzzle�� ���� �浹 ���� ��������  
            foreach (PZPart pzPart in touchingPZ.ChildPZPartList)
            {
                // # puzzlePart�� �浹�� GridPart�� Dictionary�� valueList���� ã��  
                foreach (KeyValuePair<string, Dictionary<string, IdxRCStruct>> kvp in idxsDic)
                {
                    string gpIdxStr = pzPart.TriggeredGridPartIdxStr;
                    if (kvp.Value.ContainsKey(pzPart.TriggeredGridPartIdxStr))
                    {
                        triggeredIdxDic = kvp.Value;

                        goto outerLoopEnd;
                    }

                }
            }
        outerLoopEnd:
            if (_triggeredIdxDicBackup != triggeredIdxDic)
            {
                MarkPlacableIdx(triggeredIdxDic, grid);
                _triggeredIdxDicBackup = triggeredIdxDic;
            }
        }

        else if (exitInitializeNow)// exitInitializeNow == true
        {
            //if (triggeredIdxDic != null)
            //    triggeredIdxDic.Clear();
            _pzNameBackupStr2 = string.Empty;
            if (grid.BackupData == null) grid.BackupData = grid.Data;
            grid.Data = grid.BackupData;
            Debug.Log(Util.ConvertDoubleArrayToString(grid.Data));

        }
        // MarkPlacableIdx()
    }

    /// <summary>
    /// Placable�� ���� �� TouchingGo�� �浹�� �� ���� ����
    /// </summary>
    private void MarkPlacableIdx(Dictionary<string, IdxRCStruct> dic, Grid grid)
    {
        foreach (KeyValuePair<string, IdxRCStruct> kvp in dic)
            grid.SetGridPartData(kvp.Value.IdxR, kvp.Value.IdxC, 2);
    }
    /// <summary>
    /// Placable�� ��� ���� ���� ����
    /// </summary>
    private void MarkAllPlacableIdx(Dictionary<string, Dictionary<string, IdxRCStruct>> dic, Grid grid)
    {
        // # Placable�� ��� ���� ���� ����
        foreach (KeyValuePair<string, Dictionary<string, IdxRCStruct>> kvp in dic)
            foreach (KeyValuePair<string, IdxRCStruct> kvp2 in kvp.Value)
                grid.SetGridPartData(kvp2.Value.IdxR, kvp2.Value.IdxC, 2);
    }

} // end of class 