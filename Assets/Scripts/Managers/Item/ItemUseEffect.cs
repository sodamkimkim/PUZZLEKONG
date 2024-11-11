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
        itemClone.Anim("Anim1", true); // item에 Anim 만들기
        StartCoroutine(EffectCoroutine_Item_a_Mushroom(grid, itemClone, col));
    }
    private IEnumerator EffectCoroutine_Item_a_Mushroom(Grid grid, Item item, int col)
    {
        int rowLen = grid.Data.GetLength(0);
        item.SetScale(Enum.eItemScale.Small, 0.3f);
        for (int r = rowLen - 1; r >= 0; r--)
        {
            item.SetPos(true, grid.ChildGridPartDic[$"{r},{col}"].transform.position);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval * 2f);
        }
        DestroyImmediate(item.gameObject);
    }
} // end of class