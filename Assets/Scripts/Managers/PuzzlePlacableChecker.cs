using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
    public struct IdxRCStruct
    {
        public int IdxR { get; set; }
        public int IdxC { get; set; }

        public IdxRCStruct(int idxR, int idxC)
        {
            this.IdxR = idxR;
            this.IdxC = idxC;
        }
        public override string ToString() => $"{IdxR},{IdxC}";
        // end of structure
    }
     
    private Dictionary<string, List<IdxRCStruct>> _placableGridPartsListDic = new Dictionary<string, List<IdxRCStruct>>();// key - rowIdx,colIdx
    private string _pzNameBackupStr = string.Empty;
    /// <summary>
    /// Callback �ż���
    ///   1. puzzle 3�� ���� �������� ��(PuzzleManager),
    ///   2. ���� �׸��忡 ��� ���� �� (complete�ǰų� �ȵǰų� ��� gridArr�� refresh �� ) (GridManager) 
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable_AllRemainingPuzzles(Grid grid, GameObject[] puzzleGoArr)
    {// TODO 
        if (IsGameOver(grid, puzzleGoArr))
            Debug.Log("GameOver");
    }

    /// <summary>
    ///  - �迭�� ��� Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsGameOver(Grid grid, GameObject[] puzzleGoArr)
    {
        int puzzleCnt = 0;
        int gameOverCheckNum = 0;

        foreach (GameObject go in puzzleGoArr)
        {
            // �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ
            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                puzzleCnt++;

                if (CheckPlacable_InThisGrid(grid, puzzle))
                    gameOverCheckNum++;
            }
            else // �ش� �迭 �� Puzzle Go ����
                continue;
        }

        if (puzzleCnt == 0)
        {
            // TODO
            Debug.Log("StageComplete");
            return false;
        }
        else if (puzzleCnt != 0 && puzzleCnt == gameOverCheckNum)
            return true; // GameOver 
        else
            return false; //GameOver�� �ƴ� 
    }

    /// <summary>
    /// �ش� Grid�� PUZZLE�� ���� �� �ִ��� Ȯ��
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    /// <returns></returns>
    private bool CheckPlacable_InThisGrid(Grid grid, Puzzle puzzle)
    {
        if (!puzzle.IsInGrid) return false;
        // TODO 

        return false;
    }

    /// <summary>
    /// 1. puzzle�� placable�� �ε��� ��� �� ã��
    /// 2. �� �� ���� ���� ��ġ�� ���� ����� �� ����ǥ�� (Ÿ�ż��� ȣ��)
    /// 
    /// =>> ������ placable�� ��� ���� (0,0)�� ��� grid �ε��� List�� ��� �� ���� ���ο��� ���󺯰�
    /// </summary>
    /// <param name="isPZMoving"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void MarkPlacable(bool isPZMoving, Grid grid, Puzzle puzzle)
    {
        if (isPZMoving)
        {
            if (_pzNameBackupStr == puzzle.name) return;

            _placableGridPartsListDic.Clear();
            _pzNameBackupStr = puzzle.name;
            grid.BackupData = grid.Data;

            // # �׸��� ��� idx üũ
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                    CheckMappingGridAndPuzzleIdx(grid, puzzle, grIdxR, grIdxC);
        }
        else
        {
            _placableGridPartsListDic.Clear();
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
    private void CheckMappingGridAndPuzzleIdx(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
    {
        // # �˻��� Grid���� ���� (GridInspectionArea)
        int[] idxRangeR = new int[2] { grIdxR, grIdxR + puzzle.LastIdx_rc[0] };
        int[] idxRangeC = new int[2] { grIdxC, grIdxC + puzzle.LastIdx_rc[1] };

        // # grid idx�� ���� ������ ���� ���ϸ� return
        if (idxRangeR[1] > grid.Data.GetLength(0) - 1) return;
        if (idxRangeC[1] > grid.Data.GetLength(1) - 1) return;

        // # �ش� Grid������ ���� ���� �������� �˻�
        bool isPlacable = true;

        List<IdxRCStruct> gridPartIdxList = new List<IdxRCStruct>();
        gridPartIdxList.Clear();
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
                        gridPartIdxList.Add(new IdxRCStruct(i, j));
                    }
                    else // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) => ok 
                        isPlacable &= true;
                }
                puzzleIdxC++;
            }
            puzzleIdxR++;
        }

        // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� 1. �ش� Grid���������ʱ� �ε��� List�� ���, 2. ������ο��� ���� ����!
        if (isPlacable)
        {
            Util.CheckAndAddDictionary(_placableGridPartsListDic, new IdxRCStruct(grIdxR, grIdxC).ToString(), gridPartIdxList);

            foreach (KeyValuePair<string, List<IdxRCStruct>> kvp in _placableGridPartsListDic)
                foreach (IdxRCStruct idx in kvp.Value)
                    grid.SetGridPartData(idx.IdxR, idx.IdxC, 2);
        }
    }
} // end of class 