using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int[,] Data { get; set; }

    private bool _placable = false;
   
    public bool CheckPlacable(int[,] nowGridArr)
    {
        // TODO - CheckPlacable ����
        return _placable;
    }

} // end of class
