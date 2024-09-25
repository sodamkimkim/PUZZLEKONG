using System;
using System.Collections.Generic;
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
    {
        if (IsCheckGameOver(grid.Data, puzzleGoArr))
            GameOver();
    }
 
    /// <summary>
    ///  - �迭�� ��� Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsCheckGameOver(int[,] grid, GameObject[] puzzleGoArr)
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
                
                if (CheckPlacable(grid, puzzle))
                    gameOverCheckNum++;
            }
            else // �ش� �迭 �� Puzzle Go ����
                continue;
        }

        if (puzzleCnt == 0)
        {
            StageComplete();
            return false;
        }
        else if (puzzleCnt != 0 && puzzleCnt == gameOverCheckNum)
            return true; // GameOver 
        else
            return false; //GameOver�� �ƴ� 
    }

    /// <summary>
    /// Puzzle�� Grid �ȿ� �ִ��� Check
    /// PuzzlePlacer�� ���� �������� �� �� ȣ��
    /// </summary>
    /// <returns></returns>
    public bool CheckPuzzleInGrid()
    {
        // TODO

        return true;
    }
    /// <summary>
    /// �ش� Grid�� PUZZLE�� ���� �� �ִ��� Ȯ��
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="puzzle"></param>
    /// <returns></returns>
    private bool CheckPlacable(int[,] grid, Puzzle puzzle)
    {
        // TODO
        return true;
    }
    /// <summary>
    /// Stage Complete Process
    /// </summary>
    private void StageComplete()
    {
        // TODO -  Stage Complete Process
        Debug.LogError("StageComplete");
    }

    /// <summary>
    /// GameOver Process
    /// </summary>
    private void GameOver()
    {
        // TODO - GameOverProcess
        Debug.LogError("GameOver");
    }
} // end of class
