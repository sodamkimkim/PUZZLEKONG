using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseEffect : MonoBehaviour
{
    public void Effect_Item_a_Mushroom(Grid grid, Item item, int col)
    {
        if (grid == null || item == null) return;

        GameObject cloneItemGo = Instantiate(item.gameObject, null);
        Item itemClone = cloneItemGo.GetComponent<Item>();
        itemClone.Anim("Anim1", true); // item�� Anim ����� 
        itemClone.SetScale(Enum.eItemScale.Small, 0.5f);
        StartCoroutine(EffectCoroutine_Item_a_Mushroom(grid, itemClone, col));
    }
    private IEnumerator EffectCoroutine_Item_a_Mushroom(Grid grid, Item itemClone, int idxC)
    {
        int rowLen = grid.Data.GetLength(0);
        for (int r = rowLen - 1; r >= 0; r--)
        {
            itemClone.SetPos(true, grid.ChildGridPartDic[$"{r},{idxC}"].transform.position);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval * 2f);
        }
        DestroyImmediate(itemClone.gameObject);
    }
    public void Effect_Item_b_Wandoo(Grid grid, Item item, int row)
    {
        if (grid == null || item == null) return;

        GameObject cloneItemGo = Instantiate(item.gameObject, null);
        Item itemClone = cloneItemGo.GetComponent<Item>();
        itemClone.Anim("Anim2", true); // item�� Anim ����� 
        itemClone.SetScale(Enum.eItemScale.Small, 0.5f);
        StartCoroutine(EffectCoroutine_Item_b_Wandoo(grid, itemClone, row));
    }
    private IEnumerator EffectCoroutine_Item_b_Wandoo(Grid grid, Item itemClone, int idxR)
    {
        int colLen = grid.Data.GetLength(1);
        for (int c = 0; c < colLen; c++)
        {
            itemClone.SetPos(true, grid.ChildGridPartDic[$"{idxR},{c}"].transform.position);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval * 2f);
        }
        DestroyImmediate(itemClone.gameObject);
    }
} // end of class