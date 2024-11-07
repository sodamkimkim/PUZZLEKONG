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

    private void Awake()
    {
        PlayerPrefs.SetInt("Item_a_Mushroom", 1);
        PlayerPrefs.SetInt("Item_b_Wandoo", 1);
        PlayerPrefs.SetInt("Item_c_Reset", 2);
        PlayerPrefs.SetInt("Item_d_SwitchHori", 4);
        PlayerPrefs.SetInt("Item_e_SwitchVerti", 2);
        PlayerPrefs.Save();
    }
    private void Start()
    {
         InstantiateItem();
        //_itemPrefabArr[1].GetComponent<Animator>().SetBool("Anim2", true);
        //_itemPrefabArr[2].GetComponent<Animator>().SetBool("Anim1", true);
        //_itemPrefabArr[3].GetComponent<Animator>().SetBool("Anim1", true);
        //_itemPrefabArr[4].GetComponent<Animator>().SetBool("Anim1", true);
    }
    private void InstantiateItem()
    {
        int slotIdx = 0;
        InstantiateItemPrefab(ref slotIdx, 0, PlayerPrefs.GetInt("Item_a_Mushroom"));
        InstantiateItemPrefab(ref slotIdx, 1, PlayerPrefs.GetInt("Item_b_Wandoo"));
        InstantiateItemPrefab(ref slotIdx, 2, PlayerPrefs.GetInt("Item_c_Reset"));
        InstantiateItemPrefab(ref slotIdx, 3, PlayerPrefs.GetInt("Item_d_SwitchHori"));
        InstantiateItemPrefab(ref slotIdx, 4, PlayerPrefs.GetInt("Item_e_SwitchVerti"));
    }
    private void InstantiateItemPrefab(ref int slotIdx, int prefabIdx, int itemInt)
    {
        if (itemInt == 0) return;

        GameObject go = Instantiate(_itemPrefabArr[prefabIdx], _itemSlotPosArr[slotIdx].transform);
        go.transform.localPosition = Vector3.zero;
        slotIdx++;
    }
} // end of class
