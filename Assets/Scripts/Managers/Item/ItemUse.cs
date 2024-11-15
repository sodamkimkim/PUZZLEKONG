using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField]
    private ItemUseEffect _itemUseEffect = null;
    public PuzzleManager.SetPuzzleActive SetPuzzlesActiveCallback;
    public delegate void SetPuzzleStatusData(int puzzleStatusData);
    private SetPuzzleStatusData SetPuzzleStatusDataCallback;
    public void Init(SetPuzzleStatusData setPuzzleStatusData, PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback)
    {
        SetPuzzleStatusDataCallback = setPuzzleStatusData;
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;
    }
    public void CheckUseable(PuzzleManager _puzzleManager, Grid grid, Item item)
    {
        if (grid == null || item == null) return;

        switch (item.name)
        {
            case "Item_a_Mushroom":
                CheckUseable_Item_a_Mushroom(grid, item);
                break;
            case "Item_b_Wandoo":
                CheckUseable_Item_b_Wandoo(grid, item);
                break;
            case "Item_c_Reset":
                CheckUseable_Item_c_Reset(_puzzleManager, grid, item);
                break;
            case "Item_d_SwitchHori":
                CheckUseable_Item_d_SwitchHori(grid, item);
                break;
            case "Item_e_SwitchVerti":
                CheckUseable_Item_e_SwitchVerti(grid, item);
                break;
            case "Item_f_Bumb":
                CheckUseable_Item_f_Bumb(grid, item);
                break;
            case "Item_g_Eraser":
                CheckUseable_Item_g_Eraser(grid, item);
                break;
            case "Item_h_PushLeft":
                CheckUseable_Item_h_PushLeft(grid, item);
                break;
            case "Item_i_PushUp":
                CheckUseable_Item_i_PushUp(grid, item);
                break;
        }
    }
    private void SetCheckData(Grid grid, int idxR, int idxC, int beforeData, int afterData)
    {
        if (grid.Data[idxR, idxC] == beforeData)
        {
            grid.SetDataIdx(idxR, idxC, afterData);
            grid.ChildGridPartDic[$"{idxR},{idxC}"].SetGridPartColor();
        }
    }
    // drop�ϴ� ������ reset
    private void CheckUseable_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;

        int idxC = item.TriggeredGridPartIdxC;
        for (int idxR = 0; idxR < grid.Data.GetLength(0); idxR++)
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
    }
    // drop�ϴ� ������ reset
    private void CheckUseable_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        int idxR = item.TriggeredGridPartIdxR;

        for (int idxC = 0; idxC < grid.Data.GetLength(1); idxC++)
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
    }
    // ���� reset (�����Ͱ� �ٸ���)
    private void CheckUseable_Item_c_Reset(PuzzleManager puzzleManager, Grid grid, Item item)
    {
        if (item.TriggeredPuzzle == null) return;
        SetPuzzleStatusDataCallback?.Invoke(Factor.PuzzleStatus_ItemUse);
    }
    // �ϴ� �����ٰ� ���� ����
    private void CheckUseable_Item_d_SwitchHori(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxR >= grid.Data.GetLength(0) - 1) return;

        int idxR = item.TriggeredGridPartIdxR;
        for (int idxC = 0; idxC < grid.Data.GetLength(1); idxC++)
        {
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
            SetCheckData(grid, idxR + 1, idxC, Factor.HasPuzzle, Factor.UseItem1);
        }
    }
    // ���� �����ٰ� ���� ����
    private void CheckUseable_Item_e_SwitchVerti(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC >= grid.Data.GetLength(1) - 1) return;

        int idxC = item.TriggeredGridPartIdxC;
        for (int idxR = 0; idxR < grid.Data.GetLength(0); idxR++)
        {
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
            SetCheckData(grid, idxR, idxC + 1, Factor.HasPuzzle, Factor.UseItem1);
        }
    }
    // 2X2 
    private void CheckUseable_Item_f_Bumb(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxR > grid.Data.GetLength(0) - 2) return;
        if (item.TriggeredGridPartIdxC > grid.Data.GetLength(1) - 2) return;
        if (item.TriggeredGridPartIdxR < 1f) return;
        if (item.TriggeredGridPartIdxC < 1f) return;

        int idxR = item.TriggeredGridPartIdxR;
        int idxC = item.TriggeredGridPartIdxC;

        SetCheckData(grid, idxR - 1, idxC - 1, Factor.HasPuzzle, Factor.UseItem1);
        SetCheckData(grid, idxR - 1, idxC, Factor.HasPuzzle, Factor.UseItem1);
        SetCheckData(grid, idxR - 1, idxC + 1, Factor.HasPuzzle, Factor.UseItem1);

        SetCheckData(grid, idxR, idxC - 1, Factor.HasPuzzle, Factor.UseItem1);
        SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
        SetCheckData(grid, idxR, idxC + 1, Factor.HasPuzzle, Factor.UseItem1);

        SetCheckData(grid, idxR + 1, idxC - 1, Factor.HasPuzzle, Factor.UseItem1);
        SetCheckData(grid, idxR + 1, idxC, Factor.HasPuzzle, Factor.UseItem1);
        SetCheckData(grid, idxR + 1, idxC + 1, Factor.HasPuzzle, Factor.UseItem1);
    }
    private void CheckUseable_Item_g_Eraser(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;

        SetCheckData(grid, item.TriggeredGridPartIdxR, item.TriggeredGridPartIdxC, Factor.HasPuzzle, Factor.UseItem1);
    }
    /// <summary>
    /// 3�� Left�б�
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="item"></param>
    private void CheckUseable_Item_h_PushLeft(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxR < 1f) return;
        if (item.TriggeredGridPartIdxC < 1f) return;
        if (item.TriggeredGridPartIdxR + 1 > grid.Data.GetLength(0) - 1) return;
        if (item.TriggeredGridPartIdxC + 1 > grid.Data.GetLength(1) - 1) return;

        int idxR = item.TriggeredGridPartIdxR;

        for (int idxC = 0; idxC < grid.Data.GetLength(1); idxC++)
        {
            SetCheckData(grid, idxR - 1, idxC, Factor.HasPuzzle, Factor.UseItem1);
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
            SetCheckData(grid, idxR + 1, idxC, Factor.HasPuzzle, Factor.UseItem1);
        }
    }
    /// <summary>
    /// 3�� ���� �б�
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="item"></param>
    private void CheckUseable_Item_i_PushUp(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxR < 1f) return;
        if (item.TriggeredGridPartIdxC < 1f) return;
        if (item.TriggeredGridPartIdxR + 1 > grid.Data.GetLength(0) - 1) return;
        if (item.TriggeredGridPartIdxC + 1 > grid.Data.GetLength(1) - 1) return;

        int idxC = item.TriggeredGridPartIdxC;

        for (int idxR = 0; idxR < grid.Data.GetLength(0); idxR++)
        {
            SetCheckData(grid, idxR, idxC - 1, Factor.HasPuzzle, Factor.UseItem1);
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
            SetCheckData(grid, idxR, idxC + 1, Factor.HasPuzzle, Factor.UseItem1);
        }
    }
    public bool UseItem(PuzzleManager puzzleManager, Grid grid, Item item)
    {
        if (grid == null || item == null) return false;
        switch (item.name)
        {
            case "Item_a_Mushroom":
                return Use_Item_a_Mushroom(grid, item);
            case "Item_b_Wandoo":
                return Use_Item_b_Wandoo(grid, item);
            case "Item_c_Reset":
                return Use_Item_c_Reset(puzzleManager, grid, item);
            case "Item_d_SwitchHori":
                return Use_Item_d_SwitchHori(grid, item);
            case "Item_e_SwitchVerti":
                return Use_Item_e_SwitchVerti(grid, item);
            case "Item_f_Bumb":
                return Use_Item_f_Bumb(grid, item);
            case "Item_g_Eraser":
                return Use_Item_g_Eraser(grid, item);
            case "Item_h_PushLeft":
                return Use_Item_h_PushLeft(grid, item);
            case "Item_i_PushUp":
                return Use_Item_i_PushUp(grid, item);
        }
        return false;
    }
    private bool DataComplete(Grid grid, int beforeFactor, int afterFactor
        , int startR, int endR, int startC, int endC, bool dirR, System.Action effectFunction)
    {
        bool isItemUsed = false;
        // # ��ĭ �����
        if (startR == endR && startC == endC && grid.Data[startR, startC] == beforeFactor)
        {
            grid.SetDataIdx(startR, startC, afterFactor);
            isItemUsed = true;
        }
        // # �̿�
        else
        {
            if (dirR)
            {
                for (int r = startR; r <= endR; r++)
                    for (int c = startC; c <= endC; c++)
                        if (grid.Data[r, c] == beforeFactor)
                        {
                            grid.SetDataIdx(r, c, afterFactor);
                            isItemUsed = true;
                        }
            }
            else
            {
                for (int r = startR; r >= endR; r--)
                    for (int c = startC; c >= endC; c--)
                        if (grid.Data[r, c] == beforeFactor)
                        {
                            grid.SetDataIdx(r, c, afterFactor);
                            isItemUsed = true;
                        }
            }
        }

        if (isItemUsed)
        {
            if (effectFunction != null)
                effectFunction?.Invoke();

            if (startR == endR && startC == endC) grid.ChildGridPartDic[$"{startR},{startC}"].SetGridPartColor();
            else StartCoroutine(grid.SetGridPartColorCoroutine(startR, endR, startC, endC, dirR, Factor.CompleteCoroutineInterval));

            SetPuzzlesActiveCallback();
        }
        return isItemUsed;
    }
    private bool Use_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        int idxC = item.TriggeredGridPartIdxC;

        return DataComplete(grid, Factor.UseItem1, Factor.HasNoPuzzle, grid.Data.GetLength(0) - 1, 0, idxC, idxC, false, () => _itemUseEffect.Effect_Item_a_Mushroom(grid, item, idxC));
    }
    private bool Use_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        int idxR = item.TriggeredGridPartIdxR;

        return DataComplete(grid, Factor.UseItem1, Factor.HasNoPuzzle, idxR, idxR, 0, grid.Data.GetLength(1) - 1, true, () => _itemUseEffect.Effect_Item_b_Wandoo(grid, item, idxR));
    }

    private bool Use_Item_c_Reset(PuzzleManager puzzleManager, Grid grid, Item item)
    {
        if (item.TriggeredPuzzle == null) return false;

        if (item.TriggeredPuzzle.ItemStatusData == Factor.PuzzleStatus_ItemUse)
        {
            puzzleManager.LazyStart();
            return true;
        }
        else return false;
    }
    private bool Use_Item_d_SwitchHori(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxR >= grid.Data.GetLength(0) - 1) return false;

        int idxR = item.TriggeredGridPartIdxR;
        bool isItemUsed = false;
        for (int c = 0; c < grid.Data.GetLength(1) - 1; c++)
        {
            if (grid.Data[idxR, c] == Factor.UseItem1)
            {
                isItemUsed = true;
                grid.SetDataIdx(idxR, c, Factor.HasPuzzle);
            }

            if (grid.Data[idxR + 1, c] == Factor.UseItem1)
                grid.SetDataIdx(idxR + 1, c, Factor.HasPuzzle);

            int temp = grid.Data[idxR, c];

            grid.SetDataIdx(idxR, c, grid.Data[idxR + 1, c]);
            grid.SetDataIdx(idxR + 1, c, temp);

        }

        if (isItemUsed)
        {
            //effectFunction?.Invoke();
            StartCoroutine(grid.SetGridPartColorCoroutine(idxR, idxR + 1, 0, grid.Data.GetLength(1) - 1, true, Factor.CompleteCoroutineInterval));
            SetPuzzlesActiveCallback();
        }
        return isItemUsed;
    }

    private bool Use_Item_e_SwitchVerti(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC >= grid.Data.GetLength(1) - 1) return false;

        int idxC = item.TriggeredGridPartIdxC;

        bool isItemUsed = false;
        for (int r = 0; r < grid.Data.GetLength(0) - 1; r++)
        {
            if (grid.Data[r, idxC] == Factor.UseItem1)
            {
                isItemUsed = true;
                grid.SetDataIdx(r, idxC, Factor.HasPuzzle);
            }

            if (grid.Data[r, idxC + 1] == Factor.UseItem1)
                grid.SetDataIdx(r, idxC + 1, Factor.HasPuzzle);

            int temp = grid.Data[r, idxC];

            grid.SetDataIdx(r, idxC, grid.Data[r, idxC + 1]);
            grid.SetDataIdx(r, idxC + 1, temp);
        }

        if (isItemUsed)
        {
            //effectFunction?.Invoke();
            StartCoroutine(grid.SetGridPartColorCoroutine(0, grid.Data.GetLength(0) - 1, idxC, idxC + 1, true, Factor.CompleteCoroutineInterval));
            SetPuzzlesActiveCallback();
        }
        return isItemUsed;
    }
    private bool Use_Item_f_Bumb(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxR > grid.Data.GetLength(0) - 2) return false;
        if (item.TriggeredGridPartIdxC > grid.Data.GetLength(1) - 2) return false;
        if (item.TriggeredGridPartIdxR < 1f) return false;
        if (item.TriggeredGridPartIdxC < 1f) return false; ;
        int idxR = item.TriggeredGridPartIdxR;
        int idxC = item.TriggeredGridPartIdxC;

        return DataComplete(grid, Factor.UseItem1, Factor.HasNoPuzzle, idxR - 1, idxR + 1, idxC - 1, idxC + 1, true, () => _itemUseEffect.Effect_Item_f_Bumb(grid, item));
    }
    private bool Use_Item_g_Eraser(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        int idxR = item.TriggeredGridPartIdxR;
        int idxC = item.TriggeredGridPartIdxC;
        return DataComplete(grid, Factor.UseItem1, Factor.HasNoPuzzle, idxR, idxR, idxC, idxC, true, () => _itemUseEffect.Effect_Item_g_Eraser(grid, item, idxR, idxC));
    }

    /// <summary>
    /// 3�� Push Left
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool Use_Item_h_PushLeft(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxR < 1f) return false;
        if (item.TriggeredGridPartIdxC < 1f) return false;
        if (item.TriggeredGridPartIdxR + 1 > grid.Data.GetLength(0) - 1) return false;
        if (item.TriggeredGridPartIdxC + 1 > grid.Data.GetLength(1) - 1) return false;
        int idxR = item.TriggeredGridPartIdxR;

        int[] cntArr = new int[3]; // r��° �濡 �� Factor.HasPuzzle���� ����

        for (int r = 0; r < cntArr.Length; r++)
        {
            int cnt = 0;
            for (int c = 0; c < grid.Data.GetLength(1); c++)
                if (grid.Data[idxR - 1 + r, c] == Factor.UseItem1) cnt++;

            cntArr[r] = cnt;
        }

        // # �о��ֱ� ������ ���ϱ�
        int[,] temp1 = new int[cntArr.Length, grid.Data.GetLength(1)];
        for (int r = 0; r < cntArr.Length; r++)
        {
            for (int c = 0; c < cntArr[r]; c++)
                temp1[r, c] = Factor.HasPuzzle;
            for (int c = cntArr[r]; c < grid.Data.GetLength(1); c++)
                temp1[r, c] = Factor.HasNoPuzzle;
        }

        // # ������ �迭�� �ٸ����� üũ
        bool isDifferent = false;
        for (int r = 0; r < cntArr.Length; r++)
        {
            for (int c = 0; c < grid.Data.GetLength(1); c++)
            {
                if ((grid.Data[idxR - 1 + r, c] == Factor.UseItem1)
                    && temp1[r, c] != Factor.HasPuzzle)
                {
                    isDifferent = true;
                    break;
                }
            }

            if (isDifferent)
                break;
        }

        // # ������ ����
        if (isDifferent)
        {
            //_itemUseEffect.Effect_Item_h_PushLeft
            // data 

            for (int r = 0; r < cntArr.Length; r++)
            {
                for (int c = 0; c < grid.Data.GetLength(1); c++)
                {
                    if (temp1[r, c] == Factor.HasPuzzle)
                        grid.SetDataIdx(idxR - 1 + r, c, Factor.HasPuzzle);
                    else if (temp1[r, c] == Factor.HasNoPuzzle)
                        grid.SetDataIdx(idxR - 1 + r, c, Factor.HasNoPuzzle);

                }
            }
            StartCoroutine(grid.SetGridPartColorCoroutine(idxR - 1, idxR + 1, 0, grid.Data.GetLength(1) - 1, true, Factor.CompleteCoroutineInterval));
            SetPuzzlesActiveCallback();
        }

        Debug.Log("PushLeft - isDifferent " + isDifferent);
        return isDifferent;
    }
    /// <summary>
    /// 3�� Push up
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool Use_Item_i_PushUp(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxR < 1f) return false;
        if (item.TriggeredGridPartIdxC < 1f) return false;
        if (item.TriggeredGridPartIdxR + 1 > grid.Data.GetLength(0) - 1) return false;
        if (item.TriggeredGridPartIdxC + 1 > grid.Data.GetLength(1) - 1) return false;
        int idxC = item.TriggeredGridPartIdxC;

        int[] cntArr = new int[3]; // c��° �濡 �� Factor.HasPuzzle���� ����

        for (int c = 0; c < cntArr.Length; c++)
        {
            int cnt = 0;
            for (int r = 0; r < grid.Data.GetLength(0); r++)
                if (grid.Data[r, idxC - 1 + c] == Factor.UseItem1) cnt++;

            cntArr[c] = cnt;
        }

        // # �о��ֱ� ������ ���ϱ�
        int[,] temp1 = new int[grid.Data.GetLength(0), cntArr.Length];
        for (int c = 0; c < cntArr.Length; c++)
        {
            for (int r = 0; r < cntArr[c]; r++)
                temp1[r, c] = Factor.HasPuzzle;
            for (int r = cntArr[c]; r < grid.Data.GetLength(0); r++)
                temp1[r, c] = Factor.HasNoPuzzle;
        }

        // # ������ �迭�� �ٸ����� üũ
        bool isDifferent = false;
        for (int c = 0; c < cntArr.Length; c++)
        {
            for (int r = 0; r < grid.Data.GetLength(0); r++)
            {
                if ((grid.Data[r, idxC - 1 + c] == Factor.UseItem1 )
                    && temp1[r, c] != Factor.HasPuzzle)
                {
                    isDifferent = true;
                    break;
                }
            }

            if (isDifferent)
                break;
        }

        // # ������ ����
        if (isDifferent)
        {
            for (int c = 0; c < cntArr.Length; c++)
            {
                for (int r = 0; r < grid.Data.GetLength(0); r++)
                {
                    if (temp1[r, c] == Factor.HasPuzzle)
                        grid.SetDataIdx(r, idxC - 1 + c, Factor.HasPuzzle);
                    else if (temp1[r, c] == Factor.HasNoPuzzle)
                        grid.SetDataIdx(r, idxC - 1 + c, Factor.HasNoPuzzle);

                }
            }
            StartCoroutine(grid.SetGridPartColorCoroutine(0, grid.Data.GetLength(0) - 1, idxC - 1, idxC + 1, true, Factor.CompleteCoroutineInterval));
            SetPuzzlesActiveCallback();
        }

        Debug.Log("PushUp - isDifferent " + isDifferent);
        return isDifferent;
    }
    private bool Use_Item_h_PushLeft_All(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        int idxC = item.TriggeredGridPartIdxC;

        int[] cntArr = new int[grid.Data.GetLength(0)]; // r��° �濡 �� Factor.HasPuzzle���� ����

        for (int r = 0; r < grid.Data.GetLength(0); r++)
        {
            int cnt = 0;
            for (int c = 0; c < grid.Data.GetLength(1); c++)
                if (grid.Data[r, c] == Factor.HasPuzzle || grid.Data[r, c] == Factor.UseItem1) cnt++;

            cntArr[r] = cnt;
        }

        // # �о��ֱ� ������ ���ϱ�
        int[,] temp1 = new int[grid.Data.GetLength(0), grid.Data.GetLength(1)];
        for (int r = 0; r < grid.Data.GetLength(0); r++)
        {
            for (int c = 0; c < cntArr[r]; c++)
                temp1[r, c] = Factor.HasPuzzle;
            for (int c = cntArr[r]; c < grid.Data.GetLength(1); c++)
                temp1[r, c] = Factor.HasNoPuzzle;
        }

        // # ������ �迭�� �ٸ����� üũ
        bool isDifferent = false;
        for (int r = 0; r < grid.Data.GetLength(0); r++)
        {
            for (int c = 0; c < grid.Data.GetLength(1); c++)
            {
                if ((grid.Data[r, c] == Factor.UseItem1 || grid.Data[r, c] == Factor.HasPuzzle) && temp1[r, c] != Factor.HasPuzzle)
                {
                    isDifferent = true;
                    break;
                }

            }

            if (isDifferent)
                break;
        }

        // # ������ ����
        if (isDifferent)
        {
            grid.Data = temp1;
            //_itemUseEffect.Effect_Item_h_PushLeft
            // StartCoroutine(grid.SetGridPartColorCoroutine(0, grid.Data.GetLength(0) - 1, 0, grid.Data.GetLength(1) - 1, true, Factor.CompleteCoroutineInterval));
            SetPuzzlesActiveCallback();
        }

        Debug.Log("PushUp - isDifferent" + isDifferent);
        return isDifferent;
    }
    private bool Use_Item_i_PushUp_All(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return false;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return false;
        int idxC = item.TriggeredGridPartIdxC;

        int[] cntArr = new int[grid.Data.GetLength(1)]; // c��° �濡 �� Factor.HasPuzzle���� ����

        for (int c = 0; c < grid.Data.GetLength(1); c++)
        {
            int cnt = 0;
            for (int r = 0; r < grid.Data.GetLength(0); r++)
                if (grid.Data[r, c] == Factor.HasPuzzle || grid.Data[r, c] == Factor.UseItem1) cnt++;

            cntArr[c] = cnt;
        }

        // # �о��ֱ� ������ ���ϱ�
        int[,] temp1 = new int[grid.Data.GetLength(0), grid.Data.GetLength(1)];
        for (int c = 0; c < grid.Data.GetLength(1); c++)
        {
            for (int r = 0; r < cntArr[c]; r++)
                temp1[r, c] = Factor.HasPuzzle;
            for (int r = cntArr[c]; r < grid.Data.GetLength(1); r++)
                temp1[r, c] = Factor.HasNoPuzzle;
        }

        // # ������ �迭�� �ٸ����� üũ
        bool isDifferent = false;
        for (int r = 0; r < grid.Data.GetLength(0); r++)
        {

            for (int c = 0; c < grid.Data.GetLength(1); c++)
            {
                if ((grid.Data[r, c] == Factor.UseItem1 || grid.Data[r, c] == Factor.HasPuzzle) && temp1[r, c] != Factor.HasPuzzle)
                {
                    isDifferent = true;
                    break;
                }

            }
            if (isDifferent)
                break;
        }
        Debug.Log("PushLeft - isDifferent" + isDifferent);
        // # ������ ����
        if (isDifferent)
        {
            grid.Data = temp1;
            //_itemUseEffect.Effect_Item_h_PushLeft
            // StartCoroutine(grid.SetGridPartColorCoroutine(0, grid.Data.GetLength(0) - 1, 0, grid.Data.GetLength(1) - 1, true, Factor.CompleteCoroutineInterval));
            SetPuzzlesActiveCallback();
        }

        return isDifferent;
    }
} // end of class
