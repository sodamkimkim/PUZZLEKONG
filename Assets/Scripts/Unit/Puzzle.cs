using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle : MonoBehaviour
{
    #region Hidden Private Variables
    private int[,] _data;
    private int _itemStatusData = 0;
    private int[] _lastIdx_rc;
    private Vector3 _spawnPos;
    private List<PZPart> _childPZPartList = new List<PZPart>();
     
    private Color _childColor = Color.clear;
    private Color _ItemUse = new Color(500f / 255f, 500f / 255f, 500f / 255f);
    private Color _notActiveColor = new Color(3f / 255f, 3f / 255f, 3f / 255f, 0.07f);
    private bool _activeSelf = true;
    #endregion
    #region Properties
    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            LastIdx_rc = GetLastIdx(_data);
            SetChildColor(ChildColor);
        }
    }
    public int ItemStatusData
    {
        get => _itemStatusData;
        set
        {
            _itemStatusData = value;
            if (value == Factor.PuzzleStatus_ItemUse) SetChildColor(ItemUseColor);
            else if (value == Factor.PuzzleStatus_ItemNormal) SetChildColor(ChildColor); 
            else SetChildColor(ChildColor);
        }

    }
    public int[] LastIdx_rc { get => _lastIdx_rc; private set => _lastIdx_rc = value; }

    public List<PZPart> ChildPZPartList { get => _childPZPartList; private set => _childPZPartList = value; }
    public Vector3 SpawnPos { get => _spawnPos; set => _spawnPos = value; }
    public bool ActiveSelf
    {
        get => _activeSelf; set
        {
            _activeSelf = value;
            if (ActiveSelf)
                SetChildColor(ChildColor);
            else
                SetChildColor(NotActiveColor);
        }
    }

    public Color ChildColor { get => _childColor; set => _childColor = value; }
    public Color ItemUseColor { get => _ItemUse; set => _ItemUse = value; }
    public Color NotActiveColor { get => _notActiveColor; set => _notActiveColor = value; }
    #endregion

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
    public void SetChildColor(Color color)
    {
        foreach (PZPart pZPart in ChildPZPartList)
        {
            if (pZPart.Spr.color == color) return;
            pZPart.Spr.color = color;
        }
    }
} // end of class
