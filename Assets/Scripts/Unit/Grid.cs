using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Hidden Private Variables
    private Dictionary<string, GridPart> _childGridPartDic = new Dictionary<string, GridPart>();
    private int[,] _data;
    #endregion
    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            RefreshGrid(_data);
        }
    }
    public Dictionary<string, GridPart> ChildGridPartDic { get => _childGridPartDic; private set => _childGridPartDic = value; } // GridPart_{r}_{c}

    #region Grid Color
    public Color Color_HasNoPuzzle { get; private set; }
    public Color Color_HasPuzzle { get; private set; }
    public Color Color_Placable { get; private set; }
    #endregion
    private void InitializeGridColor()
    {
        switch (ThemeManager.ETheme)
        {
            case Enum.eTheme.Grey:
                Color_HasNoPuzzle = Factor.Grey2;
                Color_Placable = Factor.Grey3;
                Color_HasPuzzle = Factor.Grey4;
                break;
            case Enum.eTheme.Green:
                Color_HasNoPuzzle = Factor.Green2;
                Color_Placable = Factor.Green3;
                Color_HasPuzzle = Factor.Green4;
                break;
            case Enum.eTheme.LightPurple:
                Color_HasNoPuzzle = Factor.LightPurple2;
                Color_Placable = Factor.LightPurple3;
                Color_HasPuzzle = Factor.LightPurple4;
                break;
            case Enum.eTheme.LightBlue:
                Color_HasNoPuzzle = Factor.LightBlue2;
                Color_Placable = Factor.LightBlue3;
                Color_HasPuzzle = Factor.LightBlue4;
                break;
            case Enum.eTheme.Pink:
                Color_HasNoPuzzle = Factor.Pink2;
                Color_Placable = Factor.Pink3;
                Color_HasPuzzle = Factor.Pink4;
                break;
            case Enum.eTheme.Mint:
                Color_HasNoPuzzle = Factor.BGColorDefault;
                Color_Placable = Factor.Grey3;
                Color_HasPuzzle = Factor.Grey4;
                break;
        }
    }
    private void SetGridPartData(int[,] data)
    {
        int rowCnt = data.GetLength(0);
        int colCnt = data.GetLength(1);

        for (int r = 0; r < rowCnt; r++)
        {
            for (int c = 0; c < colCnt; c++)
            {
                GridPart gridPart = ChildGridPartDic[$"GridPart_{r}_{c}"];
                gridPart.Data = data[r, c];
                gridPart.IdxRow = r;
                gridPart.IdxCol = c;
            }
        }
    }
    private void RefreshGrid(int[,] data)
    {
        InitializeGridColor();
        SetGridPartData(data);
    }
} // end of class
