using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public StoreManager StoreManager = null;
    private Button _button;
    private UIImageGIF uiImageGIF;
 
    private TextMeshProUGUI[] _itemTMPArr = null; //0 : name, 1: price
    [SerializeField]
    private string _itemInfoStr = string.Empty; 
    [SerializeField]
    public Str.eItemCategory ItemCategory = Str.eItemCategory.Normal;
    private void Awake()
    {
        StoreManager = this.GetComponentInParent<StoreManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ClickUIBtn());
        uiImageGIF = this.GetComponentInChildren<UIImageGIF>();
        _itemTMPArr = this.GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void ClickUIBtn()
    {
        if (uiImageGIF != null && uiImageGIF.UseGIF)
        {
            uiImageGIF.IsMoving = true;
            CancelInvoke(nameof(SetGIFFalse));
            Invoke(nameof(SetGIFFalse), 3f);
        }
        if (StoreManager != null)
        { 
            StoreManager.OpenItemDetail(uiImageGIF, this.name, _itemTMPArr[0].text, _itemInfoStr, ItemCategory, _itemTMPArr[1].text);
        }
    }
    private void SetGIFFalse()
    {
        uiImageGIF.IsMoving = false;
    }
} // end of class
