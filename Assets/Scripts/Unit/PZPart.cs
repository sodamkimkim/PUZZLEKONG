using UnityEngine;

public class PZPart : MonoBehaviour
{
    #region Hidden Private Variables
    private bool _isInGrid;
    private Puzzle _parentPuzzle;
    #endregion

    public bool IsInGrid { get => _isInGrid; private set => _isInGrid = value; }
    public Puzzle ParentPuzzle { get => _parentPuzzle; set => _parentPuzzle = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Grid") return;
        IsInGrid = true;
        ParentPuzzle.HandleCallbackChildPuzzlePartCollision(IsInGrid);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Grid") return;
        IsInGrid = false;
        ParentPuzzle.HandleCallbackChildPuzzlePartCollision(IsInGrid);
    }
} // end of class
