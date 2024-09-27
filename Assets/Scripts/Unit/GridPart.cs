using UnityEngine;

public class GridPart : MonoBehaviour
{
    #region Hidden Private Valiables
    private Grid _parentGrid = null;
    private int _data = -99;
    private int _idxRow = -99;
    private int _idxCol = -99;
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
        }
    }
    public int IdxRow { get => _idxRow; set => _idxRow = value; }
    public int IdxCol { get => _idxCol; set => _idxCol = value; }
    public SpriteRenderer Spr { get => _spr; set => _spr = value; }

    public void SetGridPartColor()
    {
        //switch (Data)
        //{
        //    case 0: // HasPuzzle == false
        //        Spr.color = ParentGrid.Color_HasNoPuzzle;
        //        break;
        //    case 1: // HasPuzzle == true
        //        Spr.color = ParentGrid.Color_HasPuzzle;
        //        break;
        //    case 2: // Placable
        //        Spr.color = ParentGrid.Color_Placable;
        //        break; 
        //}
        if (Data == 0)
            Spr.color = ParentGrid.Color_HasNoPuzzle;
        else if (Data == 1)
            Spr.color = ParentGrid.Color_HasPuzzle;
        else if (Data == 2)
            Spr.color = ParentGrid.Color_Placable;
        else
        { 
            Spr.color = Color.blue;
        }
    }
} // end of class
