using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle : MonoBehaviour
{
    #region Hidden Private Variables
    private int[,] _data;
    private int[] _lastIdx_rc;
    private int[] _getRealLength_rc= new int[2] { 0, 0 };

    private Vector3 _spawnPos;
    private List<PZPart> _childPZPartList = new List<PZPart>();
    private List<SpriteRenderer> _childSpr = new List<SpriteRenderer>();
    private Color _childColor = Color.clear;
    private bool _isInGrid = false;
    #endregion

    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            LastIdx_rc = GetLastIdx(_data);
            GetRealLength_rc[0] = LastIdx_rc[0] + 1;
            GetRealLength_rc[1] = LastIdx_rc[1] + 1;
        }
    }

    public bool IsInGrid { get => _isInGrid; set => _isInGrid = value; }
    public Vector3 SpawnPos { get => _spawnPos; set => _spawnPos = value; }
    public Color ChildColor { get => _childColor; set => _childColor = value; }
    public int[] LastIdx_rc { get => _lastIdx_rc; private set => _lastIdx_rc = value; }

    /// <summary>
    /// Puzzle이 가진 배열 데이터가 아닌 실제로 1값이 들어가있는 영역 length
    /// </summary>
    public int[] GetRealLength_rc { get => _getRealLength_rc; set => _getRealLength_rc = value; }
    public List<PZPart> ChildPZPartList { get => _childPZPartList; private set => _childPZPartList = value; }
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
    public void CallbackChildPuzzlePartCollision(bool isEnter)
    {
        if (isEnter)
        {
            if (CheckAllChildInGrid())
            {
                IsInGrid = true;
                SetChildColor(Color.red);  // TEMP
            }
            else
            {
                IsInGrid = false;
                SetChildColor(ChildColor);
            }
        }
        else
        {
            IsInGrid = false;
            SetChildColor(ChildColor);
        }
   //     Debug.Log($"{this.name} : InGrid {isEnter}");
    }
    private bool CheckAllChildInGrid()
    {
        bool isAllInGrid = true;
        foreach (PZPart pzPart in ChildPZPartList)
        {
            isAllInGrid &= pzPart.IsInGrid;
        }
        return isAllInGrid;
    }
    private void SetChildColor(Color color)
    {
        foreach (PZPart pzPart in ChildPZPartList)
        {
            if (pzPart.Spr.color == color) return;
            pzPart.Spr.color = color;
        }
    }
} // end of class
