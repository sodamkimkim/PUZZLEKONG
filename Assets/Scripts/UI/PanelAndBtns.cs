using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAndBtns : MonoBehaviour
{
    public StoreManager StoreManager = null;
    [SerializeField]
    private Button[] _btnArr = null;
    [SerializeField]
    private GameObject[] _panelArr = null;
    [SerializeField]
    private Scrollbar[] _barArr = null;
    private void Awake()
    {
        StoreManager = this.GetComponentInParent<StoreManager>();
    }
    private void OnEnable()
    {
        for (int i = 0; i < _btnArr.Length; i++)
        {
            int ii = i;
            _btnArr[ii].onClick.AddListener(() => OpenPanel(ii));
        }
        OpenPanel(0);
    }
    private void OpenPanel(int idx)
    {
        for (int i = 0; i < _panelArr.Length; i++)
        {
            if (i == idx) continue;
            _btnArr[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f);
            _btnArr[i].interactable = true;
            _panelArr[i].SetActive(false);
        }
        if (StoreManager.ItemDetailUIGo.activeSelf == true)
            StoreManager.CloseItemDetail();

        _btnArr[idx].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        _btnArr[idx].interactable = false;
        _panelArr[idx].SetActive(true);
        _barArr[idx].value = 1;
    }
} // end of class