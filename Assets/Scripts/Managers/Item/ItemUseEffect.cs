using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _effectPrefab_Fire = null;
    public void Effect_Item_a_Mushroom(Grid grid, Item item, int idxC)
    {
        if (grid == null || item == null) return;

        GameObject cloneItemGo = Instantiate(item.gameObject, null);
        Item itemClone = cloneItemGo.GetComponent<Item>();
        itemClone.Anim("Anim1", true);
        itemClone.SetScale(Str.eItemScale.Small, 0.5f);
        StartCoroutine(EffectCoroutine_Item_a_Mushroom(grid, itemClone, idxC));
    }
    private IEnumerator EffectCoroutine_Item_a_Mushroom(Grid grid, Item itemClone, int idxC)
    {
        int rowLen = grid.Data.GetLength(0);
        for (int r = rowLen - 1; r >= 0; r--)
        {
            itemClone.SetPos(true, grid.ChildGridPartDic[$"{r},{idxC}"].transform.position);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
        DestroyImmediate(itemClone.gameObject);
    }
    public void Effect_Item_b_Wandoo(Grid grid, Item item, int idxR)
    {
        if (grid == null || item == null) return;

        GameObject cloneItemGo = Instantiate(item.gameObject, null);
        Item itemClone = cloneItemGo.GetComponent<Item>();
        itemClone.Anim("Anim2", true);
        itemClone.SetScale(Str.eItemScale.Small, 0.5f);
        StartCoroutine(EffectCoroutine_Item_b_Wandoo(grid, itemClone, idxR));
    }
    private IEnumerator EffectCoroutine_Item_b_Wandoo(Grid grid, Item itemClone, int idxR)
    {
        int colLen = grid.Data.GetLength(1);
        for (int c = 0; c < colLen; c++)
        {
            itemClone.SetPos(true, grid.ChildGridPartDic[$"{idxR},{c}"].transform.position);
            yield return new WaitForSeconds(Factor.CompleteCoroutineInterval);
        }
        DestroyImmediate(itemClone.gameObject);
    }
    public void Effect_Item_f_Bumb(Grid grid, Item item)
    {
        if (grid == null || item == null) return;
        GameObject effectGo = Instantiate(_effectPrefab_Fire);
        effectGo.transform.localScale = new Vector3(4f, 4f, 4f);
        effectGo.transform.position = item.gameObject.transform.position;
    }
    public void Effect_Item_g_Eraser(Grid grid, Item item, int idxR, int idxC)
    {
        if (grid == null || item == null) return;
        GameObject cloneItemGo = Instantiate(item.gameObject, null);

        float offsetX = cloneItemGo.transform.lossyScale.x * 0.1f;
        float offsetY = cloneItemGo.transform.lossyScale.y * 0.3f;
        cloneItemGo.transform.position = grid.ChildGridPartDic[$"{idxR},{idxC}"].transform.position + new Vector3(offsetX, offsetY, -1f);

        Item itemClone = cloneItemGo.GetComponent<Item>();
        itemClone.Anim("Anim1", true);
        itemClone.SetScale(Str.eItemScale.Small, 0.5f);
        Destroy(itemClone.gameObject, 1f);
    }

} // end of class