using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ±¤°í°ü¸®
/// </summary>
public class ADManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PanAskGetKong = null;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (PanAskGetKong != null)
        {
            Button[] btns = PanAskGetKong.GetComponentsInChildren<Button>();
            btns[0].onClick.AddListener(() => ShowAD());
            btns[1].onClick.AddListener(() => PanAskGetKong.SetActive(false));

            if (PanAskGetKong.activeSelf == true)
                PanAskGetKong.SetActive(false);
        }
    }

    // ±¤°íº¸°í Get a kongÇÒ°ÇÁö ¹¯´Â UI
    public void ShowAskGetKongUI()
    {
        PanAskGetKong.SetActive(true);
    }
    /// <summary>
    /// Show µ¿¿µ»ó±¤°í 
    /// </summary>
    public void ShowAD()
    {
        // TODO - ±¤°íº¸¿©ÁÖ±â
        // +1Äá
    }
} // end of class