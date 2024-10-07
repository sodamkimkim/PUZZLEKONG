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
    private Dictionary<string, Dictionary<string, IdxRCStruct>> _cntTempDic = new Dictionary<string, Dictionary<string, IdxRCStruct>>();
    private Dictionary<string, string> _foundGridPartCntDic = new Dictionary<string, string>();
    public static string KeyBackup = string.Empty;
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
        //  if (!puzzle.IsInGrid) return false;

        _cntTempDic.Clear();
        GetPlacableIdxs(ref _cntTempDic, false, grid, puzzle);

        if (_cntTempDic.Count > 0) return true;
        else return false;
    }

    /// <summary> 
    /// ������ placable�� ��� ���� (0,0)�� ��� grid �ε��� Dic�� ��� 
    /// </summary>
    /// <param name="needFunction"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void GetPlacableIdxs(ref Dictionary<string, Dictionary<string, IdxRCStruct>> idxDic, bool needFunction, Grid grid, Puzzle puzzle)
    {
        if (needFunction)
        {
            if (_pzNameBackupStr1 == puzzle.name) return;

            idxDic.Clear();
            _pzNameBackupStr1 = puzzle.name;
            //     grid.BackupData = grid.Data;

            // # �׸��� ��� idx üũ
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    Dictionary<string, IdxRCStruct> gridPartsIdxDic = new Dictionary<string, IdxRCStruct>();
                    gridPartsIdxDic.Clear();

                    // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� => �ش� Grid���������ʱ� �ε��� List�� ���
                    string firstIdxStr = string.Empty;
                    bool isPlacable = CheckMappingGridInspectionAreaAndPuzzle(grid, puzzle, grIdxR, grIdxC, ref gridPartsIdxDic, ref firstIdxStr);
                    if (isPlacable)
                        Util.CheckAndAddDictionary(idxDic, firstIdxStr, gridPartsIdxDic);
                }
        }
        else
        {
            idxDic.Clear();
            _pzNameBackupStr1 = string.Empty;
            //  grid.Data = grid.BackupData;
        }
        Debug.Log($"idxDic: {idxDic.Count}");
    }

    /// <summary>
    /// �ش� "grididx + ���� ���� idx ����"(named "GridInspectionArea")�� 
    /// "�������Ͱ� 1�̰� �׸��嵥���Ͱ� !=1"(named "������ΰ˻�") �̸� 
    ///  1. GridInspectionArea Upper-left �ε��� List�� ���
    ///  2. �ش��ε����� key������ �ϴ� Dictionary�� gridPartIdx ����
    /// </summary> 
    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC, ref Dictionary<string, IdxRCStruct> gridPartsIdxDic, ref string firstIdxStr)
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
                    {
                        isPlacable &= true;
                        IdxRCStruct idxRCStruct = new IdxRCStruct(r, c);

                        if (firstIdxStr == string.Empty)
                            firstIdxStr = idxRCStruct.ToString();

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
       ref Dictionary<string, IdxRCStruct> triggeredIdxDic,
       Grid grid, Puzzle touchingPZ)
    {
        if (grid == null || touchingPZ == null)
            return;
        bool isFound = false;
        triggeredIdxDic.Clear();
        //  string key = string.Empty;
        string firstpzTriggered = touchingPZ.ChildPZPartList[0].TriggeredGridPartIdxStr;
        //if (!idxsDic.ContainsKey(firstpzTriggered) && KeyBackup != firstpzTriggered)
        //{
        //    foreach (KeyValuePair<string, GridPart> kvp in grid.ChildGridPartDic)
        //    {
        //        if (kvp.Value.Data == 2)
        //            kvp.Value.Data = 0;
        //    }
        //}
        if (idxsDic.ContainsKey(firstpzTriggered))
        {
            isFound = true;
            //     key = firstpzTriggered;
            triggeredIdxDic = idxsDic[firstpzTriggered];
            //  Debug.Log($"Contains key: {key}, cnt: {_foundGridPartCntDic.Count}");
        }
        #region backup
        //foreach (KeyValuePair<string, Dictionary<string, IdxRCStruct>> kvp in idxsDic)
        //{
        //    _foundGridPartCntDic.Clear();
        //    key = kvp.Key;

        //    foreach (PZPart pzPart in touchingPZ.ChildPZPartList)
        //    {
        //        string triggeredGpStr = pzPart.TriggeredGridPartIdxStr;
        //        if (kvp.Value.ContainsKey(triggeredGpStr))
        //        {
        //            Util.CheckAndAddDictionary(_foundGridPartCntDic, triggeredGpStr, pzPart.name);
        //        }
        //        if (_foundGridPartCntDic.Count >= 2)
        //        {
        //            isFound = true;
        //            triggeredIdxDic = kvp.Value;
        //            Debug.Log($"Contains key: {key}, cnt: {_foundGridPartCntDic.Count}");
        //            break;
        //        }
        //    }
        //    if (isFound)
        //        break;
        //}
        #endregion

        if (isFound && KeyBackup != firstpzTriggered)
        {
            //if (_keyBackup != key)
            //{
            //    _keyBackup = key;
            MarkPlacableIdx(ref triggeredIdxDic, grid);
            KeyBackup = firstpzTriggered;
            Debug.Log("?????????????");
            //}
        }
        triggeredIdxDic.Clear();

    }
    /// <summary>
    /// Placable�� ���� �� TouchingGo�� �浹�� �� ���� ����
    /// </summary>
    private void MarkPlacableIdx(ref Dictionary<string, IdxRCStruct> dic, Grid grid)
    { 
        foreach (KeyValuePair<string, IdxRCStruct> kvp in dic)
        {
            grid.SetGridPartData(kvp.Value.IdxR, kvp.Value.IdxC, 2);
        }
    }
    public void MarkPlacableIdxReset(ref Dictionary<string, IdxRCStruct> triggeredIdxDic, Grid grid)
    {
        triggeredIdxDic.Clear();
        KeyBackup = string.Empty;

        foreach (KeyValuePair<string, GridPart> kvp in grid.ChildGridPartDic)
        {
            if (kvp.Value.Data == 2)
                kvp.Value.Data = 0;
        }
    }
    public void MarkPlacableIdxReset2(ref Dictionary<string, IdxRCStruct> triggeredIdxDic, Grid grid)
    { 
        foreach (KeyValuePair<string, GridPart> kvp in grid.ChildGridPartDic)
        {
            if (!triggeredIdxDic.ContainsKey(kvp.Key) && kvp.Value.Data == 2)
                kvp.Value.Data = 0;
        }
    }
} // end of class 