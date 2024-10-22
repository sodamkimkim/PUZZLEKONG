using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 3*3 Area 9개 검사
/// </summary>
public class CompleteArea : MonoBehaviour
{
    public void MarkCompletable(Grid grid, int[,] gridDataSync)
    {
        // TODO 
    }
    public delegate void CompleteEffect(Vector3 worldPos);
    /// <summary>
    /// 모든 9개영역 조사
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridDataSync"></param>
    public void Complete(Grid grid, int[,] gridDataSync, System.Action onCompleteCallback, CompleteEffect completeEffectCallback)
    {
        int areaRLen = 3;
        int areaCLen = 3;

        for (int areaRIdx = 0; areaRIdx < areaRLen; areaRIdx++)
            for (int areaCIdx = 0; areaCIdx < areaCLen; areaCIdx++)
                if (CheckArea(areaRIdx, areaCIdx, grid, gridDataSync, onCompleteCallback))
                    StartCoroutine(CompleteCoroutine(areaRIdx, areaCIdx, grid, onCompleteCallback, completeEffectCallback));
    }
    public bool CheckArea(int areaRIdx, int areaCIdx, Grid grid, int[,] gridDataSync, System.Action onCompleteCallback)
    { // areaRIdx : 0, 1, 2 & areaCIdx : 0, 1, 2  

        bool isComplete = true;
        for (int idxR = areaRIdx * 3; idxR < areaRIdx * 3 + 3; idxR++)
            for (int idxC = areaCIdx * 3; idxC < areaCIdx * 3 + 3; idxC++)
                if (gridDataSync[idxR, idxC] == 1)
                    isComplete &= true;
                else
                    isComplete &= false;

        return isComplete;
    }

    /// <summary>
    /// 영역 한개 complete
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="onCompleteCallback"></param>
    /// <returns></returns>
    private IEnumerator CompleteCoroutine(int areaRIdx, int areaCIdx, Grid grid, System.Action onCompleteCallback, CompleteEffect completeEffectCallback)
    {
        for (int idxR = areaRIdx * 3; idxR < areaRIdx * 3 + 3; idxR++)
            for (int idxC = areaCIdx * 3; idxC < areaCIdx * 3 + 3; idxC++)
            {
                grid.SetDataIdx(idxR, idxC, 0);
                completeEffectCallback?.Invoke(grid.ChildGridPartDic[$"{idxR},{idxC}"].transform.position);
                yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
            }

        onCompleteCallback?.Invoke();
    }
} // end of class
