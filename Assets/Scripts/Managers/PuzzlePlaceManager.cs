using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// : �÷��̾ ������ ���� Place ���ִ� Ŭ����
/// - Placable check �� �����ͼ� ���� ���� �� Place 
/// 
/// (TODO) 1. GameOver F ���� üũ
/// (TODO) 2. ������ ���� Ȱ��ȭ���� ���� üũ
/// (TODO) 3. ������ ���� �׸��� �� ��ġ ���� üũ
/// (TODO) 4. completeManager ȣ�� - completable check 
/// (TODO) 5. ������ ���� Placable ��ġ�� drop
/// (TODO) 6. completeManager ȣ�� - complete => grid update���ִ� Ŭ���� ȣ��
/// </summary>
public class PuzzlePlaceManager : MonoBehaviour
{
    private PuzzlePlacableChecker _puzzlePlacableChecker = null;
    private GridManager _gridManager = null;
    private PuzzleManager _puzzleManager = null;
    private void Awake()
    {
        _puzzlePlacableChecker = this.GetComponent<PuzzlePlacableChecker>();
        _gridManager = this.GetComponentInChildren<GridManager>();
        _puzzleManager = this.GetComponentInChildren<PuzzleManager>();

        _gridManager.Iniit(CheckPlacableAllRemaingPuzzles);
        _puzzleManager.Iniit(CheckPlacableAllRemaingPuzzles);
    }
    /// <summary>
    /// PuzzlePlacableChecker ȣ���Ͽ� ��ü ���� ���� CheckPlacable �˻�
    /// </summary>
    public void CheckPlacableAllRemaingPuzzles()
    {
        _puzzlePlacableChecker.CheckPlacableAllRemainingPuzzles(_gridManager.Grid, _puzzleManager.PuzzleGoArr);
    }
    public void GetTriggeredPlacableIdx(Puzzle touchingPZ)
    {
        _puzzlePlacableChecker.MarkPlacable(_gridManager.Grid, touchingPZ);
    }
    public void MarkPlacableIdxReset()
    {
        _puzzlePlacableChecker.MarkPlacableIdxReset(_gridManager.Grid);
    }

    public delegate void SetTouchEndPuzzleReturn();
    public void PlacePuzzle(Puzzle touchingPZ, SetTouchEndPuzzleReturn SetTouchEndPuzzleReturnCallback)
    {
        bool isPlacePzSuccess = false;
        if (touchingPZ == null || _gridManager.Grid == null) isPlacePzSuccess = false;

        // #
        isPlacePzSuccess = _puzzlePlacableChecker.PuzzlePlace(_gridManager.Grid, touchingPZ);
        if (!isPlacePzSuccess)
            SetTouchEndPuzzleReturnCallback?.Invoke();
    } 
} // end of class