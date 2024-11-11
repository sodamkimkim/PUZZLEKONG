using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 Row 검사
/// </summary>
public class CompleteHorizontal : MonoBehaviour
{
    public bool IsProcessing = false;

    public delegate void CompleteEffect(Vector3 worldPos, MonoBehaviour callerMono);
    public CompleteEffect CompleteEffectCallback;
    public PuzzleManager.SetPuzzleActive SetPuzzlesActiveCallback;
    public void Init(PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback, CompleteEffect completeEffectCallback)
    {
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;
        CompleteEffectCallback = completeEffectCallback;
    }
    public void MarkCompletable(Grid grid, int[,] gridDataSync)
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
                    {
                        grid.SetDataIdx(idxR, i, Factor.Completable);
                        grid.ChildGridPartDic[$"{idxR},{i}"].SetGridPartColor();
                    }
                }

            }
        }
    }

    public int Complete(Grid grid, int[,] gridDataSync)
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

                CompleteData(grid, idxR, colLen);
                StartCoroutine(grid.SetGridPartColorCoroutine(idxR, idxR, 0, grid.Data.GetLength(1) - 1, true, Factor.CompleteCoroutineInterval));
                StartCoroutine(CompleteCoroutine(grid, idxR, colLen));
            }
        }

        return comboCnt;
    }
    private void CompleteData(Grid grid, int idxR, int colLen)
    {
        for (int i = 0; i < colLen; i++)
        {
            grid.SetDataIdx(idxR, i, Factor.HasNoPuzzle);
        }
        SetPuzzlesActiveCallback?.Invoke();
    }
    private IEnumerator CompleteCoroutine(Grid grid, int idxR, int colLen)
    {
        for (int i = 0; i < colLen; i++)
        {
            CompleteEffectCallback?.Invoke(grid.ChildGridPartDic[$"{idxR},{i}"].transform.position, this);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
    }
} // end of class