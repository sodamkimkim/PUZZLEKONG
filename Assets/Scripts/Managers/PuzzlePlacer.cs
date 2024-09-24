using UnityEngine;

public class PuzzlePlacer : MonoBehaviour
{
    private GridSpawner _gridSpawner = null;
    private PuzzleSpawner _puzzleSpawner = null;
    private void Awake()
    {
        _gridSpawner = this.GetComponent<GridSpawner>();
        _puzzleSpawner = this.GetComponent<PuzzleSpawner>();
    }
    private void Start()
    {
        LazyStart();
    }

    private void LazyStart()
    {
        if (GridSpawner.IsGridGoReady && _puzzleSpawner.HasPuzzlCheck() != 0)
        {
            Debug.Log("LazyStart"); 
        }
    }

} // end of class