using UnityEngine;

public class GridPart : MonoBehaviour
{
    private bool _hasPuzzle = false;
    private int _idxRow = -99;
    private int _idxCol = -99;

    public bool HasPuzzle { get => _hasPuzzle; set => _hasPuzzle = value; }
    public int IdxRow { get => _idxRow; set => _idxRow = value; }
    public int IdxCol { get => _idxCol; set => _idxCol = value; }
} // end of class
