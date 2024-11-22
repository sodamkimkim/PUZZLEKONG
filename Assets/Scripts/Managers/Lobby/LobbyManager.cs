using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _introGo = null;
    [SerializeField]
    private GameObject _homeGo = null;
    [SerializeField]
    private GameObject _storeGo = null;
    [SerializeField]
    private GameObject _myGo = null;

    #region BottomBtns
    [SerializeField]
    private GameObject _canvasBottomBtns = null;
    [SerializeField]
    private Button _btnHome = null;
    [SerializeField]
    private Button _btnStore = null;
    [SerializeField]
    private Button _btnMy = null;
    #endregion

    private void Awake()
    {
      DontDestroyOnLoad(_canvasBottomBtns);
        SetActiveAllMenuGos(false);
        _canvasBottomBtns.SetActive(false);

        _btnHome.onClick.AddListener(SetActiveTrue_LobbyGo);
        _btnStore.onClick.AddListener(SetActiveTrue_StoreGo);
        _btnMy.onClick.AddListener(SetActiveTrue_MyGo);
    }
    private void Start()
    {
        if (SceneTracker.Instance.IsFirstVisit("1.Lobby"))
        { // 해당씬 첫방문이면
            _canvasBottomBtns.SetActive(false);
            _introGo.SetActive(true);

            Invoke(nameof(SetActiveTrue_LobbyGo), 4f);
        }
        else
        {
            _introGo.SetActive(false);
            _canvasBottomBtns.SetActive(true);
            _homeGo.SetActive(true);

        }
    }
    private void SetActiveAllMenuGos(bool activeSelf)
    {
        _introGo.SetActive(activeSelf);
        _homeGo.SetActive(activeSelf);
        _storeGo.SetActive(activeSelf);
        _myGo.SetActive(activeSelf);
    }
    private void SetActiveTrue_LobbyGo()
    {
        SetActiveAllMenuGos(false);
        _canvasBottomBtns.SetActive(true);
        _homeGo.SetActive(true);
    }
    private void SetActiveTrue_StoreGo()
    {
        SetActiveAllMenuGos(false);
        _canvasBottomBtns.SetActive(true);
        _storeGo.SetActive(true);
    }
    private void SetActiveTrue_MyGo()
    {
        SetActiveAllMenuGos(false);
        _canvasBottomBtns.SetActive(true);
        _myGo.SetActive(true);
    }
} // end of class