using UnityEngine;

public class GridPart : MonoBehaviour
{
    #region Hidden Private Valiables
    private Grid _parentGrid = null;
    private int _data = Factor.IntInitialized;
    private int _idxRow = Factor.IntInitialized;
    private int _idxCol = Factor.IntInitialized;
    private SpriteRenderer _spr = null;
    #endregion

    public Grid ParentGrid { get => _parentGrid; set => _parentGrid = value; }

    /// <summary>
    /// NoPZ : 0, HasPZ: 1, Placable : 2, Test_Red : 3
    /// </summary>
    public int Data
    {
        get => _data;
        set
        {
            _data = value;
            SetGridPartColor();

            if (Data != Factor.IntInitialized && IdxRow != Factor.IntInitialized && IdxCol != Factor.IntInitialized)
            {
                if (ParentGrid.GetDataIdx(IdxRow, IdxCol) != Data)
                    ParentGrid.SetDataIdx(IdxRow, IdxCol, Data);
            }
            //   Debug.Log($"GridPart: {IdxRow},{IdxCol} - {Data}");
        }
    }
    public int IdxRow { get => _idxRow; set => _idxRow = value; }
    public int IdxCol { get => _idxCol; set => _idxCol = value; }
    public SpriteRenderer Spr
    {
        get
        {
            if (_spr == null)
                _spr = GetComponent<SpriteRenderer>();
            return _spr;
        }
        set => _spr = value;
    }

    public void SetGridPartColor()
    {
        if (Data == Factor.HasNoPuzzle) // 0
            Spr.color = ParentGrid.Color_HasNoPuzzle;
        else if (Data == Factor.HasPuzzle) // 1
            Spr.color = ParentGrid.Color_HasPuzzle;
        else if (Data == Factor.Placable) // 2
            Spr.color = ParentGrid.Color_Placable;
        else if (Data == Factor.Completable) // 3
            Spr.color = ParentGrid.Color_Completable;
        else
            Spr.color = Color.blue;
    }
} // end of class
