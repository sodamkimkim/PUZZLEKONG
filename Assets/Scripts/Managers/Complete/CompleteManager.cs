using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// 1. Comletable check & mark
/// 2. Complete => Grid Update callback ȣ��
/// </summary>
public class CompleteManager : MonoBehaviour
{
    #region Hidden Private Variables
    private static bool _isProcessiong = false;
    private int _totalComboCnt = 0;
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
    public int ComboCnt { get => _totalComboCnt; set => _totalComboCnt = value; }
    #endregion

    #region Dependency Injection 
    [SerializeField]
    private UIManager _uiManager = null;
    [SerializeField]
    private PlayerData _playerDataManager = null;
    [SerializeField]
    private GridManager _gridManager = null;

    [SerializeField]
    public CompleteHorizontal _completeHorizontal = null;
    [SerializeField]
    public CompleteVertical _completeVertical = null;
    [SerializeField]
    public CompleteArea _completeArea = null;
    [SerializeField]
    private EffectManager _effectManager = null;
    #endregion

    public PuzzleManager.SetPuzzleActive SetPuzzlesActiveCallback;
    public delegate void CheckGameOver();

    public void Init(PuzzleManager.SetPuzzleActive setPuzzlesActiveCallback)
    {
        SetPuzzlesActiveCallback = setPuzzlesActiveCallback;

        _completeHorizontal.Init(setPuzzlesActiveCallback, CompleteEffect);
        _completeVertical.Init(setPuzzlesActiveCallback, CompleteEffect);
        _completeArea.Init(setPuzzlesActiveCallback, CompleteEffect);
    }
    /// <summary>
    /// �ش� ������ �׸��忡 �η��� �� �� Complete���ε� ǥ��
    /// </summary>
    /// <param name="touchingPZ"></param>
    public void MarkCompletable(Puzzle touchingPZ)
    {
        int[,] gridDataSync = (int[,])_gridManager.Grid.Data.Clone();
        _completeHorizontal.MarkCompletable(_gridManager.Grid, gridDataSync);
        _completeVertical.MarkCompletable(_gridManager.Grid, gridDataSync);
        _completeArea.MarkCompletable(_gridManager.Grid, gridDataSync);
    }
    public void Complete(CheckGameOver CheckStageCompleteOrGameOver)
    {
        IsProcessing = true;
        MarkCompletableReset();
        int[,] gridDataSync = (int[,])_gridManager.Grid.Data.Clone();
        int comboCnt_hori = _completeHorizontal.Complete(_gridManager.Grid, gridDataSync);
        int comboCnt_verti = _completeVertical.Complete(_gridManager.Grid, gridDataSync);
        int comboCnt_area = _completeArea.Complete(_gridManager.Grid, gridDataSync);
        //    Debug.Log(Util.ConvertDoubleArrayToString(gridDataSync));

        int completeCnt = comboCnt_hori + comboCnt_verti + comboCnt_area;
        ComboAndSaveData(completeCnt);

        // Complete �ٵ�����
        IsProcessing = false;

        if (SetPuzzlesActiveCallback() == 0)
            CheckStageCompleteOrGameOver();
    }
    private void ComboAndSaveData(int comboCnt)
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
            SaveData(score);
        }
        else
            _totalComboCnt = 0;
    }
    private void SaveData(int score)
    {
        PlayerData.NowScore += score;
        PlayerData.PlayerTotalScore += score;

        if (PlayerData.MyBestScore < PlayerData.NowScore) PlayerData.MyBestScore = PlayerData.NowScore;

        Debug.Log($"Score : {score} | {PlayerData.ToString_Score()}");
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
            if (kvp.Value.Data == Factor.Completable)
            {
                kvp.Value.Data = Factor.HasPuzzle;
                kvp.Value.SetGridPartColor();
            }
        }
    }
} // end of class