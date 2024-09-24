using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 퍼즐 세개 중 남은 것 Placable Check 
/// - 각 퍼즐 Placable == false => 비활성화 : 활성화
/// - 모두 Placable == false => GameOver
/// - check는 0.5초마다
/// 
/// return ) 
/// - 각 퍼즐 Placable T|F & 각 퍼즐 Placable Grid자리
/// - GameOver여부 T|F
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
    ///  - 배열내 모든 Puzzle Go Placable == false => GameOver
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
            // 해당 배열 내 Puzzle Go가 있고 Placable이면 true반환
            if (go.GetComponent<Puzzle>() != null)
            {
                puzzleCnt++;
                Puzzle puzzle = go.GetComponent<Puzzle>();
                if (puzzle.Placable == false)
                    gameOverCheckNum++;
            }
            else // 해당 배열 내 Puzzle Go 없음
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
            //GameOver는 아님
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
