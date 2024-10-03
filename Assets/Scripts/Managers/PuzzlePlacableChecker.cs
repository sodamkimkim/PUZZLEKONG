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
    private string _pzNameBackupStr = string.Empty;
    /// <summary>
    /// Callback �ż���
    ///   1. puzzle 3�� ���� �������� ��(PuzzleManager),
    ///   2. ���� �׸��忡 ��� ���� �� (complete�ǰų� �ȵǰų� ��� gridArr�� refresh �� ) (GridManager) 
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable_AllRemainingPuzzles(Grid grid, GameObject[] puzzleGoArr)
    {// TODO 
        if (grid == null || puzzleGoArr[0] == null) return;

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

                if (!CheckPlacable(grid, puzzle))
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
    private bool CheckPlacable(Grid grid, Puzzle puzzle)
    {
        if (!puzzle.IsInGrid) return false;

        Dictionary<string, List<IdxRCStruct>> tempDic = new Dictionary<string, List<IdxRCStruct>>();
        GetPlacableIdx(ref tempDic, true, grid, puzzle);

        if (tempDic.Count > 0) return true;
        else return false;
    }

    /// <summary>
    /// 1. puzzle�� placable�� �ε��� ��� �� ã��
    /// 2. �� �� ���� ���� ��ġ�� ���� ����� �� ����ǥ�� (Ÿ�ż��� ȣ��)
    /// 
    /// =>> ������ placable�� ��� ���� (0,0)�� ��� grid �ε��� List�� ��� �� ���� ���ο��� ���󺯰�
    /// </summary>
    /// <param name="needFunction"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void GetPlacableIdx(ref Dictionary<string, List<IdxRCStruct>> dic, bool needFunction, Grid grid, Puzzle puzzle)
    {
        if (needFunction)
        {
            if (_pzNameBackupStr == puzzle.name) return;

            dic.Clear();
            _pzNameBackupStr = puzzle.name;
            grid.BackupData = grid.Data;

            // # �׸��� ��� idx üũ
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                    CheckMappingGridAndPuzzleIdx(ref dic, grid, puzzle, grIdxR, grIdxC);
        }
        else
        {
            dic.Clear();
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
    private void CheckMappingGridAndPuzzleIdx(ref Dictionary<string, List<IdxRCStruct>> dic, Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
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
            Util.CheckAndAddDictionary(dic, new IdxRCStruct(grIdxR, grIdxC).ToString(), gridPartIdxList);

#if DEBUGING
            foreach (KeyValuePair<string, List<IdxRCStruct>> kvp in dic)
                foreach (IdxRCStruct idx in kvp.Value)
                    grid.SetGridPartData(idx.IdxR, idx.IdxC, 2);
#endif
        }
    }
} // end of class 