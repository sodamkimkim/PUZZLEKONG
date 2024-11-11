using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 Column 검사
/// </summary>
public class CompleteVertical : MonoBehaviour
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
     
    public int Complete(Grid grid, int[,] gridDataSync)
    {
        IsProcessing = true;
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        int comboCnt = 0;

        for (int idxC = 0; idxC < colLen; idxC++)
        {
            bool isComplete = true;
            for (int idxR = 0; idxR < rowLen; idxR++)
            {
                if (gridDataSync[idxR, idxC] == Factor.HasPuzzle || gridDataSync[idxR, idxC] == Factor.Completable)
                    isComplete &= true;
                else
                    isComplete &= false;
            }

            if (isComplete)
            {
                comboCnt++;

                CompleteData(grid, idxC, rowLen);
                StartCoroutine(CompleteCoroutine(grid, idxC, rowLen));
            }
        }

        return comboCnt;
    }
    private void CompleteData(Grid grid, int idxC, int rowLen)
    {
        for (int i = 0; i < rowLen; i++)
        {
            grid.SetDataIdx(i, idxC, Factor.HasNoPuzzle);
        }
        SetPuzzlesActiveCallback?.Invoke();
    }
    private IEnumerator CompleteCoroutine(Grid grid, int idxC, int rowLen)
    {
        for (int i = 0; i < rowLen; i++)
        {
            CompleteEffectCallback?.Invoke(grid.ChildGridPartDic[$"{i},{idxC}"].transform.position, this);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
    }
} // end of class
