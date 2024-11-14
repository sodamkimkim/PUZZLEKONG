using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager = null;
    [SerializeField]
    private PlayerDataManager _playerDataManager = null;
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
    private void GameOverProcess_Timer()
    {
        // 5초 타이머 시작하여 조치 안하면 real gameover 
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        for (int i = 9; i >= 0; i--)
        {
            _uiManager.GameOver_Timer(i.ToString());

            if (_puzzlePlaceManager.SetPuzzlesActive() > 0)
            {
                _uiManager.Panel_GameOver_Timer.SetActive(false);
                StopCoroutine(GameOverCoroutine());
            }

            yield return new WaitForSeconds(1f);
            if (i == 0)
                GameOverProcess_Real();
        }
    }

    private void GameOverProcess_Real()
    {
        // 한번더 검사
        if (_puzzlePlaceManager.SetPuzzlesActive() == 0)
        { // # real gameover
            IsGameOver = true;
            _playerDataManager.UpdateData(_uiManager);
            _playerDataManager.SaveData();
            Debug.Log($"GameOver | {PlayerDataManager.GameData.ToString()}");
        }
        else
        {
            _uiManager.Panel_GameOver_Timer.SetActive(false);
        }
    }
    private void StageCompleteProcess()
    {
        _puzzleManager.LazyStart();
    }
} // end of class