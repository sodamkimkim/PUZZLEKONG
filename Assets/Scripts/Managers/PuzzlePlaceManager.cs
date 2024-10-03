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
    public Dictionary<string, List<IdxRCStruct>> PlacableGridPartsListDic = new Dictionary<string, List<IdxRCStruct>>();// key - rowIdx,colIdx
    private void Awake()
    {
        _puzzlePlacableChecker = this.GetComponent<PuzzlePlacableChecker>();
        _gridManager = this.GetComponentInChildren<GridManager>();
        _puzzleManager = this.GetComponentInChildren<PuzzleManager>();
    }
    private void Start()
    {
        _gridManager.Iniit(CheckPlacable_AllRemaingPuzzles);
        _puzzleManager.Iniit(CheckPlacable_AllRemaingPuzzles);
    }
    /// <summary>
    /// PuzzlePlacableChecker ȣ���Ͽ� ��ü ���� ���� CheckPlacable �˻�
    /// </summary>
    public void CheckPlacable_AllRemaingPuzzles()
    {
        _puzzlePlacableChecker.CheckPlacable_AllRemainingPuzzles(_gridManager.Grid, _puzzleManager.PuzzleGoArr);
    }
    public void MarkPlacable(bool isPZMoving, Puzzle puzzle)
    {
        if (puzzle == null) return;
        _puzzlePlacableChecker.MarkPlacable(ref PlacableGridPartsListDic, isPZMoving, _gridManager.Grid, puzzle);
    }
    public bool CheckPlacable_TouchingPZ(Puzzle touchingPZ)
    {
        if (touchingPZ == null) return false;

        // 1. Grid�� �ִ��� üũ
        if (!touchingPZ.IsInGrid) return false;
         
        // 2. Placable�� �ִ��� üũ 

        return false;

    } 

    /// <summary>
    /// Touching Go�� CheckPlacable == true �� �� Drop���� ���� ���� �ۼ�
    /// (TODO) 6. completeManager ȣ�� - complete => grid update���ִ� Ŭ���� ȣ��
    /// </summary>
    public void PlacePuzzle()
    {
        // TODO 
        Debug.Log("Puzzle Place Process");
    }
} // end of class