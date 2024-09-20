using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private Vector3 _initialPos = Vector3.zero;

    public Vector3 InitialPos
    {
        get => _initialPos; 

        private set
        {
            _initialPos = value;
        }
    }
    public void SetInitialPos(Vector3 pos)
    {
        InitialPos = pos;
    }
} // end of class
