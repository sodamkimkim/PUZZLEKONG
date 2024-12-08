using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    #region Hidden Private variables
    [SerializeField]
    private GameObject _uiTMP_TempText_Large;
    [SerializeField]
    private GameObject _uITMP_TempText_Small;
    [SerializeField]
    private GameObject _uiTMP_TempText_Large_1;

    [SerializeField]
    private GameObject _panel_Gameover_Timer;
    [SerializeField]
    private TextMeshProUGUI _panel_GameOver_Text;
    [SerializeField]
    private GameObject _panel_GameOver;
    [SerializeField]
    private GameObject _uiTMP_TotalScore;

    [SerializeField]
    private TextMeshProUGUI _tmp_NowScore = null;
    [SerializeField]
    private TextMeshProUGUI _tmp_MyBest = null;
    [SerializeField]
    private TextMeshProUGUI _tmp_PlayerTotal = null; 
    #endregion

    #region UI_GameOver
    public GameObject Panel_GameOver_Timer => _panel_Gameover_Timer;
    private TextMeshProUGUI Panel_GameOver_Text => _panel_GameOver_Text;
    public GameObject Panel_GameOver => _panel_GameOver;
    public GameObject UITMP_TotalScore => _uiTMP_TotalScore;
    #endregion

    #region UI_InGame
    public GameObject UITMP_TempText_Large => _uiTMP_TempText_Large;
    public GameObject UITMP_TempText_Small => _uITMP_TempText_Small;
    public GameObject UITMP_TempText_Large_1 => _uiTMP_TempText_Large_1; 
    public TextMeshProUGUI Tmp_NowScore => _tmp_NowScore;
    public TextMeshProUGUI Tmp_MyBest => _tmp_MyBest;
    public TextMeshProUGUI Tmp_PlayerTotal => _tmp_PlayerTotal; 
    #endregion

    private void Awake()
    {
        Panel_GameOver_Timer.SetActive(false);
        Panel_GameOver.SetActive(false);
        UITMP_TotalScore.SetActive(false);
        UITMP_TempText_Large.SetActive(false);
        UITMP_TempText_Large_1.SetActive(false);
    }
    private void Start()
    {
        UpdateHeaderScore();
    }
    public void UpdateHeaderScore()
    {
        Tmp_NowScore.text = $"SCORE : {PlayerData.NowScore.ToString()}";
        if (StageManager.Stage == Str.eStage.Item)
        {
            Tmp_MyBest.text = $"MYBEST : {PlayerData.GetStr(Str.MyBestScore_Item)}";
            Tmp_PlayerTotal.text = $"PLAYER TOTAL : {PlayerData.GetStr(Str.PlayerTotalScore_Item)}";
        }
        else
        {
            Tmp_MyBest.text = $"MYBEST : {PlayerData.GetStr(Str.MyBestScore_Classic)}";
            Tmp_PlayerTotal.text = $"PLAYER TOTAL : {PlayerData.GetStr(Str.PlayerTotalScore_Classic)}";
        }

    }
    public void GameOver_Timer(string text)
    {
        if (Panel_GameOver_Timer.activeSelf == false) Panel_GameOver_Timer.SetActive(true);
        _panel_GameOver_Text.text = text;
    }
    public void GameOver(string text)
    {
        Panel_GameOver.SetActive(true);
        SetTMPText(UITMP_TotalScore, text, Color.white, false);
    }
    public void SetTMPText(GameObject uiGo, string text, Color color, bool lazyClose)
    {
        StopCoroutine(nameof(UISetActiveFalse));

        TextMeshProUGUI tmpro = uiGo.GetComponent<TextMeshProUGUI>();
        if (tmpro == null) return;

        if (uiGo.activeSelf == false)
            uiGo.SetActive(true);
        tmpro.text = text;
        tmpro.color = color;

        if (lazyClose)
        {
            StopCoroutine(nameof(UISetActiveFalse));
            StartCoroutine(UISetActiveFalse(uiGo, 1f));
        }
    }
    IEnumerator UISetActiveFalse(GameObject uiGo, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (uiGo.activeSelf == true)
            uiGo.SetActive(false);
    } 
} // end of class