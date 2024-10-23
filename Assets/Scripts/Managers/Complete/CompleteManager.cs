using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Comletable check & mark
/// 2. Complete => Grid Update callback 호출
/// </summary>
public class CompleteManager : MonoBehaviour
{
    #region Hidden Private Variables
    private static bool _isAllComplete = false;
    #endregion

    #region Properties
    /// <summary>
    /// Complete 코루틴/Effect 처리 중일 떄 Puzzle Touching 안되게 해줘야함 =><- Shot Ray(x) 
    /// 
    /// </summary>
    public static bool IsProcessing
    {
        get => _isAllComplete;
        set
        {
            _isAllComplete = value;
        }
    }
    #endregion

    #region Dependency Injection
    [SerializeField]
    private GridManager _gridManager = null;

    [SerializeField]
    private CompleteHorizontal _completeHorizontal = null;
    [SerializeField]
    private CompleteVertical _completeVertical = null;
    [SerializeField]
    private CompleteArea _completeArea = null;
    [SerializeField]
    private EffectManager _effectManager = null;
    #endregion
    public delegate void CheckPlacableAllRemaingPuzzles();
    /// <summary>
    /// 해당 퍼즐을 그리드에 두려고 할 때 Complete여부도 표시
    /// </summary>
    /// <param name="touchingPZ"></param>
    public void MarkCompletable(Puzzle touchingPZ)
    {
        // TODO
        //int[,] gridDataSync = _gridManager.Grid.Data;
        //_completeHorizontal.MarkCompletable(_gridManager.Grid, gridDataSync);
        //_completeVertical.MarkCompletable(_gridManager.Grid, gridDataSync);
        //_completeArea.MarkCompletable(_gridManager.Grid, gridDataSync);
    }
    public void Complete(CheckPlacableAllRemaingPuzzles checkPlacableAllRemainingPzCallback)
    {
        IsProcessing = true;
        int[,] gridDataSync = _gridManager.Grid.Data;
        _completeHorizontal.Complete(_gridManager.Grid, gridDataSync,
            () => checkPlacableAllRemainingPzCallback(), CompleteEffect);
        _completeVertical.Complete(_gridManager.Grid, gridDataSync,
            () => checkPlacableAllRemainingPzCallback(), CompleteEffect);
        _completeArea.Complete(_gridManager.Grid, gridDataSync,
            () => checkPlacableAllRemainingPzCallback(), CompleteEffect);

        IsProcessing = false;
        checkPlacableAllRemainingPzCallback();
    }

    public void CompleteEffect(Vector3 worldPos, MonoBehaviour callerMono)
    {
        if (callerMono == null) return;

        worldPos.z = Factor.PosEffectSpawnZ;

        if (callerMono.GetType() == typeof(CompleteHorizontal))
            Instantiate(_effectManager.EffectPrefab_Complete_Hori, worldPos, Quaternion.identity);
        else if (callerMono.GetType() == typeof(CompleteVertical))
            Instantiate(_effectManager.EffectPrefab_Complete_Verti, worldPos, Quaternion.identity);
        else if (callerMono.GetType() == typeof(CompleteArea))
            Instantiate(_effectManager.EffectPrefab_Complete_Area, worldPos, Quaternion.identity);
    }

} // end of class