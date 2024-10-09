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
    public void Complete(Grid grid, int[,] gridDataSync)
    {
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);

        for(int idxR=0; idxR < rowLen; idxR++)
        {
            for (int idxC = 0; idxC < colLen; idxC++)
            {
                if (grid.Data[idxR, idxC] == 1)
                    grid.SetData(idxR, idxC, 0);
            } 
        }
    }
} // end of class