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
        _puzzlePrefab_leftLine = Resources.Load<GameObject>("Prefabs/BuildPuzzle/Puzzle_LeftLine");
        _puzzlePrefab_UpperLine = Resources.Load<GameObject>("Prefabs/BuildPuzzle/Puzzle_UpperLine");
        _puzzlePrefab_LeftUpperLine = Resources.Load<GameObject>("Prefabs/BuildPuzzle/Puzzle_LeftUpperLine");
        _puzzlePrefab_NoLine = Resources.Load<GameObject>("Prefabs/BuildPuzzle/Puzzle_NoLine");

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
                            else if (r != 0 && c == 0)
                                puzzlePartPrefab = _puzzlePrefab_UpperLine;
                            else
                                puzzlePartPrefab = _puzzlePrefab_LeftUpperLine;
                        }

                        GameObject puzzlePart = Instantiate(puzzlePartPrefab, puzzlePrefab.transform);
                        puzzlePart.name = $"PuzzlePart_R{r}_C{c}";
                        puzzlePart.transform.rotation = Quaternion.identity;
                        puzzlePart.transform.localPosition = new Vector3(puzzlePart.transform.localScale.x * c, -puzzlePart.transform.localScale.y * r, 0);
                    }

                }
            }
            puzzlePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            puzzlePrefab.transform.rotation = Quaternion.identity;
            SetPivotToChildCenter(puzzlePrefab.transform);
            puzzlePrefab.transform.position = new Vector3(i*puzzlePrefab.transform.localScale.x * 4f, -3f, 0f);
        }
    }
    private void SetPivotToChildCenter(Transform puzzlePrefabTr)
    {
        if (puzzlePrefabTr.childCount == 0)
        {
            Debug.LogWarning("This GameObject has no children.");
            return;
        }

        // 자식들의 월드 좌표를 합산하여 중심 계산
        Vector3 center = Vector3.zero;
        foreach (Transform child in puzzlePrefabTr)
        {
            center += child.position;
        }
        center /= puzzlePrefabTr.childCount;

        // 부모의 위치를 중심으로 이동하고, 자식들의 위치를 새로운 부모 위치에 맞게 조정
        Vector3 parentPos = puzzlePrefabTr.position;
        Vector3 offset = parentPos - center;

        // 부모 오브젝트를 자식들의 중심으로 이동
        puzzlePrefabTr.position = center;

        // 자식들의 위치를 새로운 부모 위치에 맞게 이동
        foreach (Transform child in puzzlePrefabTr)
        {
            child.position += offset;
        }
    }
} // end of class
