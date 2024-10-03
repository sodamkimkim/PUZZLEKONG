using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// : 퍼즐 세개 중 남은 것 & 현재 선택한 퍼즐 Placable Check 
/// (TODO) 1. selectPuzzle그리드 영역내인지 check => T|F
/// (TODO) 2. 각 퍼즐 Placable == false => 비활성화 : 활성화
/// (DONE) 3. 모두 Placable == false => GameOver
/// - check는 특정시간 반복이 아니라 이벤트 종속
///   ㄴ
///   1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
///   
/// return ) 
/// - Grid영역 내 T|F
/// - 각 퍼즐 Placable T|F & 각 퍼즐 Placable Grid자리 색깔변경
/// - GameOver여부 T|F
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
    /// Callback 매서드
    ///   1. puzzle 3개 새로 생성됐을 때(PuzzleManager),
    ///   2. 퍼즐 그리드에 드랍 됐을 때 (complete되거나 안되거나 어쩄뜬 gridArr는 refresh 됨 ) (GridManager) 
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    public void CheckPlacable_AllRemainingPuzzles(Grid grid, GameObject[] puzzleGoArr)
    {// TODO 
        if (IsGameOver(grid, puzzleGoArr))
            Debug.Log("GameOver");
    }

    /// <summary>
    ///  - 배열내 모든 Puzzle Go Placable == false => GameOver
    /// </summary>
    /// <param name="puzzleGoArr"></param>
    /// <returns> T: GameOver, F: NotGameOver </returns>
    private bool IsGameOver(Grid grid, GameObject[] puzzleGoArr)
    {
        int puzzleCnt = 0;
        int gameOverCheckNum = 0;

        foreach (GameObject go in puzzleGoArr)
        {
            // 해당 배열 내 Puzzle Go가 있고 Placable이면 true반환
            Puzzle puzzle = go.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                puzzleCnt++;

                if (CheckPlacable_InThisGrid(grid, puzzle))
                    gameOverCheckNum++;
            }
            else // 해당 배열 내 Puzzle Go 없음
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
            return false; //GameOver는 아님 
    }

    /// <summary>
    /// 해당 Grid에 PUZZLE을 넣을 수 있는지 확인
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
    /// 1. puzzle의 placable한 인덱스 모든 곳 찾기
    /// 2. 그 중 현재 퍼즐 위치와 가장 가까운 곳 색상표시 (타매서드 호출)
    /// 
    /// =>> 퍼즐이 placable한 모든 퍼즐 (0,0)이 닿는 grid 인덱스 List에 담기 및 퍼즐 매핑영역 색상변경
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

            // # 그리드 모든 idx 체크
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
    /// 해당 "grididx + 퍼즐 실질 idx 영역"(named "GridInspectionArea")에 
    /// "퍼즐데이터가 1이고 그리드데이터가 !=1"(named "퍼즐매핑검사") 이면 
    ///  1. GridInspectionArea Upper-left 인덱스 List에 담기
    ///  2. 해당인덱스를 key값으로 하는 Dictionary에 gridPartIdx 저장
    /// </summary> 
    private void CheckMappingGridAndPuzzleIdx(Grid grid, Puzzle puzzle, int grIdxR, int grIdxC)
    {
        // # 검사할 Grid영역 설정 (GridInspectionArea)
        int[] idxRangeR = new int[2] { grIdxR, grIdxR + puzzle.LastIdx_rc[0] };
        int[] idxRangeC = new int[2] { grIdxC, grIdxC + puzzle.LastIdx_rc[1] };

        // # grid idx가 퍼즐 영역을 담지 못하면 return
        if (idxRangeR[1] > grid.Data.GetLength(0) - 1) return;
        if (idxRangeC[1] > grid.Data.GetLength(1) - 1) return;

        // # 해당 Grid영역에 퍼즐 매핑 가능한지 검사
        bool isPlacable = true;

        List<IdxRCStruct> gridPartIdxList = new List<IdxRCStruct>();
        gridPartIdxList.Clear();
        int puzzleIdxR = 0;
        for (int i = idxRangeR[0]; i <= idxRangeR[1]; i++)
        {
            int puzzleIdxC = 0;
            for (int j = idxRangeC[0]; j <= idxRangeC[1]; j++)
            {
                // # 이 안에서 griddata !=1 && puzzledata ==1인 곳  퍼즐 영역만큼  List에 담음
                if (grid.Data[i, j] == 1) // grid의 해당 인덱스에 퍼즐이 놓여있을 떄
                {
                    if (puzzle.Data[puzzleIdxR, puzzleIdxC] == 0) // => ok 
                        isPlacable &= true;
                    else  // (puzzle.Data[puzzleIdxR, puzzleIdxC] == 1) => no 
                        isPlacable &= false;
                }
                else // grid.Data[i, j] == 0 || 2  // grid의 해당 인덱스에 퍼즐이 놓여있지 않을 때
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

        // # Grid-Puzzle영역 Puzzle매핑 검사 완료 후 true라면 1. 해당 Grid영역가장초기 인덱스 List에 담기, 2. 퍼즐매핑영역 색상 변경!
        if (isPlacable)
        {
            Util.CheckAndAddDictionary(_placableGridPartsListDic, new IdxRCStruct(grIdxR, grIdxC).ToString(), gridPartIdxList);

            foreach (KeyValuePair<string, List<IdxRCStruct>> kvp in _placableGridPartsListDic)
                foreach (IdxRCStruct idx in kvp.Value)
                    grid.SetGridPartData(idx.IdxR, idx.IdxC, 2);
        }
    }
} // end of class 