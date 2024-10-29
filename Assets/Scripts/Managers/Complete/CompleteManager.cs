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
    private static bool _isProcessiong = false;
    private int _totalComboCnt = 0;
    #endregion

    #region Properties
    /// <summary>
    /// Complete 코루틴/Effect 처리 중일 떄 Puzzle Touching 안되게 해줘야함 =><- Shot Ray(x) 
    /// 
    /// </summary>
    public static bool IsProcessing
    {
        get => _isProcessiong;
        set
        {
            _isProcessiong = value;
        }
    }
    public int ComboCnt { get => _totalComboCnt; set => _totalComboCnt = value; }
    #endregion

    #region Dependency Injection 
    [SerializeField]
    private UIManager _uiManager = null;
    [SerializeField]
    private PlayerDataManager _playerDataManager = null;
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
        int[,] gridDataSync = _gridManager.Grid.Data;
        _completeHorizontal.MarkCompletable(_gridManager.Grid, gridDataSync, MarkCompletableReset);
        _completeVertical.MarkCompletable(_gridManager.Grid, gridDataSync, MarkCompletableReset);
        _completeArea.MarkCompletable(_gridManager.Grid, gridDataSync, MarkCompletableReset);
    }
    public void Complete(CheckPlacableAllRemaingPuzzles checkPlacableAllRemainingPzCallback)
    {
        IsProcessing = true;
        MarkCompletableReset();
        int[,] gridDataSync = _gridManager.Grid.Data;
        int comboCnt_hori = _completeHorizontal.Complete(_gridManager.Grid, gridDataSync,
               () => checkPlacableAllRemainingPzCallback(), CompleteEffect);
        int comboCnt_verti = _completeVertical.Complete(_gridManager.Grid, gridDataSync,
            () => checkPlacableAllRemainingPzCallback(), CompleteEffect);
        int comboCnt_area = _completeArea.Complete(_gridManager.Grid, gridDataSync,
            () => checkPlacableAllRemainingPzCallback(), CompleteEffect);

        ComboAndSave(comboCnt_hori + comboCnt_verti + comboCnt_area);

        IsProcessing = false;
        checkPlacableAllRemainingPzCallback();
    }
    private void ComboAndSave(int comboCnt)
    {
        if (comboCnt > 0)
        {
            int score = Factor.CompleteScore;
            _totalComboCnt += comboCnt;

            // Combo Celebration
            if (_totalComboCnt > 1)
            {
                Instantiate(_effectManager.EffectPrefab_Celebration_Combo, Factor.EffectPos_Celebration, Quaternion.identity);
                _uiManager.SetTMPText(_uiManager.UITMP_TempText_Large, $"{_totalComboCnt} C O M B O", Color.white, true); 

                score *= _totalComboCnt;
                _uiManager.SetTMPText(_uiManager.UITMP_TempText_Small, $"+ {score}", Color.red, true);   
            }

            PlayerDataManager.GameData.NowScore += score;
            _playerDataManager.SaveData();
            Debug.Log($"Score : {score} | {PlayerDataManager.GameData.ToString()}");
        }
        else
            _totalComboCnt = 0;
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
    public void MarkCompletableReset()
    {
        foreach (KeyValuePair<string, GridPart> kvp in _gridManager.Grid.ChildGridPartDic)
        {
            if (kvp.Value.Data == 3)
                kvp.Value.Data = 1;
        }
    }
} // end of class