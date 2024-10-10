using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid Complete여부 Column 검사
/// </summary>
public class CompleteVertical : MonoBehaviour
{
    public void MarkCompletable(Grid grid, int[,] gridDataSync)
    {
        // TODO 
    }
    public void Complete(Grid grid, int[,] gridDataSync, System.Action onCompleteCallback)
    {
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
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
                StartCoroutine(CompleteCoroutine(grid, idxC, rowLen, onCompleteCallback));
        }
    }
    private IEnumerator CompleteCoroutine(Grid grid, int idxC, int rowLen, System.Action onCompleteCallback)
    {
        for (int i = 0; i < rowLen; i++)
        {
            grid.SetDataIdx(i, idxC, 0);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
        onCompleteCallback?.Invoke();
    }
} // end of class
