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
    // drop하는 세로줄 reset
    private void CheckUseable_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;

        int idxC = item.TriggeredGridPartIdxC;
        for (int idxR = 0; idxR < grid.Data.GetLength(0); idxR++)
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
    }
    // drop하는 가로줄 reset
    private void CheckUseable_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        int idxR = item.TriggeredGridPartIdxR;

        for (int idxC = 0; idxC < grid.Data.GetLength(1); idxC++)
            SetCheckData(grid, idxR, idxC, Factor.HasPuzzle, Factor.UseItem1);
    }
    // 퍼즐 reset (기존것과 다르게)
    private void CheckUseable_Item_c_Reset(PuzzleManager puzzleManager, Grid grid, Item item)
    {
        if (item.TriggeredPuzzle == null) return;
        SetPuzzleStatusDataCallback?.Invoke(Factor.PuzzleStatus_ItemUse);
    }
    // 하단 가로줄과 상태 변경
    private void CheckUseable_Item_d_SwitchHori(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        // TODO 하단 가로줄이 없는 젤 마지막 가로줄이면 return;
    }
    // 우측 세로줄과 상태 변경
    private void CheckUseable_Item_e_SwitchVerti(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxR == Factor.IntInitialized) return;
        if (item.TriggeredGridPartIdxC == Factor.IntInitialized) return;
        // TODO 최우측세로줄이면 return;
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
        }
        return false;
    }
    private bool DataComplete(Grid grid, int beforeFactor, int afterFactor
        , int startR, int endR, int startC, int endC, bool dirR, System.Action effectFunction)
    {
        bool isItemUsed = false;
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
                for (int c = startC; c <= endC; c++)
                    if (grid.Data[r, c] == beforeFactor)
                    {
                        grid.SetDataIdx(r, c, afterFactor);
                        isItemUsed = true;
                    }
        }

        if (isItemUsed)
        {
            effectFunction?.Invoke();
            StartCoroutine(grid.SetGridPartColorCoroutine(startR, endR, startC, endC, dirR, Factor.CompleteCoroutineInterval));
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
        return false;
    }

    private bool Use_Item_e_SwitchVerti(Grid grid, Item item)
    {
        return false;
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

        return DataComplete(grid, Factor.UseItem1, Factor.HasNoPuzzle, idxR - 1, idxR + 1, idxC - 1, idxC + 1, true, () => _itemUseEffect.Effect_Item_f_Bumb(grid, item, idxR, idxC));
    }
} // end of class
