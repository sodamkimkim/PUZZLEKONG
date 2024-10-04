using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-8)]
public class PuzzleManager : MonoBehaviour
{
    #region Hidden Private Variables
    private GameObject[] _puzzleGoArr = new GameObject[3];
    #endregion

    [SerializeField]
    private PuzzleSpawner _puzzleSpawner = null;
    public GameObject[] PuzzleGoArr { get => _puzzleGoArr; private set => _puzzleGoArr = value; }
    [SerializeField]
    private Button _btnReset = null;
    private void Awake()
    {
        _btnReset.onClick.AddListener(() => LazyStart());
    }
 
    public void LazyStart()
    {
        InstantiatePuzzleGos(ref _puzzleGoArr); 
    }
    private void InstantiatePuzzleGos(ref GameObject[] puzzleGoArr)
    {
        _puzzleSpawner.DestroyChilds();

        for (int i = 0; i < _puzzleGoArr.Length; i++)
            puzzleGoArr[i] = _puzzleSpawner.SpawnPuzzle(i);

        _checkPlacableCallback?.Invoke();
    }
    public delegate void CheckPlacable();
    private CheckPlacable _checkPlacableCallback { get; set; }
    public void Iniit(CheckPlacable checkPlacableCallback)
    {
        _checkPlacableCallback = checkPlacableCallback;
    }
} // end of class
