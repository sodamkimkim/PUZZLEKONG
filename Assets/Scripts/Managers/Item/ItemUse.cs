using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField]
    private ItemUseEffect _itemUseEffect = null;
    public PuzzleManager.SetPuzzleActive SetPuzzlesActiveCallback;

    public void Init(PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback)
    {
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;
    }
    public void CheckUseable(Grid grid, Item item)
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
                CheckUseable_Item_c_Reset(grid, item);
                break;
            case "Item_d_SwitchHori":
                CheckUseable_Item_d_SwitchHori(grid, item);
                break;
            case "Item_e_SwitchVerti":
                CheckUseable_Item_e_SwitchVerti(grid, item);
                break;
        }
    }

    // drop�ϴ� ������ reset
    private void CheckUseable_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        int idxC = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[1], out idxC);

        for (int idxR = 0; idxR < grid.Data.GetLength(0); idxR++)
        {
            if (grid.Data[idxR, idxC] == Factor.HasPuzzle)
            {
                grid.SetDataIdx(idxR, idxC, Factor.Point);
                grid.ChildGridPartDic[$"{idxR},{idxC}"].SetGridPartColor();
            }
        }

    }
    // drop�ϴ� ������ reset
    private void CheckUseable_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        int idxR = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[0], out idxR);

        for (int idxC = 0; idxC < grid.Data.GetLength(1); idxC++)
        {
            if (grid.Data[idxR, idxC] == Factor.HasPuzzle)
            {
                grid.SetDataIdx(idxR, idxC, Factor.Point);
                grid.ChildGridPartDic[$"{idxR},{idxC}"].SetGridPartColor();
            }
        }
    }
    // ���� reset (�����Ͱ� �ٸ���)
    private void CheckUseable_Item_c_Reset(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
    }
    // �ϴ� �����ٰ� ���� ����
    private void CheckUseable_Item_d_SwitchHori(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        // TODO �ϴ� �������� ���� �� ������ �������̸� return;
    }
    // ���� �����ٰ� ���� ����
    private void CheckUseable_Item_e_SwitchVerti(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        // TODO �ֿ����������̸� return;
    }

    public bool UseItem(Grid grid, Item item)
    {
        if (grid == null || item == null) return false;
        switch (item.name)
        {
            case "Item_a_Mushroom":
                return Use_Item_a_Mushroom(grid, item);
            case "Item_b_Wandoo":
                return Use_Item_b_Wandoo(grid, item);
            case "Item_c_Reset":
                return Use_Item_c_Reset(grid, item);
            case "Item_d_SwitchHori":
                return Use_Item_d_SwitchHori(grid, item);
            case "Item_e_SwitchVerti":
                return Use_Item_e_SwitchVerti(grid, item);
        }
        return false;
    }

    private void PointComplete(Grid grid, int startR, int endR, int startC, int endC, bool dirR)
    {
        if (dirR)
        {
            for (int r = startR; r <= endR; r++)
            {
                for (int c = startC; c <= endC; c++)
                {
                    if (grid.Data[r, c] == Factor.Point)
                        grid.SetDataIdx(r, c, Factor.HasNoPuzzle);
                }
            }
        }
        else
        {
            for (int r = startR; r >= endR; r--)
            {
                for (int c = startC; c <= endC; c++)
                {
                    if (grid.Data[r, c] == Factor.Point)
                        grid.SetDataIdx(r, c, Factor.HasNoPuzzle);
                }
            }
        }
        StartCoroutine(grid.SetGridPartColorCoroutine(startR, endR, startC, endC, dirR, Factor.CompleteCoroutineInterval*2));
        SetPuzzlesActiveCallback();
    }
    private bool Use_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return false;
        int col = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[1], out col);

        if (grid.ChildGridPartDic[item.TriggeredGridPartIdxStr].Data == Factor.Point)
        {
            PointComplete(grid, grid.Data.GetLength(0) - 1, 0, col, col, false);
            _itemUseEffect.Effect_Item_a_Mushroom(grid, item, col);
            return true;
        }

        int rowLen = grid.Data.GetLength(0);
        for (int i = 0; i < rowLen; i++)
        {
            if (grid.ChildGridPartDic[$"{i},{col}"].Data == Factor.Point)
            {
                PointComplete(grid, grid.Data.GetLength(0) - 1, 0, col, col, false);
                _itemUseEffect.Effect_Item_a_Mushroom(grid, item, col);
                return true;
            }
        }
        return false;
    }
    private bool Use_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return false;
        int row = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[0], out row);

        if (grid.ChildGridPartDic[item.TriggeredGridPartIdxStr].Data == Factor.Point)
        {
            PointComplete(grid, row, row, 0, grid.Data.GetLength(1) - 1, true);
            _itemUseEffect.Effect_Item_b_Wandoo(grid, item, row);
            return true;
        }

        int colLen = grid.Data.GetLength(1);
        for (int i = 0; i < colLen; i++)
        {
            if (grid.ChildGridPartDic[$"{row},{i}"].Data == Factor.Point)
            {
                PointComplete(grid, row, row, 0, grid.Data.GetLength(1) - 1, true);
                _itemUseEffect.Effect_Item_b_Wandoo(grid, item, row);
                return true;
            }
        }
        return false;
    }

    private bool Use_Item_c_Reset(Grid grid, Item item)
    {
        return false;
    }
    private bool Use_Item_d_SwitchHori(Grid grid, Item item)
    {
        return false;
    }

    private bool Use_Item_e_SwitchVerti(Grid grid, Item item)
    {
        return false;
    }

} // end of class