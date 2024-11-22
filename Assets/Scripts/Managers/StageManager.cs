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
    Button btnStageEvent = null;
    [SerializeField]
    Button btnStageItem = null;
    [SerializeField]
    Button btnStageClassic = null;

    public static Str.eStage Stage { get => _stage; set => _stage = value; }
    public static StageManager Instance = null;
    private void Awake()
    {
      //  Stage = Str.eStage.Item;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (btnStageEvent != null)
            btnStageEvent.onClick.AddListener(() =>
            {
                Stage = Str.eStage.Event;
                SceneManager.LoadScene("2.InGame");
            });
        if (btnStageItem != null)
            btnStageItem.onClick.AddListener(() =>
        {
            Stage = Str.eStage.Item;
            SceneManager.LoadScene("2.InGame");
        });
        if (btnStageClassic != null)
            btnStageClassic.onClick.AddListener(() =>
        {
            Stage = Str.eStage.Classic;
            SceneManager.LoadScene("2.InGame");
        });

    } 
} // end of class