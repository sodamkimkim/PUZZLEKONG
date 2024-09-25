using UnityEngine;

public class Grid : MonoBehaviour
{
    private int[,] _data;

    public int[,] Data { get => _data; set => _data = value; }
} // end of class
