using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Hidden Private Variables
    private Dictionary<string, GridPart> _childGridPartDic = new Dictionary<string, GridPart>(); // GridPart_{r},{c}
    private int[,] _data;
    private int[,] _backupData;
    #endregion
    #region Grid Color
    public Color Color_HasNoPuzzle { get; private set; }
    public Color Color_HasPuzzle { get; private set; }
    public Color Color_Placable { get; private set; }
    #endregion
    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            InitializeGridColor();
            InitializeGridPartData(Data);
        }
    }
    public int[,] BackupData
    {
        get => _backupData; set => _backupData = value;
    }
    public Dictionary<string, GridPart> ChildGridPartDic { get => _childGridPartDic; private set => _childGridPartDic = value; } // GridPart_{r},{c}

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
    private void InitializeGridPartData(int[,] data)
    {
        int rowCnt = data.GetLength(0);
        int colCnt = data.GetLength(1);

        for (int r = 0; r < rowCnt; r++)
        {
            for (int c = 0; c < colCnt; c++)
            {
                GridPart gridPart = ChildGridPartDic[$"GridPart_{r},{c}"];
                gridPart.Data = data[r, c];
                gridPart.IdxRow = r;
                gridPart.IdxCol = c;
            }
        }
    }

    public void SetGridPartDataRange(int startIdxR, int startIdxC, int endIdxR, int endIdxC, int gridPartAfterData)
    {
        if (endIdxR > Data.GetLength(0) - 1) return;
        if (endIdxC > Data.GetLength(1) - 1) return;

        for (int i = startIdxR; i <= endIdxR; i++)
            for (int j = startIdxC; j <= endIdxC; j++)
                if (Data[i, j] != 1 && ChildGridPartDic.ContainsKey($"GridPart_{i},{j}"))
                    ChildGridPartDic[$"GridPart_{i},{j}"].Data = gridPartAfterData;
    }
    public void SetGridPartData(int idxR, int idxC, int gridPartAfterData)
    {
        if (idxR > Data.GetLength(0) - 1) return;
        if (idxC > Data.GetLength(1) - 1) return;

        if (Data[idxR, idxC] != 1 && Data[idxR, idxC] != gridPartAfterData && ChildGridPartDic.ContainsKey($"GridPart_{idxR},{idxC}"))
            ChildGridPartDic[$"GridPart_{idxR},{idxC}"].Data = gridPartAfterData;
    }
} // end of class
