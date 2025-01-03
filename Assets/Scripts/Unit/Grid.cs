using System.Collections;
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
    public Color Color_Completable { get; private set; }
    public Color Color_Point { get; private set; }
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
            //  Debug.Log($"GridData : {Util.ConvertDoubleArrayToString(Data)}"); 
        }
    }
    public int[,] BackupData
    {
        get => _backupData; set => _backupData = value;
    }
    public Dictionary<string, GridPart> ChildGridPartDic { get => _childGridPartDic; set => _childGridPartDic = value; } // {r},{c}

    private void InitializeGridColor()
    {
        switch (ThemaManager.ETheme)
        {
            case Str.eTheme.Grey:
                Color_HasNoPuzzle = Factor.Grey1;
                Color_Placable = Factor.Grey2;
                Color_HasPuzzle = Factor.Grey3;
                break;
            case Str.eTheme.Green:
                Color_HasNoPuzzle = Factor.Green1;
                Color_Placable = Factor.Green2;
                Color_HasPuzzle = Factor.Green3;
                break;
            case Str.eTheme.LightPurple:
                Color_HasNoPuzzle = Factor.LightPurple1;
                Color_Placable = Factor.LightPurple2;
                Color_HasPuzzle = Factor.LightPurple3;
                break;
            case Str.eTheme.LightBlue:
                Color_HasNoPuzzle = Factor.LightBlue1;
                Color_Placable = Factor.LightBlue2;
                Color_HasPuzzle = Factor.LightBlue3;
                break;
            case Str.eTheme.Pink:
                Color_HasNoPuzzle = Factor.Pink1;
                Color_Placable = Factor.Pink2;
                Color_HasPuzzle = Factor.Pink3;
                break;
            case Str.eTheme.Yellow:
                Color_HasNoPuzzle = Factor.Yellow1;
                Color_Placable = Factor.Yellow2;
                Color_HasPuzzle = Factor.Yellow3;
                break;
            case Str.eTheme.Mint:
                Color_HasNoPuzzle = Factor.BGColorDefault;
                Color_Placable = Factor.Grey2;
                Color_HasPuzzle = Factor.Green3;
                break;
        }
        Color_Completable = Color_HasPuzzle * Factor.CompletableOffset;
        Color_Point = new Color(500f / 255f, 500f / 255f, 500f / 255f);
    }
    private void InitializeGridPartData(int[,] data)
    {
        if (data == null) return;

        int rowCnt = data.GetLength(0);
        int colCnt = data.GetLength(1);

        for (int r = 0; r < rowCnt; r++)
            for (int c = 0; c < colCnt; c++)
            {
                GridPart gridPart = ChildGridPartDic[$"{r},{c}"];
                if (gridPart.IdxRow == Factor.IntInitialized)
                    gridPart.IdxRow = r;
                if (gridPart.IdxCol == Factor.IntInitialized)
                    gridPart.IdxCol = c;
                if (gridPart.Data != data[r, c])
                {
                    gridPart.Data = data[r, c];
                    gridPart.SetGridPartColor();
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
    /// 호출 시 gridpart 색상변경코드 추가 필요
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
        //ChildGridPartDic[$"{idxR},{idxC}"].SetGridPartColor();

    }
    public IEnumerator SetGridPartColorCoroutine(int startR, int endR, int startC, int endC, bool dirR, float interval)
    {
        if (dirR)
        {
            for (int r = startR; r <= endR; r++)
                for (int c = startC; c <= endC; c++)
                {
                    ChildGridPartDic[$"{r},{c}"].SetGridPartColor();
                    yield return new WaitForSeconds(interval);
                }
        }
        else
        {
            for (int r = startR; r >= endR; r--)
                for (int c = startC; c >= endC; c--)
                {
                    ChildGridPartDic[$"{r},{c}"].SetGridPartColor();
                    yield return new WaitForSeconds(interval);
                }
        }
    }
} // end of class
