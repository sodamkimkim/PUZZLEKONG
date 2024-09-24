using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. ���� ���� �� ���� �� Placable Check 
/// - �� ���� Placable == false => ��Ȱ��ȭ : Ȱ��ȭ
/// - ��� Placable == false => GameOver
/// - check�� 0.5�ʸ���
/// 
/// return ) 
/// - �� ���� Placable T|F & �� ���� Placable Grid�ڸ�
/// - GameOver���� T|F
/// 
/// </summary>
public class PuzzlePlacableChecker : MonoBehaviour
{
    private bool _isStartRepeating = false;
    private Action _repeatingAction;
    private float _repeatTime = 0.5f;
    private float _timeElapsed = 0f;
    private void Update()
    {
        if (_isStartRepeating == false) return;

        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _repeatTime)
        {
            _repeatingAction();
            _timeElapsed = 0f;
        }
    }
    public void StartCheck(bool isStartRepeating, GameObject[] puzzleArr)
    {
        _repeatingAction += () => Check(puzzleArr);
        _isStartRepeating = isStartRepeating;
    }
    private void Check(GameObject[] puzzleArr)
    {

        if (IsCheckGameOver(puzzleArr))
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
        //List<bool> isGameOvercheckList = new List<bool>();
        foreach (GameObject go in puzzleGoArr)
        {
            // �ش� �迭 �� Puzzle Go�� �ְ� Placable�̸� true��ȯ
            if (go.GetComponent<Puzzle>() != null)
            {
                puzzleCnt++;
                Puzzle puzzle = go.GetComponent<Puzzle>();
                if (puzzle.Placable == false)
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
        {
            // GameOver
            return true;
        }
        else
        {
            //GameOver�� �ƴ�
            return false;
        }
    }

    private void StageComplete()
    {
        // TODO -  Stage Complete Process
    }

    private void CheckPlacable(int[,] puzzleArr)
    {

    }
    private void GameOver()
    {
        // TODO - GameOverProcess
    }
} // end of class
