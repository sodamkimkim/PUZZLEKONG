using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GridManager _gridManager = null;
    [SerializeField]
    private PuzzleManager _puzzleManager = null;
    [SerializeField]
    private PuzzlePlaceManager _puzzlePlaceManager = null;
    [SerializeField]
    private CompleteManager _completeManager = null;
    private void Awake()
    {
        _puzzlePlaceManager.PuzzlePlacableChecker.Init(GameOver, StageComplete);
    }
    private void Start()
    {
        LazyStart();
    }
    private void LazyStart()
    {
        _gridManager.LazyStart();
        _puzzleManager.LazyStart();
        _completeManager.Complete(_puzzlePlaceManager.CheckPlacableAllRemainingPuzzles);
    }
    private void GameOver()
    {
        // TODO - GameOver Process
        Debug.Log("GameOver");
    }
    private void StageComplete()
    {
        _puzzleManager.LazyStart();
    }
} // end of class