using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _itemDetailUIGo = null;
    [SerializeField]
    private UIImageGIF _itemDetailGIF;
    [SerializeField]
    private TextMeshProUGUI _itemDetail_Name = null;
    [SerializeField]
    private TMP_InputField _itemDetailInputField = null;

    private void Awake()
    {
        CloseItemDetail();
    }
    public void OpenItemDetail(UIImageGIF gifImage, string goName, string itemName, string price, string itemInfo)
    {
        CloseItemDetail();

        if (_itemDetailGIF.IsItemDetail&& gifImage.ItemDetailSpriteArr.Length!=0)
        {
            Debug.Log("?");
            _itemDetailGIF.MainSprite = gifImage.ItemDetailMainSprite;
        }
        else
            _itemDetailGIF.MainSprite = gifImage.MainSprite;

        _itemDetail_Name.text = itemName;

        if (gifImage.useItemDetailSpriteArr && gifImage.ItemDetailSpriteArr.Length != 0)
        {
            _itemDetailGIF.SpriteArr = gifImage.ItemDetailSpriteArr;
            _itemDetailGIF.IsMoving = true;
        }
        else if (!gifImage.useItemDetailSpriteArr && gifImage.SpriteArr.Length != 0)
        {// spriteArr은 있고, useItemDetailSpriteArr == false
            _itemDetailGIF.SpriteArr = gifImage.SpriteArr;
            _itemDetailGIF.IsMoving = true;
        }
        else if (gifImage.SpriteArr == null || gifImage.SpriteArr.Length == 0)
        {// spriteArr할당 x 
            _itemDetailGIF.IsMoving = false;
        }

        if (itemInfo != string.Empty)
        {
            _itemDetailInputField.gameObject.SetActive(true);
            _itemDetailInputField.text = itemInfo;
        }
        else
        {
            _itemDetailInputField.gameObject.SetActive(false);
        }

        _itemDetailUIGo.SetActive(true);
    }
    public void CloseItemDetail()
    {
        _itemDetailUIGo.SetActive(false);

        _itemDetailGIF.Image.sprite = null;
        _itemDetailGIF.MainSprite = null;
        _itemDetailGIF.ItemDetailMainSprite = null;

        _itemDetailGIF.SpriteArr = null;
        _itemDetailGIF.ItemDetailMainSprite = null;

        _itemDetail_Name.text = string.Empty;
        _itemDetailInputField.gameObject.SetActive(false);
        _itemDetailInputField.text = string.Empty;

    }
} // end of class