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
    public delegate void CheckGameOver();
    public delegate int SetPuzzleActive();
    private CheckGameOver _checkStageCompleteOrGameOverCallback { get; set; }
    private SetPuzzleActive _setPuzzleActiveCallback { get; set; }
    public void Init(SetPuzzleActive SetPuzzleActiveCallback, CheckGameOver checkStageCompleteOrGameOverCallback)
    {
        _setPuzzleActiveCallback = SetPuzzleActiveCallback;
        _checkStageCompleteOrGameOverCallback = checkStageCompleteOrGameOverCallback;
    }
    private void Awake()
    {
        _btnReset.onClick.AddListener(() => LazyStart());
    }
    //public void LazyStart()
    //{
    //    InstantiatePuzzleGos();
    //    _setPuzzleActiveCallback?.Invoke();
    //    _checkStageCompleteOrGameOverCallback?.Invoke();
    //}
    /// <summary>
    /// 3���� ��� activeself == true �ΰɷ� ��������� ��
    /// gameover���� �ȵ�
    /// �Ȱ����� 3�� �����ص� ��
    /// </summary>
    public void LazyStart()
    {
        InstantiatePuzzleGos();

        if (_setPuzzleActiveCallback?.Invoke() <2)
        {
            Debug.Log("�ٽû���");
            LazyStart();
        } 
    }


    private void InstantiatePuzzleGos()
    {
        _puzzleSpawner.DestroyChilds();

        for (int i = 0; i < PuzzleGoArr.Length; i++)
        {
            PuzzleGoArr[i] = _puzzleSpawner.SpawnPuzzle(i);
        }
    }
} // end of class
