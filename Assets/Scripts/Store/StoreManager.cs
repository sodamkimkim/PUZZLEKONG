using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Str;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    public GameObject ItemDetailUIGo = null;

    #region Image & Name & Info
    [SerializeField]
    private UIImageGIF _itemDetailGIF;
    [SerializeField]
    private TextMeshProUGUI _itemDetail_Name = null;
    [SerializeField]
    private TMP_InputField _itemDetailInputField = null;
    #endregion

    #region Price & BuyCnt
    private string _itemPricePer1UnitStr = string.Empty;
    [SerializeField]
    private Sprite[] _itemPriceSpriteArr = null;
    [SerializeField]
    private Image _itemPriceImage = null;
    [SerializeField]
    private TextMeshProUGUI _itemPrice = null;

    [SerializeField]
    private TMP_Dropdown _buyCntDropdown = null;
    #endregion

    private void Awake()
    {
        _buyCntDropdown.onValueChanged.AddListener(OnBuyCntDropdownValueChanged);
        CloseItemDetail();
    }

    public void OpenItemDetail(string itemUItag, UIImageGIF gifImage, string goName, string itemName,
       string itemInfo, Str.eItemUse itemCategory, string priceStr)
    {
        CloseItemDetail();

        if (_itemDetailGIF.IsItemDetail && gifImage.ItemDetailSpriteArr.Length != 0)
        {
            _itemDetailGIF.MainSprite = gifImage.ItemDetailMainSprite;
        }
        else
            _itemDetailGIF.MainSprite = gifImage.MainSprite;


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

        // ItemInfo
        if (itemInfo != string.Empty)
        {
            _itemDetailInputField.gameObject.SetActive(true);
            _itemDetailInputField.text = itemInfo;
        }
        else
        {
            _itemDetailInputField.gameObject.SetActive(false);
        }

        _itemDetail_Name.text = itemName;
        SetPriceAndBuyCnt(itemUItag, itemCategory, priceStr);

        ItemDetailUIGo.SetActive(true);
    }

    /// <summary>
    /// PriceSprite, Price, BuyCntDropdown setting
    /// </summary>
    /// <param name="itemCategory"></param>
    /// <param name="priceStr"></param>
    private void SetPriceAndBuyCnt(string itemUITag, Str.eItemUse itemCategory, string priceStr)
    {
        _itemPricePer1UnitStr = priceStr;
        SetItemPriceImage(itemCategory);
        _itemPrice.text = priceStr;
        _buyCntDropdown.value = 0;
        if (itemUITag == Str.eItemUITag.UI_Theme.ToString() ||
            itemUITag == Str.eItemUITag.UI_Effect.ToString())
        {
            _buyCntDropdown.interactable = false;
        }
        else if (itemUITag == Str.eItemUITag.UI_Item.ToString())
        {
            _buyCntDropdown.interactable = true;
        }
        else
            _buyCntDropdown.interactable = false;
    }
    private void SetItemPriceImage(Str.eItemUse itemCategory)
    {
        switch (itemCategory)
        {
            case Str.eItemUse.Normal:
                _itemPriceImage.sprite = _itemPriceSpriteArr[0];
                break;
            case Str.eItemUse.HeartEvent:
                _itemPriceImage.sprite = _itemPriceSpriteArr[1];
                break;
        }
    }
    private void OnBuyCntDropdownValueChanged(int value)
    {
        int pricePer1Unit = 0;
        int.TryParse(_itemPricePer1UnitStr, out pricePer1Unit);

        int cnt = 0;
        int.TryParse(_buyCntDropdown.options[value].text, out cnt);

        _itemPrice.text = (pricePer1Unit * cnt).ToString();
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

        SetPriceAndBuyCnt(string.Empty, Str.eItemUse.Normal, string.Empty);
    }

} // end of class