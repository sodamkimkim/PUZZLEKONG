using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 Column 검사
/// </summary>
public class CompleteVertical : MonoBehaviour
{
    public void MarkCompletable(Grid grid, int[,] gridDataSync, System.Action resetFunction)
    {
        if (grid == null) return;
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);

        for (int idxC = 0; idxC < colLen; idxC++)
        {
            bool isCompletable = true;
            for (int idxR = 0; idxR < rowLen; idxR++)
            {
                if (gridDataSync[idxR, idxC] == Factor.HasPuzzle || gridDataSync[idxR, idxC] == Factor.Placable)
                    isCompletable &= true;
                else
                    isCompletable &= false;
            }

            if (isCompletable)
            {
                for (int i = 0; i < rowLen; i++)
                {
                    if (gridDataSync[i, idxC] == Factor.HasPuzzle)
                        grid.SetDataIdx(i, idxC, Factor.Completable);
                }

            }
        }
    }

    public delegate void CompleteEffect(Vector3 worldPos, MonoBehaviour callerMono); 
    public int Complete(Grid grid, int[,] gridDataSync, System.Action completeCallback, CompleteEffect completeEffectCallback)
    {
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        int comboCnt = 0;

        for (int idxC = 0; idxC < colLen; idxC++)
        {
            bool isComplete = true;
            for (int idxR = 0; idxR < rowLen; idxR++)
            {
                if (gridDataSync[idxR, idxC] == 1)
                    isComplete &= true;
                else
                    isComplete &= false;
            }

            if (isComplete)
            {
                comboCnt++;
                StartCoroutine(CompleteCoroutine(grid, idxC, rowLen, completeCallback, completeEffectCallback));
            }
        }

        return comboCnt;
    }
    private IEnumerator CompleteCoroutine(Grid grid, int idxC, int rowLen, System.Action completeCallback, CompleteEffect completeEffectCallback)
    {
        for (int i = 0; i < rowLen; i++)
        { 
            grid.SetDataIdx(i, idxC, 0);
            completeEffectCallback?.Invoke(grid.ChildGridPartDic[$"{i},{idxC}"].transform.position, this);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
        completeCallback?.Invoke();
    }
} // end of class
