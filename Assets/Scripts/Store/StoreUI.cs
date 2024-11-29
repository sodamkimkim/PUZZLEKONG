using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    private Button _button;
    private UIImageGIF uiImageGIF;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => UIBtnClick());
        uiImageGIF = this.GetComponentInChildren<UIImageGIF>();
    }
    private void UIBtnClick()
    {
        if (uiImageGIF != null)
        {
            uiImageGIF.IsMoving = true;
            CancelInvoke(nameof(SetGIFFalse));
            Invoke(nameof(SetGIFFalse), 3f);
        }
    }
    private void SetGIFFalse()
    {
        uiImageGIF.IsMoving = false;
    }
} // end of class
