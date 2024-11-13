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
    private void Start()
    {
        //   SetItemSlotColor(ThemaManager.ETheme);
        // Player가 가진 Item 갯수
        PlayerPrefs.SetInt("Item_a_Mushroom", 9999);
        PlayerPrefs.SetInt("Item_b_Wandoo", 9999);
        PlayerPrefs.SetInt("Item_c_Reset", 9999);
        PlayerPrefs.SetInt("Item_d_SwitchHori", 4);
        PlayerPrefs.SetInt("Item_e_SwitchVerti", 2);
        PlayerPrefs.Save();

        // Player가 Slot에 Item 지정
        PlayerPrefs.SetString("ItemSlot0", "Item_b_Wandoo");
        PlayerPrefs.SetString("ItemSlot1", "Item_c_Reset");
        PlayerPrefs.SetString("ItemSlot2", "Item_a_Mushroom");
        PlayerPrefs.SetString("ItemSlot3", "Item_d_SwitchHori");
        PlayerPrefs.SetString("ItemSlot4", "Item_e_SwitchVerti");
        InstantiateItem();
    }
    private void SetItemSlotColor(Enum.eTheme eTheme)
    {
        SpriteRenderer spr = _itemSlotGo.GetComponent<SpriteRenderer>();
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

    public delegate LazyStart LazyStart();
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

        _itemUse.CheckUseable(_puzzleManager, _gridManager.Grid, touchingItem);
    }
    public delegate void SetTouchEndItemReturn();
    // Item에 따른 로직 처리
    // 조건에 맞지 않으면 SetTouchEndItemReturn
    public void PlaceItem(Item dropItem, SetTouchEndItemReturn setTouchEndItemReturnCallback)
    {
        if (TouchRaycast_Item.TouchingItem == null) return;

       
        if (_itemUse.UseItem(_puzzleManager, _gridManager.Grid, dropItem))
        {
            // 아이템 갯수 반영, 남아있으면 돌려보내기
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
            TouchRaycast_Item.TouchingItem.name == "Item_b_Wandoo")
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