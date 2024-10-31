using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject IntroGo = null;
    [SerializeField]
    private GameObject LobbyGo = null;
    private void Start()
    {
        if (SceneTracker.Instance.IsFirstVisit("1.Lobby"))
        { // 해당씬 첫방문이면
            if (LobbyGo.activeSelf == true)
                LobbyGo.SetActive(false);

            if (IntroGo.activeSelf == false)
                IntroGo.SetActive(true);

            Invoke(nameof(SetActiveTrueLobbyGo), 4f);
        }
        else
        {
            if (IntroGo.activeSelf == true)
                IntroGo.SetActive(false);
            if (LobbyGo.activeSelf == false)
                LobbyGo.SetActive(true);

        }
    }
    private void SetActiveTrueLobbyGo()
    {
        if (IntroGo.activeSelf == true)
            IntroGo.SetActive(false);
        LobbyGo.SetActive(true);
    }
    public void SwitchSceneToInGame()
    {
        SceneManager.LoadScene("3.InGame");
    }
} // end of class