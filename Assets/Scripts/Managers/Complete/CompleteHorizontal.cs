using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 Row 검사
/// </summary>
public class CompleteHorizontal : MonoBehaviour
{
    public void MarkCompletable(Grid grid, int[,] gridDataSync)
    {
        // TODO 
    }
    public delegate void CompleteEffect(Vector3 worldPos);
  //  private  CompleteEffect1 completeEffect1Callback;
    public void Complete(Grid grid, int[,] gridDataSync, System.Action onCompleteCallback, CompleteEffect completeEffectCallback)
    {
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        for (int idxR = 0; idxR < rowLen; idxR++)
        {
            bool isComplete = true;
            for (int idxC = 0; idxC < colLen; idxC++)
            {
                if (gridDataSync[idxR, idxC] == 1)
                    isComplete &= true;
                else
                    isComplete &= false;
            }

            if (isComplete)
                StartCoroutine(CompleteCoroutine(grid, idxR, colLen, onCompleteCallback, completeEffectCallback));
        }
    }

    private IEnumerator CompleteCoroutine(Grid grid, int idxR, int colLen, System.Action onCompleteCallback, CompleteEffect completeEffectCallback)
    {
        for (int i = 0; i < colLen; i++)
        {
            grid.SetDataIdx(idxR, i, 0);
            completeEffectCallback?.Invoke(grid.ChildGridPartDic[$"{idxR},{i}"].transform.position);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
        onCompleteCallback?.Invoke();
    }
} // end of class