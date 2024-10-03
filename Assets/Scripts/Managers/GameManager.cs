using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponentInChildren<PuzzlePlacableChecker>().Init(GameOver);    
    }
    public void GameOver()
    {
        // GameOver Process
        Debug.Log("GameOver");
    }
} // end of class