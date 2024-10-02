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
    public struct IdxRCStruct
    {
        public int idxR { get; set; }
        public int idxC { get; set; }

        public IdxRCStruct(int idxR, int idxC)
        {
            this.idxR = idxR;
            this.idxC = idxC;
        }
    }
    private List<IdxRCStruct> _idxRcStructList = new List<IdxRCStruct>();
    private List<IdxRCStruct> _tempIdxRCList = new List<IdxRCStruct>();
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
        _idxRcStructList.Clear();
        _tempIdxRCList.Clear();
        Debug.Log($"MarkPlace");
        if (isPZMoving)
        {
            grid.BackupData = grid.Data;
            // �׸��� ��� idx üũ
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
            {
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    CheckMappingGridAndPuzzleIdx(grid, puzzle, grIdxR, grIdxC);
                }
            }
        }
        else
            grid.Data = grid.BackupData;
    }
    /// <summary>
    /// �ش� "grididx + ���� ���� idx ����(���� 'Grid-Puzzle����'�̶� Ī��)"�� �������Ͱ� 1�̰�
    /// �׸��嵥���Ͱ� !=1 �̸�(���� '������ΰ˻�'�� Ī��)
    /// ���� ������ŭ  ���� ���� & Grid Idx List�� ���
    /// </summary>
    /// <param name="grIdxR"></param>
    /// <param name="grIdxC"></param>
    private void CheckMappingGridAndPuzzleIdx(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
    {
        // # �˻��� grid���� ����
        int[] idxRangeR = new int[2] { grIdxR, grIdxR + puzzle.LastIdx_rc[0] };
        int[] idxRangeC = new int[2] { grIdxC, grIdxC + puzzle.LastIdx_rc[1] };

        Debug.Log(grid.Data.GetLength(0));

        // # grid idx�� ���� ������ ���� ���ϸ� return
        if (idxRangeR[1] > grid.Data.GetLength(0) - 1) return;
        if (idxRangeC[1] > grid.Data.GetLength(1) - 1) return;

        // # �ش� Grid������ ���� ���� �������� �˻�
        bool isPlacable = true;

        _tempIdxRCList.Clear();
        int puzzleIdxR = 0;
        for (int i = idxRangeR[0]; i <= idxRangeR[1]; i++)
        {
            int puzzleIdxC = 0;
            for (int j = idxRangeC[0]; j <= idxRangeC[1]; j++)
            {
                // # �� �ȿ��� griddata !=1 && puzzledata ==1�� ��  ���� ������ŭ  ���� ����
                if (grid.Data[i, j] == 1) // grid�� �ش� �ε����� ������ �������� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) // => ok
                    {
                        isPlacable &= true;

                    }
                    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no
                    {
                        isPlacable &= false;
                    }
                }
                else // grid.Data[i, j] == 0 || 2  // grid�� �ش� �ε����� ������ �������� ���� ��
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) // => ok
                    {
                        isPlacable &= true;
                        _tempIdxRCList.Add(new IdxRCStruct(i, j));
                    }
                    else // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) => ok
                    {
                        isPlacable &= true;
                    }
                }

                puzzleIdxC++;
            }
            puzzleIdxR++;
        }

        // # Grid-Puzzle���� Puzzle���� �˻� �Ϸ� �� true��� 1. �ش� Grid���������ʱ� �ε��� List�� ���, 2. ������ο��� ���� ����!
        if (isPlacable)
        {
            _idxRcStructList.Add(new IdxRCStruct(grIdxR, grIdxC));
            foreach (IdxRCStruct idxRC in _tempIdxRCList)
            {
                grid.SetGridPartData(idxRC.idxR, idxRC.idxC, 2);
            }
        }
    } 
} // end of class 