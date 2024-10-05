using UnityEngine;

public class PZPart : MonoBehaviour
{
    #region Hidden Private Variables
    private bool _isInGrid;
    private Puzzle _parentPuzzle;
    private SpriteRenderer _spr;
    #endregion
    public IdxRCStruct idxStruct = new IdxRCStruct(0,0);
    public bool IsInGrid { get => _isInGrid; private set => _isInGrid = value; }
    public Puzzle ParentPuzzle { get => _parentPuzzle; set => _parentPuzzle = value; }
    public SpriteRenderer Spr { get => _spr; set => _spr = value; }
    public string TriggeredGridPartIdxStr = string.Empty; // r,c 형태로 저장
    private void Awake()
    {
        string[] idxStr = this.name.Split('_')[1].Split(',');
        idxStruct.IdxR = int.Parse(idxStr[0]);
        idxStruct.IdxC = int.Parse(idxStr[1]);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Grid")
        {
            IsInGrid = true;
            ParentPuzzle.CallbackChildPuzzlePartCollision(IsInGrid);
        }
        if (collision.tag == "GridPart")
        {
            if (TriggeredGridPartIdxStr == collision.name) return;
            TriggeredGridPartIdxStr = collision.name.Split('_')[1];
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Grid")
        {
            IsInGrid = false;
            TriggeredGridPartIdxStr = string.Empty;
            ParentPuzzle.CallbackChildPuzzlePartCollision(IsInGrid);
        }

        if (collision.tag == "GridPart")
            TriggeredGridPartIdxStr = string.Empty;
    }
} // end of class
