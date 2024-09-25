using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 퍼즐 세개 중 남은 것 Placable Check 
/// - 각 퍼즐 Placable == false => 비활성화 : 활성화
/// - 모두 Placable == false => GameOver
/// - check는 특정시간 반복이 아니라 이벤트 종속
///   ㄴ1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
/// 
/// return ) 
/// - 각 퍼즐 Placable T|F & 각 퍼즐 Placable Grid자리 색깔변경
/// - GameOver여부 T|F
/// 
/// </summary>
public class PuzzlePlacableChecker : MonoBehaviour
{
    private GridManager _gridManager = null;
    private PuzzleManager _puzzleManager = null; 
    private void Awake()
    {
        _gridManager = this.GetComponentInChildren<GridManager>();
        _puzzleManager = this.GetComponentInChildren<PuzzleManager>(); 
    }
    private void Start()
    {
        _gridManager.Iniit(CheckPlacable);
        _puzzleManager.Iniit(CheckPlacable); 
    }
    /// <summary>
    /// 배열의 각 GameObject Placable Check
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable()
    {
        if (IsCheckGameOver(_gridManager.Grid.Data, _puzzleManager.PuzzleGoArr))
            GameOver();
    }
 
    /// <summary>
    ///  - 배열내 모든 Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsCheckGameOver(int[,] grid, GameObject[] puzzleGoArr)
    {
        int puzzleCnt = 0;
        int gameOverCheckNum = 0;

        foreach (GameObject go in puzzleGoArr)
        {
            // 해당 배열 내 Puzzle Go가 있고 Placable이면 true반환
            if (go.GetComponent<Puzzle>() != null)
            {
                puzzleCnt++;
                
                if (CheckPlacable(grid, go.GetComponent<Puzzle>()))
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
            return true; // GameOver 
        else
            return false; //GameOver는 아님 
    }
    private bool CheckPlacable(int[,] grid, Puzzle puzzle)
    {
        // TODO
        return true;
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
