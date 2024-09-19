using UnityEngine;

public class BuildPuzzlePrefab : MonoBehaviour
{
    public int[][,] PuzzleArr = new int[][,] {
            new int[,] // 퍼즐001
            {
                { 1, 1, 1, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0 ,0 },
                { 0, 0, 0 ,0 }
            },
            new int[,] // 퍼즐002
            {
                { 0, 0, 1, 0 },
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐003
            {
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐004
            {
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐005
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐006
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 }
            },
            new int[,] // 퍼즐007
            {
                { 1, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐008
            {
                { 0, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 }
            }
            ,
            new int[,] // 퍼즐009
            {
                { 1, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐010
            {
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐011
            {
                { 1, 0, 0, 0 },
                { 1, 1, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐012
            {
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // 퍼즐013
            {
                { 1, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            }
        };
    private void Start()
    {
        BuildPuzzlePrefabFunc();
    }
    private void BuildPuzzlePrefabFunc()
    {
        GameObject _puzzlePrefab_NoLine = Resources.Load<GameObject>("Prefabs/BuildPuzzle/Puzzle_NoLine");

        for (int i = 0; i < PuzzleArr.Length; i++)
        {
            int[,] selectedPuzzle = PuzzleArr[i];
            GameObject puzzlePrefab = new GameObject($"Puzzle_{i}");

            int puzzleRows = selectedPuzzle.GetLength(0);
            int puzzleCols = selectedPuzzle.GetLength(1);
            for (int r = 0; r < puzzleRows; r++)
            {
                for (int c = 0; c < puzzleCols; c++)
                {
                    if (selectedPuzzle[r, c] == 1)
                    {
                        bool isUpperOffset = false;
                        bool isLeftOffset = false; 
                        if (r == 0 && c == 0)
                        {
                            //puzzlePartPrefab = _puzzlePrefab_NoLine;
                            isUpperOffset = false;
                            isLeftOffset = false;
                        }
                        else
                        {
                            if (r == 0 && c != 0)
                            {
                                //puzzlePartPrefab = _puzzlePrefab_leftLine; 
                                isLeftOffset = true;
                            }
                            else if (r != 0 && c == 0)
                            {
                                //puzzlePartPrefab = _puzzlePrefab_UpperLine;
                                isUpperOffset = true;
                            }
                            else
                            {
                                //puzzlePartPrefab = _puzzlePrefab_LeftUpperLine;
                                isUpperOffset = true;
                                isLeftOffset = true;
                            }
                        }

                        GameObject puzzlePart = Instantiate(_puzzlePrefab_NoLine, puzzlePrefab.transform);
                        puzzlePart.name = $"PuzzlePart_R{r}_C{c}";
                        puzzlePart.transform.rotation = Quaternion.identity;
                        puzzlePart.transform.localPosition = new Vector3(puzzlePart.transform.localScale.x * c + (isLeftOffset ? 0.1f*c : 0), -(puzzlePart.transform.localScale.y * r + (isUpperOffset ? 0.1f*r : 0)), 0);
                    }

                }
            }
            puzzlePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            puzzlePrefab.transform.rotation = Quaternion.identity;
            SetPivotToChildCenter(puzzlePrefab.transform);
            puzzlePrefab.transform.position = new Vector3(i * puzzlePrefab.transform.localScale.x * 4f, -3f, 0f);

            puzzlePrefab.AddComponent<Rigidbody2D>();
            if(puzzlePrefab.GetComponent<Rigidbody2D>()!=null)
            {
                puzzlePrefab.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            puzzlePrefab.AddComponent<Puzzle>();
            puzzlePrefab.tag = "Puzzle";

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
