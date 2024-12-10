using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public StoreManager StoreManager = null;
    private Button _button;
    private Image _itemMainImage = null;
    private UIImageGIF _uiImageGIF = null;

    private TextMeshProUGUI[] _itemTMPArr = new TextMeshProUGUI[2]; //0 : name, 1: price
    [SerializeField]
    private string _itemInfoStr = string.Empty;
    [SerializeField]
    public Str.eItemUse ItemCategory = Str.eItemUse.Normal;
    private void Awake()
    {
        StoreManager = this.GetComponentInParent<StoreManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnClickItemUIBtn());

        // gif클래스 검색
        UIImageGIF[] uiImageGIFArr = this.GetComponentsInChildren<UIImageGIF>();
        if (uiImageGIFArr != null)
        {
            foreach (UIImageGIF gif in uiImageGIFArr)
            {
                if (gif.gameObject.name.Equals("Image_Item"))
                    _uiImageGIF = gif;
            }
        }
        // item main image 검색
        Image itemImage = this.transform.Find("Image_Item").GetComponent<Image>();
        if (itemImage != null)
            _itemMainImage = itemImage;

        // 아이템이름, 가격 tmp검색
        TextMeshProUGUI[] tmpArr = this.GetComponentsInChildren<TextMeshProUGUI>();
        if (tmpArr != null)
        {
            foreach (TextMeshProUGUI tmp in tmpArr)
            {
                if (tmp.gameObject.name.Equals("TMP_ItemName"))
                    _itemTMPArr[0] = tmp;
                if (tmp.gameObject.name.Equals("TMP_Price"))
                    _itemTMPArr[1] = tmp;
            }
        }
    }
    private void OnClickItemUIBtn()
    {
        if (_uiImageGIF != null && _uiImageGIF.UseGIF)
        {
            _uiImageGIF.IsMoving = true;
            CancelInvoke(nameof(SetGIFFalse));
            Invoke(nameof(SetGIFFalse), 3f);
        }
        if (this.name == "UI_ETC_ADFORKONG")
        {
            // 광고보고 1콩얻기
        }
        else if (StoreManager != null)
        {
            StoreManager.OpenItemDetail(this.gameObject.tag, this.name, _itemMainImage, _uiImageGIF, _itemTMPArr[0].text, _itemInfoStr, ItemCategory, _itemTMPArr[1].text);
        }

    }
    private void SetGIFFalse()
    {
        // GIF 이미지 동작중 False
        _uiImageGIF.IsMoving = false;
    }
} // end of class
