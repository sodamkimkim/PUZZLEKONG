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

    // drop하는 세로줄 reset
    private void CheckUseable_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        int idxC = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[1], out idxC);

        for (int idxR = 0; idxR < grid.Data.GetLength(0); idxR++)
        {
            if (grid.Data[idxR, idxC] == Factor.HasPuzzle)
                grid.SetDataIdx(idxR, idxC, Factor.Point);
        }

    }
    // drop하는 가로줄 reset
    private void CheckUseable_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
    }
    // 퍼즐 reset (기존것과 다르게)
    private void CheckUseable_Item_c_Reset(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
    }
    // 하단 가로줄과 상태 변경
    private void CheckUseable_Item_d_SwitchHori(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        // TODO 하단 가로줄이 없는 젤 마지막 가로줄이면 return;
    }
    // 우측 세로줄과 상태 변경
    private void CheckUseable_Item_e_SwitchVerti(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
        // TODO 최우측세로줄이면 return;
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

    private void PointComplete(Grid grid)
    {
        int rowLen = grid.Data.GetLength(0);
        int colLen = grid.Data.GetLength(1);
        for (int r = 0; r < rowLen; r++)
        {
            for (int c = 0; c < colLen; c++)
            {
                if (grid.Data[r, c] == Factor.Point)
                {
                    grid.SetDataIdx(r, c, Factor.HasNoPuzzle);
                }
            }
        }
        SetPuzzlesActiveCallback();
    }
    private bool Use_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return false;
        int col = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[1], out col);

        if (grid.ChildGridPartDic[item.TriggeredGridPartIdxStr].Data == Factor.Point)
        {
            PointComplete(grid);
            _itemUseEffect.Effect_Item_a_Mushroom(grid,item, col);
            return true;
        }

        int rowLen = grid.Data.GetLength(0);
        for (int i = 0; i < rowLen; i++)
        {
            if (grid.ChildGridPartDic[$"{i},{col}"].Data == Factor.Point)
            {
                PointComplete(grid); 
                _itemUseEffect.Effect_Item_a_Mushroom(grid, item, col);
                return true;
            }
        }
        return false;
    }
    private bool Use_Item_b_Wandoo(Grid grid, Item item)
    {
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
