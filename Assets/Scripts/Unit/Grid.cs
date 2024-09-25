using UnityEngine;

public class Grid : MonoBehaviour
{
    private int[,] _data;

    public int[,] Data { get => _data; set => _data = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Puzzle puzzle = collision.GetComponentInParent<Puzzle>();
        if (puzzle != null)
            Debug.Log($"From Grid - {puzzle.name}");
        else
            return;
    }
} // end of class
