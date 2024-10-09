using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Hidden Private Variables
    private Dictionary<string, GridPart> _childGridPartDic = new Dictionary<string, GridPart>(); // {r},{c}
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
        get
        {
            return _data;
        }
        set
        {
            _data = value;
            InitializeGridColor();
            InitializeGridPartData(Data);
            Debug.Log($"GridData : {Util.ConvertDoubleArrayToString(Data)}"); 
        }
    }
    public int[,] BackupData
    {
        get => _backupData; set => _backupData = value;
    }
    public Dictionary<string, GridPart> ChildGridPartDic { get => _childGridPartDic; set => _childGridPartDic = value; } // {r},{c}

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
        if (data == null) return;

        int rowCnt = data.GetLength(0);
        int colCnt = data.GetLength(1);

        for (int r = 0; r < rowCnt; r++)
        {
            for (int c = 0; c < colCnt; c++)
            {
                GridPart gridPart = ChildGridPartDic[$"{r},{c}"];
                if (gridPart.IdxRow == Factor.IntInitialized)
                    gridPart.IdxRow = r;
                if (gridPart.IdxCol == Factor.IntInitialized)
                    gridPart.IdxCol = c;
                if (gridPart.Data != data[r, c])
                    gridPart.Data = data[r, c]; 
            }
        }
    }
    public int GetDataIdx(int idxR, int idxC)
    {
        if (idxR > Data.GetLength(0) - 1 || idxR < 0) ;
        if (idxC > Data.GetLength(1) - 1 || idxC < 0) ;

        return Data[idxR, idxC];
    }

    /// <summary>
    /// GridPart 데이터가 변했을 때 Grid의 Data에 반영해주는 함수
    /// </summary>
    /// <param name="idxR"></param>
    /// <param name="idxC"></param>
    /// <param name="afterData"></param>
    public void SetDataIdx(int idxR, int idxC, int afterData)
    {
        if (idxR > Data.GetLength(0) - 1 || idxR < 0) return;
        if (idxC > Data.GetLength(1) - 1 || idxC < 0) return;
        if (afterData == Factor.IntInitialized) return;

        if (Data[idxR, idxC] != afterData)
            Data[idxR, idxC] = afterData;
        ChildGridPartDic[$"{idxR},{idxC}"].Data = afterData;
    }
} // end of class
