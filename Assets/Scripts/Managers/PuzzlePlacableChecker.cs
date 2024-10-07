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
    /// Callback 매서드
    ///   1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
    ///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
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
            // # 해당 배열 내 Puzzle Go가 있고 Placable이면 true반환
            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                remainingPuzzleCnt++;

                if (!CheckPlacableThisPuzzle(grid, puzzle))
                    gameOverCheckNum++;
            }
            else // # 해당 배열 내 Puzzle Go 없음
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
            isGameOver = false; // # GameOver는 아님 

        if (!isGameOver)
            _gameOverCallback?.Invoke();
    }

    /// <summary>
    /// 해당 Grid에 해당 PUZZLE을 넣을 수 있는지 확인
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
    /// 퍼즐이 placable한 모든 퍼즐 (0,0)이 닿는 grid 인덱스 Dic에 담기 
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

            // # 그리드 모든 idx 체크
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    Dictionary<string, IdxRCStruct> gridPartsIdxDic = new Dictionary<string, IdxRCStruct>();
                    gridPartsIdxDic.Clear();

                    // # Grid-Puzzle영역 Puzzle매핑 검사 완료 후 true라면 => 해당 Grid영역가장초기 인덱스 List에 담기
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
    /// 해당 "grididx + 퍼즐 실질 idx 영역"(named "GridInspectionArea")에 
    /// "퍼즐데이터가 1이고 그리드데이터가 !=1"(named "퍼즐매핑검사") 이면 
    ///  1. GridInspectionArea Upper-left 인덱스 List에 담기
    ///  2. 해당인덱스를 key값으로 하는 Dictionary에 gridPartIdx 저장
    /// </summary> 
    private bool CheckMappingGridInspectionAreaAndPuzzle(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC, ref Dictionary<string, IdxRCStruct> gridPartsIdxDic, ref string firstIdxStr)
    {
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
                // # 이 안에서 griddata !=1 && puzzledata ==1인 곳  퍼즐 영역만큼  List에 담음
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
    /// - PuzzleTouchingGo 충돌 검사 결과를 받아서
    /// - 동시에 여러군데 부딪혔다면 제일 처음 부딪힌 한 곳의 소속 GridPart를 자료구조에 담아줌
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
    /// Placable한 영역 중 TouchingGo가 충돌한 곳 색상 변경
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