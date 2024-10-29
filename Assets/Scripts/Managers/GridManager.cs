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
       _checkPlacableCallback?.Invoke();
    }

    #region delegate
    public delegate void CheckPlacable();
    private CheckPlacable _checkPlacableCallback { get; set; }

    public void Iniit(CheckPlacable checkPlacableCallback)
    {
        _checkPlacableCallback = checkPlacableCallback;
    }
    #endregion 
} // end of class
