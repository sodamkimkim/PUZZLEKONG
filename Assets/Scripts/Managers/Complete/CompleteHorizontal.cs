using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 Row 검사
/// </summary>
public class CompleteHorizontal : MonoBehaviour
{
    public void MarkCompletable(Grid grid, int[,] gridDataSync, System.Action resetFunction)
    {
        if (grid == null) return;
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        for (int idxR = 0; idxR < rowLen; idxR++)
        {
            bool isCompletable = true;
            for (int idxC = 0; idxC < colLen; idxC++)
            {
                if (gridDataSync[idxR, idxC] == Factor.HasPuzzle || gridDataSync[idxR, idxC] == Factor.Placable)
                    isCompletable &= true;
                else
                    isCompletable &= false;
            }

            if (isCompletable)
            {
                for (int i = 0; i < colLen; i++)
                {
                    if (gridDataSync[idxR, i] == Factor.HasPuzzle)
                        grid.SetDataIdx(idxR, i, Factor.Completable);
                }

            }
        }
    }

    public delegate void CompleteEffect(Vector3 worldPos, MonoBehaviour callerMono);
    public void Complete(Grid grid, int[,] gridDataSync, System.Action completeCallback, CompleteEffect completeEffectCallback)
    {
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        for (int idxR = 0; idxR < rowLen; idxR++)
        {
            bool isComplete = true;
            for (int idxC = 0; idxC < colLen; idxC++)
            {
                if (gridDataSync[idxR, idxC] == Factor.HasPuzzle || gridDataSync[idxR, idxC] == Factor.Completable)
                    isComplete &= true;
                else
                    isComplete &= false;
            }

            if (isComplete)
                StartCoroutine(CompleteCoroutine(grid, idxR, colLen, completeCallback, completeEffectCallback));
        }
    }

    private IEnumerator CompleteCoroutine(Grid grid, int idxR, int colLen, System.Action completeCallback, CompleteEffect completeEffectCallback)
    {
        for (int i = 0; i < colLen; i++)
        {
            grid.SetDataIdx(idxR, i, Factor.HasNoPuzzle);
            completeEffectCallback?.Invoke(grid.ChildGridPartDic[$"{idxR},{i}"].transform.position, this);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
        completeCallback?.Invoke();
    }
} // end of class