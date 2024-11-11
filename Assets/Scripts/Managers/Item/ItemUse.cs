using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
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
                grid.SetDataIdx(idxR, idxC, Factor.Point);
        }

    }
    // drop�ϴ� ������ reset
    private void CheckUseable_Item_b_Wandoo(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return;
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

    public bool UseItem(CompleteManager completeManager, Grid grid, Item item)
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
    private bool Use_Item_a_Mushroom(Grid grid, Item item)
    {
        if (item.TriggeredGridPartIdxStr == string.Empty) return false;
        if (grid.ChildGridPartDic[item.TriggeredGridPartIdxStr].Data == Factor.Point)
        {
      // PointComplete();
            return true; 
        }

        int col = 0;
        int.TryParse(item.TriggeredGridPartIdxStr.Split(',')[1], out col);
        int rowLen = grid.Data.GetLength(0);
        for (int i = 0; i < rowLen; i++)
        {
            if (grid.ChildGridPartDic[$"{i},{col}"].Data == Factor.Point)
            {
          //  PointComplete();
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
