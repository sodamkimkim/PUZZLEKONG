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
    [SerializeField]
    private GridManager _gridManager = null;
    [SerializeField]
    private PuzzleManager _puzzleManager = null;
    [SerializeField]
    private CompleteManager _completeManager = null;

    [SerializeField]
    public PuzzlePlacableChecker PuzzlePlacableChecker = null;

    private void Awake()
    {
        _gridManager.Iniit(CheckPlacableAllRemainingPuzzles);
        _puzzleManager.Iniit(CheckPlacableAllRemainingPuzzles);
    }
    /// <summary>
    /// PuzzlePlacableChecker 호출하여 전체 남은 퍼즐 CheckPlacable 검사
    /// </summary>
    public void CheckPlacableAllRemainingPuzzles()
    {
        PuzzlePlacableChecker.CheckPlacableAllRemainingPuzzles(_gridManager.Grid, _puzzleManager.PuzzleGoArr);
    }
    public bool MarkPlacable(Puzzle touchingPZ)
    {
        return PuzzlePlacableChecker.MarkPlacable(_gridManager.Grid, touchingPZ);
    }
    public void MarkPlacableReset()
    {
        PuzzlePlacableChecker.MarkPlacableReset(_gridManager.Grid);
    }

    public delegate void SetTouchEndPuzzleReturn();
    public void PlacePuzzle(Puzzle touchingPZ, SetTouchEndPuzzleReturn SetTouchEndPuzzleReturnCallback)
    {
        bool isPlacePzSuccess = false;
        if (touchingPZ == null || _gridManager.Grid == null) isPlacePzSuccess = false;

        // #
        isPlacePzSuccess = PuzzlePlacableChecker.PuzzlePlace(_gridManager.Grid, touchingPZ);
        if (!isPlacePzSuccess)
            SetTouchEndPuzzleReturnCallback?.Invoke();
        else
        {
            _completeManager.Complete(CheckPlacableAllRemainingPuzzles);
            //PuzzlePlacableChecker.CheckPlacableAllRemainingPuzzles(_gridManager.Grid, _puzzleManager.PuzzleGoArr);
        }
    }
} // end of class