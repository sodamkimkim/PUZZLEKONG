using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    public GameObject ItemDetailUIGo = null;
    [SerializeField]
    private UIImageGIF _itemDetailGIF;
    [SerializeField]
    private TextMeshProUGUI _itemDetail_Name = null;
    [SerializeField]
    private TMP_InputField _itemDetailInputField = null;


    [SerializeField]
    private Sprite[] _itemPriceSpriteArr = null;
    [SerializeField]
    private Image _itemPriceImage = null;
    [SerializeField]
    private TextMeshProUGUI _itemPrice = null;
    private void Awake()
    {
        CloseItemDetail();
    }
    public void OpenItemDetail(UIImageGIF gifImage, string goName, string itemName, string price, string itemInfo, Str.eItemCategory itemCategory,string priceStr)
    {
        CloseItemDetail();

        if (_itemDetailGIF.IsItemDetail && gifImage.ItemDetailSpriteArr.Length != 0)
        {
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

        SetItemPriceImage(itemCategory);
        _itemPrice.text = priceStr;
        ItemDetailUIGo.SetActive(true);
    }
    private void SetItemPriceImage(Str.eItemCategory itemCategory)
    {
        switch (itemCategory)
        {
            case Str.eItemCategory.Normal:
                _itemPriceImage.sprite = _itemPriceSpriteArr[0];
                break;
            case Str.eItemCategory.HeartEvent:
                _itemPriceImage.sprite = _itemPriceSpriteArr[1];
                break;
        }
    }
    public void CloseItemDetail()
    {
        ItemDetailUIGo.SetActive(false);

        _itemDetailGIF.Image.sprite = null;
        _itemDetailGIF.MainSprite = null;
        _itemDetailGIF.ItemDetailMainSprite = null;

        _itemDetailGIF.SpriteArr = null;
        _itemDetailGIF.ItemDetailMainSprite = null;

        _itemDetail_Name.text = string.Empty;
        _itemDetailInputField.gameObject.SetActive(false);
        _itemDetailInputField.text = string.Empty;

        _itemPriceImage.sprite = null;
        _itemPrice.text = string.Empty;
    }
} // end of class