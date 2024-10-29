using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    #region Hidden Private variables
    [SerializeField]
    private GameObject _uITMP_TempText_Large;
    [SerializeField]
    private GameObject _uITMP_TempText_Small;

    [SerializeField]
    private GameObject _panel_GameOver;
    [SerializeField]
    private GameObject _uiTMP_TotalScore;
    #endregion

    #region UI_GameOver
    public GameObject Panel_GameOver => _panel_GameOver;
    public GameObject UITMP_TotalScore => _uiTMP_TotalScore;
    #endregion

    #region UI_InGame
    public GameObject UITMP_TempText_Large => _uITMP_TempText_Large;
    public GameObject UITMP_TempText_Small => _uITMP_TempText_Small;
    #endregion
    private void Awake()
    {
        Panel_GameOver.SetActive(false);
        UITMP_TotalScore.SetActive(false);
        UITMP_TempText_Large.SetActive(false);
    }
    public void GameOver(string text)
    {
        Panel_GameOver.SetActive(true);
        SetTMPText(UITMP_TotalScore, text, Color.white, false);
    }
    public void SetTMPText(GameObject uiGo, string text, Color color, bool lazyClose)
    {
        TextMeshProUGUI tmpro = uiGo.GetComponent<TextMeshProUGUI>();
        if (tmpro == null) return;

        if (uiGo.activeSelf == false)
            uiGo.SetActive(true);
        tmpro.text = text;
        tmpro.color = color;

        if (lazyClose)
            StartCoroutine(UISetActiveFalse(uiGo, 1f));
    }
    IEnumerator UISetActiveFalse(GameObject uiGo, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (uiGo.activeSelf == true)
            uiGo.SetActive(false);
    }
} // end of class