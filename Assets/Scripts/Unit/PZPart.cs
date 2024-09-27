using UnityEngine;

public class PZPart : MonoBehaviour
{
    #region Hidden Private Variables
    private bool _isInGrid;
    private Puzzle _parentPuzzle;
    private SpriteRenderer _spr;
    #endregion

    public bool IsInGrid { get => _isInGrid; private set => _isInGrid = value; }
    public Puzzle ParentPuzzle { get => _parentPuzzle; set => _parentPuzzle = value; }
    public SpriteRenderer Spr { get => _spr; set => _spr = value; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Grid") return;
        IsInGrid = true;
        ParentPuzzle.CallbackChildPuzzlePartCollision(IsInGrid);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Grid") return;
        IsInGrid = false;
        ParentPuzzle.CallbackChildPuzzlePartCollision(IsInGrid);
    }
} // end of class
