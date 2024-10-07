using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public Dictionary<string, Dictionary<string, IdxRCStruct>> PlacableGridPartsListDic = new Dictionary<string, Dictionary<string, IdxRCStruct>>();// key - rowIdx,colIdx
    private void Awake()
    {
        _puzzlePlacableChecker = this.GetComponent<PuzzlePlacableChecker>();
        _gridManager = this.GetComponentInChildren<GridManager>();
        _puzzleManager = this.GetComponentInChildren<PuzzleManager>();

        _gridManager.Iniit(CheckPlacableAllRemaingPuzzles);
        _puzzleManager.Iniit(CheckPlacableAllRemaingPuzzles);
    }
    /// <summary>
    /// PuzzlePlacableChecker 호출하여 전체 남은 퍼즐 CheckPlacable 검사
    /// </summary>
    public void CheckPlacableAllRemaingPuzzles()
    {
        _puzzlePlacableChecker.CheckPlacableAllRemainingPuzzles(_gridManager.Grid, _puzzleManager.PuzzleGoArr);
    }
    public void GetIdxDic(bool needFunction, Puzzle puzzle)
    {
        if (puzzle == null) return;
        _puzzlePlacableChecker.GetPlacableIdxs(ref PlacableGridPartsListDic, needFunction, _gridManager.Grid, puzzle);
    }
    public void GetTriggeredPlacableIdx( Puzzle puzzle)
    {
        _puzzlePlacableChecker.GetTriggeredPlacableIdx(_gridManager.Grid, puzzle);
    }
    public void MarkPlacableIdxReset()
    {
        _puzzlePlacableChecker.MarkPlacableIdxReset(_gridManager.Grid);
    } 
    public bool CheckPlacable(Puzzle touchingPZ)
    {
        if (touchingPZ == null) return false;

        // 1. Grid에 있는지 체크
        //   if (!touchingPZ.IsInGrid) return false;

        // 2. Placable에 있는지 체크 

        return false;

    }

    /// <summary>
    /// Touching Go가 CheckPlacable == true 된 후 Drop했을 때의 로직 작성
    /// (TODO) 6. completeManager 호출 - complete => grid update해주는 클래스 호출
    /// </summary>
    public void PuzzlePlace()
    {
        // TODO 
        Debug.Log("Puzzle Place Process");
    }
} // end of class