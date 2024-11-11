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
    private void Awake()
    {
        _puzzlePlaceManager.PuzzlePlacableChecker.Init(GameOverProcess, StageCompleteProcess);
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
    private void GameOverProcess()
    { 
        _playerDataManager.UpdateData(_uiManager);
        _playerDataManager.SaveData();
        Debug.Log($"GameOver | {PlayerDataManager.GameData.ToString()}");
    }
    private void StageCompleteProcess()
    {
        _puzzleManager.LazyStart();
    }
} // end of class