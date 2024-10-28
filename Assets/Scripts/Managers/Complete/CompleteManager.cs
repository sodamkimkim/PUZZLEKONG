using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// 1. Comletable check & mark
/// 2. Complete => Grid Update callback ȣ��
/// </summary>
public class CompleteManager : MonoBehaviour
{
    #region Hidden Private Variables
    private static bool _isProcessiong = false;
    private int _comboCnt = 0;
    #endregion

    #region Properties
    /// <summary>
    /// Complete �ڷ�ƾ/Effect ó�� ���� �� Puzzle Touching �ȵǰ� ������� =><- Shot Ray(x) 
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
    public int ComboCnt { get => _comboCnt; set => _comboCnt = value; }
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

    [SerializeField]
    private GameObject _uiTMP_COMBO = null;
    [SerializeField]
    private GameObject _uiTMP_SCORE = null;
    #endregion
    public delegate void CheckPlacableAllRemaingPuzzles();

    private void Awake()
    {
        _uiTMP_COMBO.SetActive(false);
    }
    /// <summary>
    /// �ش� ������ �׸��忡 �η��� �� �� Complete���ε� ǥ��
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

        int totalCombo = comboCnt_hori + comboCnt_verti + comboCnt_area;
        if (totalCombo > 0)
        {
            int score = Factor.CompleteScore;
            _comboCnt += totalCombo;

            // Combo Celebration
            if (_comboCnt > 1)
            {
                Instantiate(_effectManager.EffectPrefab_Celebration_Combo, Factor.EffectPos_Celebration, Quaternion.identity);
                _uiTMP_COMBO.SetActive(true);
                _uiTMP_COMBO.GetComponent<TextMeshProUGUI>().text = $"{_comboCnt} C O M B O";

                score *= _comboCnt;
                _uiTMP_SCORE.GetComponent<TextMeshProUGUI>().text = $"+ {score}";
                Invoke(nameof(UIComboSetActiveFalse), 1f);
            }

            PlayerDataManager.GameData.Score += score;
            Debug.Log($"Score : {score} | PlayerManager.Score == {PlayerDataManager.GameData.Score}");
        }
        else
            _comboCnt = 0;


        Debug.Log("Combo : " + _comboCnt);

        IsProcessing = false;
        checkPlacableAllRemainingPzCallback();
    }
    private void UIComboSetActiveFalse()
    {
        _uiTMP_COMBO.SetActive(false);
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