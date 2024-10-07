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
    /// PuzzlePlacableChecker ȣ���Ͽ� ��ü ���� ���� CheckPlacable �˻�
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

        // 1. Grid�� �ִ��� üũ
        //   if (!touchingPZ.IsInGrid) return false;

        // 2. Placable�� �ִ��� üũ 

        return false;

    }

    /// <summary>
    /// Touching Go�� CheckPlacable == true �� �� Drop���� ���� ���� �ۼ�
    /// (TODO) 6. completeManager ȣ�� - complete => grid update���ִ� Ŭ���� ȣ��
    /// </summary>
    public void PuzzlePlace()
    {
        // TODO 
        Debug.Log("Puzzle Place Process");
    }
} // end of class