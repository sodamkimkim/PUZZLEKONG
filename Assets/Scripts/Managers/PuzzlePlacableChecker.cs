using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. ���� ���� �� ���� �� Placable Check 
/// - �� ���� Placable == false => ��Ȱ��ȭ : Ȱ��ȭ
/// - ��� Placable == false => GameOver
/// - check�� Ư���ð� �ݺ��� �ƴ϶� �̺�Ʈ ����
///   ��1. puzzle 3�� ���� �������� ��(PuzzleManager),
///   2. ���� �׸��忡 ��� ���� ��(puzzlePlacer),
///   3. puzzle complete�Ǿ gridarr reset���� ��(GridManager) 
/// 
/// return ) 
/// - �� ���� Placable T|F & �� ���� Placable Grid�ڸ� ���򺯰�
/// - GameOver���� T|F
/// 
/// </summary>
public class PuzzlePlacableChecker : MonoBehaviour
{
    private GridManager _gridManager = null;
    private PuzzleManager _puzzleManager = null;
    private PuzzlePlacer _puzzlePlacer = null;
    private void Awake()
    {
        _gridManager = this.GetComponentInChildren<GridManager>();
        _puzzleManager = this.GetComponentInChildren<PuzzleManager>();
        _puzzlePlacer = this.GetComponentInChildren<PuzzlePlacer>();
    }
    private void Start()
    {
        _gridManager.Iniit(CheckPlacable);
        _puzzleManager.Iniit(CheckPlacable);
        _puzzlePlacer.Iniit(CheckPlacable);
    }
    /// <summary>
    /// �迭�� �� GameObject Placable Check
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable()
    {
        if (IsCheckGameOver(_puzzleManager.PuzzleGoArr))
            GameOver();
    }
 
    /// <summary>
    ///  - �迭�� ��� Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsCheckGameOver(GameObject[] puzzleGoArr)
    {
        int puzzleCnt = 0;
        int gameOverCheckNum = 0;

        foreach (GameObject go in puzzleGoArr)
        {
            // �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ
            if (go.GetComponent<Puzzle>() != null)
            {
                puzzleCnt++;
                Puzzle puzzle = go.GetComponent<Puzzle>();
                if (puzzle.CheckPlacable(_gridManager.NowGridArr))
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

    private void StageComplete()
    {
        // TODO -  Stage Complete Process
        Debug.LogError("StageComplete");
    }


    private void GameOver()
    {
        // TODO - GameOverProcess
        Debug.LogError("GameOver");
    }
} // end of class
