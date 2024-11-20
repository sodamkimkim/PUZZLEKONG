using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _introGo = null;
    [SerializeField]
    private GameObject _lobbyGo = null;
    [SerializeField]
    private GameObject _canvasBottomBtns = null;
    private void Start()
    {
        if (SceneTracker.Instance.IsFirstVisit("1.Lobby"))
        { // 해당씬 첫방문이면
            _lobbyGo.SetActive(false);
            _canvasBottomBtns.SetActive(false);
            _introGo.SetActive(true);

            Invoke(nameof(SetActiveTrueLobbyGo), 4f);
        }
        else
        {
            _introGo.SetActive(false);
            _canvasBottomBtns.SetActive(true);
            _lobbyGo.SetActive(true);

        }
    }
    private void SetActiveTrueLobbyGo()
    {
        _introGo.SetActive(false);

        _canvasBottomBtns.SetActive(true);
        _lobbyGo.SetActive(true);
    }

} // end of class