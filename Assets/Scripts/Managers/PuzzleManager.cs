using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[DefaultExecutionOrder(-8)]
public class PuzzleManager : MonoBehaviour
{
    #region Hidden Private Variables
    private GameObject[] _puzzleGoArr = new GameObject[3];
    #endregion
    #region Properties
    public GameObject[] PuzzleGoArr { get => _puzzleGoArr; private set => _puzzleGoArr = value; }
    #endregion

    [SerializeField]
    public PuzzleSpawner _puzzleSpawner = null;
    [SerializeField]
    private Button _btnReset = null;
    private Dictionary<string, GameObject> _samePuzzleCheckDic = new Dictionary<string, GameObject>();
    private void Awake()
    {
        _samePuzzleCheckDic.Clear();
        _btnReset.onClick.AddListener(() => LazyStart());
    }
    public void LazyStart()
    {
        InstantiatePuzzleGos();
        _SetPuzzleActiveCallback?.Invoke();
        _checkStageCompleteOrGameOverCallback?.Invoke();
    }
    private void InstantiatePuzzleGos()
    {
        _puzzleSpawner.DestroyChilds();

        for (int i = 0; i < PuzzleGoArr.Length; i++)
        {
            PuzzleGoArr[i] = _puzzleSpawner.SpawnPuzzle(i);
        }

        if (_samePuzzleCheckDic.ContainsKey(PuzzleGoArr[0].name) &&
            _samePuzzleCheckDic.ContainsKey(PuzzleGoArr[1].name) &&
            _samePuzzleCheckDic.ContainsKey(PuzzleGoArr[2].name))
            InstantiatePuzzleGos();
        else
        {
            _samePuzzleCheckDic.Clear();
            for (int i = 0; i < PuzzleGoArr.Length; i++)
            {
                Util.CheckAndAddDictionary<GameObject>
                    (_samePuzzleCheckDic, PuzzleGoArr[i].name, PuzzleGoArr[i]);
            }
        }
    }

    #region delegate
    public delegate void CheckGameOver();
    public delegate int SetPuzzleActive();
    private CheckGameOver _checkStageCompleteOrGameOverCallback { get; set; }
    private SetPuzzleActive _SetPuzzleActiveCallback { get; set; }
    public void Iniit(SetPuzzleActive SetPuzzleActiveCallback, CheckGameOver checkStageCompleteOrGameOverCallback)
    {
        _SetPuzzleActiveCallback = SetPuzzleActiveCallback;
        _checkStageCompleteOrGameOverCallback = checkStageCompleteOrGameOverCallback;
    }
    #endregion
} // end of class
