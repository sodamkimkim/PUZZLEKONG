using System.Collections.Generic;
using System.Collections;
using UnityEngine;
[DefaultExecutionOrder(-9)]
public class GridManager : MonoBehaviour
{
    #region Hidden Private Variables
    private Grid _grid;
    private static bool _isGridReady = false;
    #endregion
    #region Properties
    public Grid Grid { get => _grid; private set => _grid = value; }
    public static bool IsGridGoReady
    {
        get => _isGridReady;
        private set
        {
            _isGridReady = value;
        }
    }
    #endregion

    [SerializeField]
    private GridSpawner _gridSpawner = null;
    public void LazyStart()
    {
        IsGridGoReady = _gridSpawner.SpawnGridGo(GridArrayRepository.GridArrArr[2], ref _grid);
        //  _SetPuzzleActiveCallback?.Invoke();
        //  _CheckStageCompleteCallback?.Invoke();
        //  _checkGameOverCallback?.Invoke();
    }

    #region delegate
    public delegate int SetPuzzleActive();
    public delegate void CheckStageComplete();
    public delegate void CheckGameover();
    private SetPuzzleActive _SetPuzzleActiveCallback { get; set; }
    private CheckStageComplete _CheckStageCompleteCallback { get; set; }
    private CheckGameover _checkGameOverCallback { get; set; }

    public void Iniit(SetPuzzleActive SetPuzzleActiveCallback, CheckStageComplete checkGameOverCallback, CheckGameover checkGameOVerCallback)
    {
        _SetPuzzleActiveCallback = SetPuzzleActiveCallback;
        _CheckStageCompleteCallback = checkGameOverCallback;
        _checkGameOverCallback = checkGameOVerCallback;
    }
    #endregion 
} // end of class
