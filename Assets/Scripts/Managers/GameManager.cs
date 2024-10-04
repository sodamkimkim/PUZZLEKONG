using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponentInChildren<PuzzlePlacableChecker>().Init(GameOver, StageComplete);
    }
    private void GameOver()
    {
        // TODO - GameOver Process
        Debug.Log("GameOver");
    }
    private void StageComplete()
    {
        Debug.Log("StageComplete");
    }
} // end of class