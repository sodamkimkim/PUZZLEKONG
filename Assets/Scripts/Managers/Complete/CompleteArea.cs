using System.Collections;
using UnityEngine;

/// <summary>
/// Grid Complete여부 3*3 Area 9개 검사
/// </summary>
public class CompleteArea : MonoBehaviour
{
    public void MarkCompletable(Grid grid, int[,] gridDataSync, System.Action resetFunction)
    {
        int areaRLen = 3;
        int areaCLen = 3;

        for (int areaRIdx = 0; areaRIdx < areaRLen; areaRIdx++)
            for (int areaCIdx = 0; areaCIdx < areaCLen; areaCIdx++)
                if (CheckArea(areaRIdx, areaCIdx, grid, gridDataSync, Factor.Placable))
                {
                    for (int idxR = areaRIdx * 3; idxR < areaRIdx * 3 + 3; idxR++)
                        for (int idxC = areaCIdx * 3; idxC < areaCIdx * 3 + 3; idxC++)
                        {
                            if (gridDataSync[idxR, idxC] == Factor.HasPuzzle)
                                grid.SetDataIdx(idxR, idxC, Factor.Completable);
                        }
                }
    }
    public delegate void CompleteEffect(Vector3 worldPos, MonoBehaviour callerMono);
    /// <summary>
    /// 모든 9개영역 조사
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridDataSync"></param>
    public int Complete(Grid grid, int[,] gridDataSync, System.Action completeCallback, CompleteEffect completeEffectCallback)
    {
        int areaRLen = 3;
        int areaCLen = 3;
        int comboCnt = 0;

        for (int areaRIdx = 0; areaRIdx < areaRLen; areaRIdx++)
            for (int areaCIdx = 0; areaCIdx < areaCLen; areaCIdx++)
                if (CheckArea(areaRIdx, areaCIdx, grid, gridDataSync, Factor.Completable))
                {
                    comboCnt++;
                    StartCoroutine(CompleteCoroutine(areaRIdx, areaCIdx, grid, completeCallback, completeEffectCallback));
                }

        return comboCnt;
    }
    public bool CheckArea(int areaRIdx, int areaCIdx, Grid grid, int[,] gridDataSync, int gridPartFactor)
    { // areaRIdx : 0, 1, 2 & areaCIdx : 0, 1, 2  

        bool isComplete = true;
        for (int idxR = areaRIdx * 3; idxR < areaRIdx * 3 + 3; idxR++)
            for (int idxC = areaCIdx * 3; idxC < areaCIdx * 3 + 3; idxC++)
                if (gridDataSync[idxR, idxC] == Factor.HasPuzzle || gridDataSync[idxR, idxC] == gridPartFactor)
                    isComplete &= true;
                else
                    isComplete &= false;

        return isComplete;
    }

    /// <summary>
    /// 영역 한개 complete
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="completeCallback"></param>
    /// <returns></returns>
    private IEnumerator CompleteCoroutine(int areaRIdx, int areaCIdx, Grid grid, System.Action completeCallback, CompleteEffect completeEffectCallback)
    {
        for (int idxR = areaRIdx * 3; idxR < areaRIdx * 3 + 3; idxR++)
            for (int idxC = areaCIdx * 3; idxC < areaCIdx * 3 + 3; idxC++)
            {
                grid.SetDataIdx(idxR, idxC, Factor.HasNoPuzzle);
                completeEffectCallback?.Invoke(grid.ChildGridPartDic[$"{idxR},{idxC}"].transform.position, this);
                yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
            }

       completeCallback?.Invoke();
    }
} // end of class
