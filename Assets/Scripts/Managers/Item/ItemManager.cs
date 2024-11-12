using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GridManager _gridManager = null; 
    [SerializeField]
    private ItemUse _itemUse = null;

    [SerializeField]
    private GameObject _itemSlotGo = null;

    [SerializeField]
    private GameObject[] _itemSlotPosArr = null;

    [SerializeField]
    private GameObject[] _itemPrefabArr = null;

    private void Start()
    {
     //   SetItemSlotColor(ThemaManager.ETheme);
        // Player�� ���� Item ����
        PlayerPrefs.SetInt("Item_a_Mushroom", 2);
        PlayerPrefs.SetInt("Item_b_Wandoo", 1);
        PlayerPrefs.SetInt("Item_c_Reset", 2);
        PlayerPrefs.SetInt("Item_d_SwitchHori", 4);
        PlayerPrefs.SetInt("Item_e_SwitchVerti", 2);
        PlayerPrefs.Save();

        // Player�� Slot�� Item ����
        PlayerPrefs.SetString("ItemSlot0", "Item_b_Wandoo");
        PlayerPrefs.SetString("ItemSlot1", "Item_c_Reset");
        PlayerPrefs.SetString("ItemSlot2", "Item_a_Mushroom");
        PlayerPrefs.SetString("ItemSlot3", "Item_d_SwitchHori");
        PlayerPrefs.SetString("ItemSlot4", "Item_e_SwitchVerti");
        InstantiateItem();
    }
    private void SetItemSlotColor(Enum.eTheme eTheme)
    {
        SpriteRenderer spr =  _itemSlotGo.GetComponent<SpriteRenderer>();
        switch (eTheme)
        {
            case Enum.eTheme.Grey:
                spr.color = Factor.Grey0;
                break;
            case Enum.eTheme.Green:
                spr.color = Factor.Green0;
                break;
            case Enum.eTheme.LightPurple:
                spr.color = Factor.LightPurple0;
                break;
            case Enum.eTheme.LightBlue:
                spr.color = Factor.LightBlue0;
                break;
            case Enum.eTheme.Pink:
                spr.color = Factor.Pink0;
                break;
            case Enum.eTheme.Yellow:
                spr.color = Factor.Yellow0;
                break;
            case Enum.eTheme.Mint:
                spr.color = Factor.Grey0;
                break;
        }
    }

    public PuzzleManager.SetPuzzleActive SetPuzzlesActiveCallback;

    public void Init(PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback)
    {
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;
        _itemUse.Init(setPuzzlesActiveCallback);
    }
    private void InstantiateItem()
    {
        for (int i = 0; i < _itemSlotPosArr.Length; i++)
            InstantiateItemInItemSlot(i, PlayerPrefs.GetString($"ItemSlot{i}"));
    }
    private void InstantiateItemInItemSlot(int slotIdx, string itemStr)
    {
        if (itemStr == string.Empty || itemStr == null) return;

        GameObject itemGo = null;
        switch (itemStr)
        {
            case "Item_a_Mushroom":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[0], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_b_Wandoo":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[1], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_c_Reset":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[2], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_d_SwitchHori":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[3], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_e_SwitchVerti":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[4], _itemSlotPosArr[slotIdx].transform);
                break;
        }

        if (itemGo == null) return;

        itemGo.name = itemGo.name.Replace("(Clone)", "");
        Item item = Util.CheckAndAddComponent<Item>(itemGo);
        item.SetPos(false, Vector3.zero);
        item.IsForEffect = false;
        item.InitialItemSlotPos = _itemSlotPosArr[slotIdx];
    }

    public void CheckUseable(Item touchingItem)
    {
        if (touchingItem.TriggeredGridPartIdxStr == string.Empty) return;
        _itemUse.CheckUseable(_gridManager.Grid, touchingItem);
    }
    public delegate void SetTouchEndItemReturn();
    // Item�� ���� ���� ó��
    // ���ǿ� ���� ������ SetTouchEndItemReturn
    public void PlaceItem(Item dropItem, SetTouchEndItemReturn setTouchEndItemReturnCallback)
    {
        if (TouchRaycast2D_Item.TouchingItem == null) return;

        if (_itemUse.UseItem(_gridManager.Grid, dropItem))
        {
            // ������ ���� �ݿ�, ���������� ����������
            PlayerPrefs.SetInt(dropItem.name, PlayerPrefs.GetInt(dropItem.name) - 1);
            if (PlayerPrefs.GetInt(dropItem.name) == 0)
                DestroyImmediate(dropItem.gameObject);
            else
                setTouchEndItemReturnCallback?.Invoke();
        }
        else
            setTouchEndItemReturnCallback?.Invoke();

    }
    public void CheckUseableReset()
    {
        foreach (KeyValuePair<string, GridPart> kvp in _gridManager.Grid.ChildGridPartDic)
        {
            if (kvp.Value.Data == Factor.Item1_MushroomAndWandoo)
            {
                kvp.Value.Data = Factor.HasPuzzle;
                kvp.Value.SetGridPartColor();
            }
        }
    }
} // end of class