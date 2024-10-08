using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Comletable check & mark
/// 2. Complete => Grid Update callback ȣ��
/// </summary>
public class CompleteManager : MonoBehaviour
{
    #region Hidden Private Variables
    private static bool _isProcessing = false;
    #endregion

    #region Properties
    /// <summary>
    /// Complete �ڷ�ƾ/Effect ó�� ���� �� Puzzle Touching �ȵǰ� ������� =><- Shot Ray(x) 
    /// 
    /// </summary>
    public static bool IsProcessing { get => _isProcessing; set => _isProcessing = value; }
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
    #endregion

    /// <summary>
    /// �ش� ������ �׸��忡 �η��� �� �� Complete���ε� ǥ��
    /// </summary>
    /// <param name="touchingPZ"></param>
    public void MarkCompletable(Puzzle touchingPZ)
    {
        // TODO
        int[,] gridDataSync = _gridManager.Grid.Data;
        _completeHorizontal.MarkCompletable(_gridManager.Grid, gridDataSync);
        _completeVertical.MarkCompletable(_gridManager.Grid, gridDataSync);
        _completeArea.MarkCompletable(_gridManager.Grid, gridDataSync);
    }
    public void Complete()
    {
        // TODO - Effect
        int[,] gridDataSync = _gridManager.Grid.Data;
        _completeHorizontal.Complete(_gridManager.Grid, gridDataSync);
        _completeVertical.Complete(_gridManager.Grid, gridDataSync);
        _completeArea.Complete(_gridManager.Grid, gridDataSync);
    }
} // end of class