using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _itemSlotPosArr = null;

    [SerializeField]
    private GameObject[] _itemPrefabArr = null;

    private void Start()
    {
        // Player가 가진 Item 갯수
        PlayerPrefs.SetInt("Item_a_Mushroom", 1);
        PlayerPrefs.SetInt("Item_b_Wandoo", 1);
        PlayerPrefs.SetInt("Item_c_Reset", 2);
        PlayerPrefs.SetInt("Item_d_SwitchHori", 4);
        PlayerPrefs.SetInt("Item_e_SwitchVerti", 2);
        PlayerPrefs.Save();

        // Player가 Slot에 Item 지정
        PlayerPrefs.SetString("ItemSlot0", "Item_b_Wandoo");
        PlayerPrefs.SetString("ItemSlot1", "Item_c_Reset");
        PlayerPrefs.SetString("ItemSlot2", "Item_a_Mushroom");
        // PlayerPrefs.SetString("ItemSlot1", "Item_b_Wandoo");
        // PlayerPrefs.SetString("ItemSlot2", "Item_c_Reset");
        PlayerPrefs.SetString("ItemSlot3", "Item_d_SwitchHori");
        PlayerPrefs.SetString("ItemSlot4", "Item_e_SwitchVerti");
        InstantiateItem();
    }
    private void InstantiateItem()
    {
        for (int i = 0; i < _itemSlotPosArr.Length; i++)
            InstantiateItemInItemSlot(i, PlayerPrefs.GetString($"ItemSlot{i}"));
    }
    private void InstantiateItemPrefab(ref int slotIdx, int prefabIdx, int itemInt)
    {
        if (itemInt == 0) return;

        GameObject go = Instantiate(_itemPrefabArr[prefabIdx], _itemSlotPosArr[slotIdx].transform);
        go.transform.localPosition = Vector3.zero;
        slotIdx++;
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

        itemGo.transform.localPosition = Vector3.zero;
        Item item = Util.CheckAndAddComponent<Item>(itemGo);
        item.InitialITemSlotPos = _itemSlotPosArr[slotIdx];  
    } // end of class
}