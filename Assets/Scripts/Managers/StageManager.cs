using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    #region Hidden Private Variables
    private static Str.eStage _stage;
    #endregion
    [SerializeField]
    private ADManager _adManager = null;

    #region Stage Buttons
    [SerializeField]
    Button btnStageEvent = null;
    [SerializeField]
    Button btnStageItem = null;
    [SerializeField]
    Button btnStageClassic = null;
    #endregion
    public static Str.eStage Stage { get => _stage; set => _stage = value; }
    public static StageManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (btnStageEvent != null)
            btnStageEvent.onClick.AddListener(() => OnClickBtnStage(Str.eStage.Event));
        if (btnStageItem != null)
            btnStageItem.onClick.AddListener(() => OnClickBtnStage(Str.eStage.Item));
        if (btnStageClassic != null)
            btnStageClassic.onClick.AddListener(() => OnClickBtnStage(Str.eStage.Classic));

    }
    private void OnClickBtnStage(Str.eStage stage)
    {
        if (PlayerData.Kong >= Factor.GamePriceKong)
        { // Kong이 충분
            Stage = stage;
            SceneManager.LoadScene("2.InGame");
            PlayerData.Kong -= Factor.GamePriceKong;
        }
        else
        { // 광고보고 Kong얻기 UI
            _adManager.ShowAskGetKongUI();        
        }
    }
} // end of class