using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _uITMP_TempText_Large;
    [SerializeField]
    private GameObject _uITMP_TempText_Small;

    #region UI

    public GameObject UITMP_TempText_Large { get => _uITMP_TempText_Large; }
    public GameObject UITMP_TempText_Small { get => _uITMP_TempText_Small; }
    #endregion 

    private void Awake()
    {
        UITMP_TempText_Large.SetActive(false);
    }
    public void SetText(GameObject uiGo, string text)
    {
        TextMeshProUGUI tmpro = uiGo.GetComponent<TextMeshProUGUI>();
        if (tmpro == null) return;

        if (uiGo.activeSelf == false)
            uiGo.SetActive(true);
        tmpro.text = text;
        StartCoroutine(UISetActiveFalse(uiGo, 1f));
    }
    IEnumerator UISetActiveFalse(GameObject uiGo, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (uiGo.activeSelf == true)
            uiGo.SetActive(false);
    }
} // end of class