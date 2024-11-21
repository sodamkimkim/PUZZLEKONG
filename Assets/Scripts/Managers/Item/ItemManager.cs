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

    private void Awake()
    {
        _itemSlotGo.SetActive(false);
    }
    private void Start()
    {
        if (StageManager.Stage != Str.eStage.Item) return;
        //   SetItemSlotColor(ThemaManager.ETheme);
        // Player가 가진 Item 갯수

        InstantiateItem();
    }

    public void Init(PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback)
    {
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;
        _itemUse.Init(SetPuzzleStatusData, setPuzzlesActiveCallback);
    }
    private void InstantiateItem()
    {
        _itemSlotGo.SetActive(true);

        for (int i = 0; i < _itemSlotPosArr.Length; i++)
            InstantiateItemInItemSlot(i, PlayerData.GetStr($"ItemSlot{i}"));
    }
    private void InstantiateItemInItemSlot(int slotIdx, string itemStr)
    {
        if (itemStr == string.Empty || itemStr == null) return;
        if (PlayerData.GetInt(itemStr) == 0) return;
        GameObject itemGo = null;
        switch (itemStr)
        {
            case "Item_a_Mushroom":
                itemGo = Instantiate(_itemPrefabArr[0], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_b_Wandoo":
                itemGo = Instantiate(_itemPrefabArr[1], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_c_Reset":
                itemGo = Instantiate(_itemPrefabArr[2], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_d_SwitchRows":
                itemGo = Instantiate(_itemPrefabArr[3], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_e_SwitchColumns":
                itemGo = Instantiate(_itemPrefabArr[4], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_f_Bumb":
                itemGo = Instantiate(_itemPrefabArr[5], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_g_Eraser":
                itemGo = Instantiate(_itemPrefabArr[6], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_h_PushLeft":
                itemGo = Instantiate(_itemPrefabArr[7], _itemSlotPosArr[slotIdx].transform);
                break;
            case "Item_i_PushUp":
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
    // Item에 따른 로직 처리
    // 조건에 맞지 않으면 SetTouchEndItemReturn
    public void PlaceItem(Item dropItem, SetTouchEndItemReturn setTouchEndItemReturnCallback)
    {
        if (TouchRaycast_Item.TouchingItem == null) return;

        if (_itemUse.UseItem(_puzzleManager, _gridManager.Grid, dropItem))
        {

            // 아이템 갯수 반영, 남아있으면 돌려보내기
            PlayerData.SetStr(dropItem.name, (PlayerData.GetInt(dropItem.name) - 1).ToString());
            Debug.Log(dropItem.name + " -- " + PlayerData.GetStr(dropItem.name));
            if (PlayerData.GetInt(dropItem.name) == 0)
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
            TouchRaycast_Item.TouchingItem.name == "Item_b_Wandoo" ||
            TouchRaycast_Item.TouchingItem.name == "Item_d_SwitchRows" ||
            TouchRaycast_Item.TouchingItem.name == "Item_e_SwitchColumns" ||
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