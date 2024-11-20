using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    #region Hidden Private Variables
    private static Enum.eStage _stage;
    #endregion

    [SerializeField]
    Button btnStageEvent = null;
    [SerializeField]
    Button btnStageItem = null;
    [SerializeField]
    Button btnStageClassic = null;

    public static Enum.eStage Stage { get => _stage; set => _stage = value; }
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
            btnStageEvent.onClick.AddListener(() =>
            {
                Stage = Enum.eStage.Event;
                SceneManager.LoadScene("2.InGame");
            });
        if (btnStageItem != null)
            btnStageItem.onClick.AddListener(() =>
        {
            Stage = Enum.eStage.Item;
            SceneManager.LoadScene("2.InGame");
        });
        if (btnStageClassic != null)
            btnStageClassic.onClick.AddListener(() =>
        {
            Stage = Enum.eStage.Classic;
            SceneManager.LoadScene("2.InGame");
        });

    } 
} // end of class