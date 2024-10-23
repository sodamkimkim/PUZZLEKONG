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
    /// PuzzlePlacableChecker ȣ���Ͽ� ��ü ���� ���� CheckPlacable �˻�
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