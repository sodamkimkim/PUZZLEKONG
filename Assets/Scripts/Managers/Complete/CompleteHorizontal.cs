using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete���� Row �˻�
/// </summary>
public class CompleteHorizontal : MonoBehaviour
{
    public bool IsProcessing = false;
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
    public int Complete(Grid grid, int[,] gridDataSync, System.Action SetPuzzlesActive, CompleteEffect completeEffectCallback)
    {
        IsProcessing = true;
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        int comboCnt = 0;

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
            {
                comboCnt++;

                CompleteData(grid, idxR, colLen, SetPuzzlesActive);
                StartCoroutine(CompleteCoroutine(grid, idxR, colLen, completeEffectCallback));
            }
        }

        return comboCnt;
    }
    private void CompleteData(Grid grid, int idxR, int colLen, System.Action SetPuzzleActive)
    {
        for (int i = 0; i < colLen; i++)
        {
            grid.SetDataIdx(idxR, i, Factor.HasNoPuzzle);
        }
        SetPuzzleActive?.Invoke();
    }
    private IEnumerator CompleteCoroutine(Grid grid, int idxR, int colLen, CompleteEffect completeEffectCallback)
    {
        for (int i = 0; i < colLen; i++)
        { 
            completeEffectCallback?.Invoke(grid.ChildGridPartDic[$"{idxR},{i}"].transform.position, this);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
    }
} // end of class