using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject IntroGo = null;
    [SerializeField]
    private GameObject LobbyGo = null;
    private void Start()
    {
        if (SceneTracker.Instance.IsFirstVisit("1.Lobby"))
        {
            // �ش�� ù�湮�̸�
            if (IntroGo.activeSelf == false)
                IntroGo.SetActive(true);

            Invoke(nameof(SetActiveTrueLobbyGo), 4f);
        }
        else
        {
            if (IntroGo.activeSelf == true)
                IntroGo.SetActive(false);
            LobbyGo.SetActive(true);
        }
    }
    private void SetActiveTrueLobbyGo()
    {
        if (IntroGo.activeSelf == true)
            IntroGo.SetActive(false);
        LobbyGo.SetActive(true);
    }

} // end of class