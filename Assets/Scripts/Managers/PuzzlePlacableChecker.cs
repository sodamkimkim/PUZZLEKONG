using UnityEngine;

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
    /// <summary>
    /// Callback �ż���
    ///   1. puzzle 3�� ���� �������� ��(PuzzleManager),
    ///   2. ���� �׸��忡 ��� ���� �� (complete�ǰų� �ȵǰų� ��� gridArr�� refresh �� ) (GridManager) 
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable_AllRemainingPuzzles(Grid grid, GameObject[] puzzleGoArr)
    {// TODO 
        if (IsCheckGameOver(grid, puzzleGoArr))
            Debug.Log("GameOver");
    }

    /// <summary>
    ///  - �迭�� ��� Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsCheckGameOver(Grid grid, GameObject[] puzzleGoArr)
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

                if (CheckPlacableInThisGrid(grid, puzzle))
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
    private bool CheckPlacableInThisGrid(Grid grid, Puzzle puzzle)
    {
        if (!puzzle.IsInGrid) return false;
        // TODO 

        return false;
    }
    /// <summary>
    /// puzzle�� ���� ��ġ�� placable���� �Ǻ��ϰ� mark
    /// </summary>
    /// <param name="isPZMoving"></param>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    public void MarkPlacable(bool isPZMoving, Grid grid, Puzzle puzzle)
    {
        if (isPZMoving)
        {
            grid.BackupData = grid.Data;

            // ������ �ٸ� ����� ��ġ���� Ȯ��
            for (int grIdxR = 0; grIdxR < grid.Data.GetLength(0); grIdxR++)
            {
                for (int grIdxC = 0; grIdxC < grid.Data.GetLength(1); grIdxC++)
                {
                    // Grid������ check
                    int lastRowIdx = grIdxR + puzzle.LastIdx_rc[0];
                    int lastColIdx = grIdxC + puzzle.LastIdx_rc[1];
                    if (lastRowIdx > grid.Data.GetLength(0) - 1 || lastColIdx > grid.Data.GetLength(1) - 1)
                    {
                        Debug.Log($"CheckIdx : {lastRowIdx}, {lastColIdx}");
                        continue;
                    }
                    else
                    {
                        if (grid.Data[grIdxR, grIdxC] == 0)
                        {
                            grid.SetGridPartDataRange(grIdxR, grIdxC, grIdxR, grIdxC, 0, 2); // �׸��� ���θ� idx ���󺯰�
                            CanPlacePuzzle(grid, grIdxR, grIdxC, puzzle);
                        }
                    }
                }
            }
        }
        else
            grid.Data = grid.BackupData;

    }

    /// <summary>
    /// Grid ���� �� Placable�� Idx && puzzle == 1�̸鼭 grid[r,c] !=1 ã�� �ż���
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridIdxR"></param>
    /// <param name="gridIdxC"></param>
    /// <param name="puzzle"></param>
    private void CanPlacePuzzle(Grid grid, int gridIdxR, int gridIdxC, Puzzle puzzle)
    {
        //int endIdxR = gridIdxR + puzzle.GetRealLength_rc[0];
        //int endIdxC = gridIdxC + puzzle.GetRealLength_rc[1];

        //// grid[r,c] + ���� ���� �� ��� �����Ͱ� 0�̿�����
        //if (grid.CheckPlacable(gridIdxR, gridIdxC, endIdxR, endIdxC))
        //    grid.SetGridPartDataRange(gridIdxR, gridIdxC, endIdxR, endIdxC, 2, 3);
    }
} // end of class 