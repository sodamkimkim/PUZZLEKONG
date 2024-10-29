using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    private void Awake()
    {
        _puzzlePlaceManager.PuzzlePlacableChecker.Init(GameOverProcess, StageCompleteProcess);
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
        _completeManager.Complete(_puzzlePlaceManager.CheckPlacableAllRemainingPuzzles);
    }
    private void GameOverProcess()
    {
        // TODO - GameOver Process
        _playerDataManager.UpdateData();
        _playerDataManager.SaveData();
        Debug.Log($"GameOver | {PlayerDataManager.GameData.ToString()}");
    }
    private void StageCompleteProcess()
    {
        _puzzleManager.LazyStart();
    }
} // end of class