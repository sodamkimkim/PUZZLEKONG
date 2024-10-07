using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle : MonoBehaviour
{
    #region Hidden Private Variables
    private int[,] _data;
    private int[] _lastIdx_rc;
    private int[] _getRealLength_rc = new int[2] { 0, 0 };

    private Vector3 _spawnPos;
    private List<PZPart> _childPZPartList = new List<PZPart>(); 
    private Color _childColor = Color.clear; 
    private string _firstTriggeredGridPartNameStr;
    #endregion

    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            LastIdx_rc = GetLastIdx(_data);
        }
    }
    public int[] LastIdx_rc { get => _lastIdx_rc; private set => _lastIdx_rc = value; }

    public List<PZPart> ChildPZPartList { get => _childPZPartList; private set => _childPZPartList = value; }
    public Vector3 SpawnPos { get => _spawnPos; set => _spawnPos = value; }
    public Color ChildColor { get => _childColor; set => _childColor = value; }

//    public bool IsInGrid { get => _isInGrid; set => _isInGrid = value; }
    public string FirstTriggeredGridPartNameStr { get => _firstTriggeredGridPartNameStr; set => _firstTriggeredGridPartNameStr = value; }
    private Vector3 _mousePosBackUP = Vector3.zero;
    private int[] GetLastIdx(int[,] dbArr)
    {
        int[] rcIdxArr = new int[2] { 0, 0 };

        int row = dbArr.GetLength(0);
        int col = dbArr.GetLength(1);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                if (dbArr[r, c] == 1)
                {
                    if (rcIdxArr[0] < r)
                        rcIdxArr[0] = r;

                    // 이전 값이랑 비교해서 크면 넣어야 함
                    if (rcIdxArr[1] < c)
                        rcIdxArr[1] = c;
                }
            }
        }
        return rcIdxArr;
    }
    private void SetChildColor(Color color)
    {
        foreach (PZPart pZPart in ChildPZPartList)
        {
            if (pZPart.Spr.color == color) return;
            pZPart.Spr.color = color;
        }
    }
    //private bool CheckAllChildInGrid()
    //{
    //    bool isAllInGrid = true;
    //    foreach (PZPart pZPart in ChildPZPartList)
    //    {
    //        isAllInGrid &= pZPart.IsInGrid;
    //    }
    //    return isAllInGrid;
    //}
    //public void CallbackChildPuzzlePartCollision(bool isEnter)
    //{
    //    if (isEnter)
    //    {
    //        if (CheckAllChildInGrid())
    //        {
    //            IsInGrid = true;
    //            SetChildColor(Color.red);  // DEBUGGING  
    //        }
    //        else
    //        {
    //            IsInGrid = false;
    //            SetChildColor(ChildColor); // DEBUGGING
    //        }
    //    }
    //    else
    //    {
    //        IsInGrid = false;
    //        SetChildColor(ChildColor); // DEBUGGING
    //    }
    //} 
} // end of class
