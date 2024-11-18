using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GridManager _gridManager = null;
    [SerializeField]
    private PuzzleManager _puzzleManager = null;
    [SerializeField]
    private ItemUse _itemUse = null;

    [SerializeField]
    private GameObject _itemSlotGo = null;

    [SerializeField]
    private GameObject[] _itemSlotPosArr = null;

    [SerializeField]
    private GameObject[] _itemPrefabArr = null;
    public static bool IsProcessing = false;
    public PuzzleManager.SetPuzzleActive SetPuzzlesActiveCallback;

    public delegate LazyStart LazyStart();
    public delegate void SetTouchEndItemReturn();
    private void Start()
    {
        //   SetItemSlotColor(ThemaManager.ETheme);
        // Player�� ���� Item ����
        PlayerPrefs.SetInt("Item_a_Mushroom", 9999);
        PlayerPrefs.SetInt("Item_b_Wandoo", 9999);
        PlayerPrefs.SetInt("Item_c_Reset", 9999);
        PlayerPrefs.SetInt("Item_d_SwitchRows", 9999);
        PlayerPrefs.SetInt("Item_e_SwitchColumns", 9999);
        PlayerPrefs.SetInt("Item_f_Bumb", 9999);
        PlayerPrefs.SetInt("Item_g_Eraser", 9999);
        PlayerPrefs.SetInt("Item_h_PushLeft", 9999);
        PlayerPrefs.SetInt("Item_i_PushUp", 9999);
        PlayerPrefs.Save();

        // Player�� Slot�� Item ����
        PlayerPrefs.SetString("ItemSlot0", "Item_e_SwitchColumns");
        PlayerPrefs.SetString("ItemSlot1", "Item_c_Reset");
        PlayerPrefs.SetString("ItemSlot2", "Item_a_Mushroom");
        //PlayerPrefs.SetString("ItemSlot3", "Item_d_SwitchRows");
        //PlayerPrefs.SetString("ItemSlot4", "Item_e_SwitchColumns");
        PlayerPrefs.SetString("ItemSlot3", "Item_f_Bumb");
        PlayerPrefs.SetString("ItemSlot4", "Item_g_Eraser");
        PlayerPrefs.SetString("ItemSlot5", "Item_h_PushLeft");
        PlayerPrefs.SetString("ItemSlot6", "Item_i_PushUp");
        PlayerPrefs.SetString("ItemSlot7", "Item_d_SwitchRows");
        InstantiateItem();
    }

    public void Init(PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback)
    {
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;
        _itemUse.Init(SetPuzzleStatusData, setPuzzlesActiveCallback);
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
            case "Item_d_SwitchRows":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[3], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_e_SwitchColumns":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[4], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_f_Bumb":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[5], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_g_Eraser":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[6], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_h_PushLeft":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[7], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_i_PushUp":
                if (PlayerPrefs.GetInt(itemStr) == 0) return;
                itemGo = Instantiate(_itemPrefabArr[8], _itemSlotPosArr[slotIdx].transform);
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
        _itemUse.CheckUseable(_puzzleManager, _gridManager.Grid, touchingItem);
    }
    // Item�� ���� ���� ó��
    // ���ǿ� ���� ������ SetTouchEndItemReturn
    public void PlaceItem(Item dropItem, SetTouchEndItemReturn setTouchEndItemReturnCallback)
    {
        if (TouchRaycast_Item.TouchingItem == null) return;
         
        if (_itemUse.UseItem(_puzzleManager, _gridManager.Grid, dropItem))
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
        IsProcessing = false;
    }
    public void CheckUseableReset()
    {
        if (TouchRaycast_Item.TouchingItem == null) return;
        if (TouchRaycast_Item.TouchingItem.name == "Item_a_Mushroom" ||
            TouchRaycast_Item.TouchingItem.name == "Item_b_Wandoo"||
            TouchRaycast_Item.TouchingItem.name == "Item_d_SwitchRows"||
            TouchRaycast_Item.TouchingItem.name == "Item_e_SwitchColumns"||
            TouchRaycast_Item.TouchingItem.name == "Item_f_Bumb" ||
            TouchRaycast_Item.TouchingItem.name == "Item_g_Eraser" ||
            TouchRaycast_Item.TouchingItem.name == "Item_h_PushLeft" ||
            TouchRaycast_Item.TouchingItem.name == "Item_i_PushUp")
        {
            foreach (KeyValuePair<string, GridPart> kvp in _gridManager.Grid.ChildGridPartDic)
            {
                if (kvp.Value.Data == Factor.UseItem1)
                {
                    kvp.Value.Data = Factor.HasPuzzle;
                    kvp.Value.SetGridPartColor();
                }
            }
        }
        else if (TouchRaycast_Item.TouchingItem.name == "Item_c_Reset")
            SetPuzzleStatusData(Factor.PuzzleStatus_ItemNormal);
    }
    public void SetPuzzleStatusData(int puzzleStatusFactor)
    {
        if (_puzzleManager.PuzzleGoArr == null) return;
        foreach (GameObject puzzleGo in _puzzleManager.PuzzleGoArr)
        {
            if (puzzleGo == null) continue;
            Puzzle puzzle = puzzleGo.GetComponent<Puzzle>();
            if (puzzle == null /*|| puzzle.ActiveSelf == false*/) continue;

            puzzle.ItemStatusData = puzzleStatusFactor;
            if (puzzle.ActiveSelf == false)
            {
                if (puzzleStatusFactor == Factor.PuzzleStatus_ItemUse)
                    puzzle.SetChildColor(puzzle.ItemUseColor);
                else
                    puzzle.SetChildColor(puzzle.NotActiveColor);
            }
        }
    }
} // end of class