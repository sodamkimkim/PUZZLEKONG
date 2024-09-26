using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle : MonoBehaviour
{
    private int[,] _data;
    private int[] _lastIdx;
    private Vector3 _spawnPos;
    private List<PZPart> _childPZPartList = new List<PZPart>();
    private List<SpriteRenderer> _childSpr = new List<SpriteRenderer>();
    private Color _childColor = Color.clear;
    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            LastIdx = GetLastIdx(_data);
        }
    }

    public int[] LastIdx { get => _lastIdx; private set => _lastIdx = value; }
    public Vector3 SpawnPos { get => _spawnPos; set => _spawnPos = value; }
    public List<PZPart> ChildPZPartList { get => _childPZPartList; private set => _childPZPartList = value; }
    public List<SpriteRenderer> ChildSprList { get => _childSpr; private set => _childSpr = value; }
    public Color ChildColor { get => _childColor; set => _childColor = value; }

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

    public void HandleCallbackChildPuzzlePartCollision(bool isEnter)
    {

        if (isEnter)
        {
            if (CheckAllChildInGrid())
                SetChildColor(Color.red);  // TEMP
            else
                SetChildColor(ChildColor);
        }
        else
            SetChildColor(ChildColor);

        Debug.Log($"{this.name} : InGrid {isEnter}");
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
        foreach (SpriteRenderer spr in ChildSprList)
        {
            if (spr.color == color) return;
            spr.color = color;
        }
    }
} // end of class
