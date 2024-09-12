using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPuzzlePrefab : MonoBehaviour
{
    [SerializeField]
    PuzzleManager _puzzleManager = null;
    private void Start()
    {
        BuildPuzzlePrefabFunc();
    }
    private void BuildPuzzlePrefabFunc()
    {
        GameObject _puzzlePrefab_leftLine = null;
        GameObject _puzzlePrefab_UpperLine = null;
        GameObject _puzzlePrefab_LeftUpperLine = null;
        GameObject _puzzlePrefab_NoLine = null;
        _puzzlePrefab_leftLine = Resources.Load<GameObject>("Prefabs/Puzzle/Puzzle_LeftLine");
        _puzzlePrefab_UpperLine = Resources.Load<GameObject>("Prefabs/Puzzle/Puzzle_UpperLine");
        _puzzlePrefab_LeftUpperLine = Resources.Load<GameObject>("Prefabs/Puzzle/Puzzle_LeftUpperLine");
        _puzzlePrefab_NoLine = Resources.Load<GameObject>("Prefabs/Puzzle/Puzzle_NoLine");

        for (int i = 0; i < _puzzleManager.PuzzleArr.Length; i++)
        {
            int[,] selectedPuzzle = _puzzleManager.PuzzleArr[i];
            GameObject puzzlePrefab = new GameObject($"Puzzle_{i}");

            int puzzleRows = selectedPuzzle.GetLength(0);
            int puzzleCols = selectedPuzzle.GetLength(1);
            for (int r = 0; r < puzzleRows; r++)
            {
                for (int c = 0; c < puzzleCols; c++)
                {
                    if (selectedPuzzle[r, c] == 1)
                    {
                        GameObject puzzlePartPrefab = null;
                        if (r == 0 && c == 0)
                            puzzlePartPrefab = _puzzlePrefab_NoLine;
                        else 
                        {
                            if (r == 0 && c != 0)
                                puzzlePartPrefab = _puzzlePrefab_leftLine;
                            else if(r!=0 && c==0)
                                puzzlePartPrefab = _puzzlePrefab_UpperLine;
                            else
                                puzzlePartPrefab = _puzzlePrefab_LeftUpperLine;
                        }

                        GameObject puzzlePart = Instantiate(puzzlePartPrefab, puzzlePrefab.transform);
                        puzzlePart.transform.rotation = Quaternion.identity;
                        puzzlePart.transform.localPosition = new Vector3(puzzlePart.transform.localScale.x * c, -puzzlePart.transform.localScale.y * r, 0);
                    }

                }
            }
            // puzzlePrefab.transform.localScale = new Vector3(1f,1f, 1f);
            puzzlePrefab.transform.rotation = Quaternion.identity;
        }
    }
} // end of class
