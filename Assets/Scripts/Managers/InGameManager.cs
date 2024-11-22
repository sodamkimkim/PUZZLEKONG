using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager = null;
    [SerializeField]
    private GridManager _gridManager = null;
    [SerializeField]
    private PuzzleManager _puzzleManager = null;
    [SerializeField]
    private PuzzlePlaceManager _puzzlePlaceManager = null;
    [SerializeField]
    private CompleteManager _completeManager = null;
    [SerializeField]
    private EffectManager _effectManager = null;
    [SerializeField]
    private ItemManager _itemManager = null;
    public static bool IsGameOver = false;
    private void Awake()
    {
        _puzzlePlaceManager.PuzzlePlacableChecker.Init(GameOverProcess_Timer, StageCompleteProcess);
        _completeManager.Init(_puzzlePlaceManager.SetPuzzlesActive);
        _itemManager.Init(_puzzlePlaceManager.SetPuzzlesActive);
    }
    private void Start()
    {
        LazyStart();
    }
    private void LazyStart()
    {
        _effectManager.LazyStart();
        _gridManager.LazyStart();
        _puzzleManager.LazyStart();
        _completeManager.Complete(_puzzlePlaceManager.CheckStageCompleteOrGameOver);
    }
    private void OnApplicationQuit()
    {
        UpdatePlayerData(false);
    }
    private void OnDisable()
    {
        UpdatePlayerData(false);
    }
    public   void ReturnToHome()
    {
        SceneManager.LoadScene("1.Lobby");
    }
    private void GameOverProcess_Timer()
    {
        if (_puzzlePlaceManager.SetPuzzlesActive() > 0) return;

        // 타이머 시작하여 조치 안하면 real gameover 
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        for (int i = 7; i >= 0; i--)
        {
            if (_puzzlePlaceManager.SetPuzzlesActive() > 0)
            {
                _uiManager.Panel_GameOver_Timer.SetActive(false);
                StopCoroutine(GameOverCoroutine());
            }
            else
            {
                _uiManager.GameOver_Timer(i.ToString());
                yield return new WaitForSeconds(1f);
                if (i == 0)
                    GameOverProcess_Real();
            }
        }
    }

    private void GameOverProcess_Real()
    {
        // 한번더 검사
        if (_puzzlePlaceManager.SetPuzzlesActive() == 0)
        { // # real gameover
            IsGameOver = true;
            UpdatePlayerData(true);
            Debug.Log($"GameOver | {PlayerData.ToString()}");
        }
        else
        {
            _uiManager.Panel_GameOver_Timer.SetActive(false);
        }
    }
    private void UpdatePlayerData(bool isNeedCelebration)
    { 
        if (PlayerData.MyBestScore == PlayerData.NowScore)
        {
            Debug.Log("BestScore 갱신");
            if (isNeedCelebration)
            {
                _uiManager.SetTMPText(_uiManager.UITMP_TempText_Large, "YOUR\nBEST SCORE!", Color.white, false);
                Instantiate(_effectManager.EffectPrefab_Celebration_Finish, Factor.EffectPos_Celebration, Quaternion.identity);
            }
            PlayerData.NowScore = 0;
        }

        if (isNeedCelebration) _uiManager.GameOver($"Score: {PlayerData.NowScore}\nMyBest: {PlayerData.MyBestScore}\nMyTotal: {PlayerData.PlayerTotalScore}");
    }
    private void StageCompleteProcess()
    {
        _puzzleManager.LazyStart();
    }
} // end of class