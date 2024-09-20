using UnityEngine;

public class BuildPuzzlePrefab : MonoBehaviour
{
    public int[][,] PuzzleArr = new int[][,] {
            new int[,] // ∆€¡Ò001
            {
                { 1, 1, 1, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0 ,0 },
                { 0, 0, 0 ,0 }
            },
            new int[,] // ∆€¡Ò002
            {
                { 0, 0, 1, 0 },
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò003
            {
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò004
            {
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò005
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò006
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò007
            {
                { 1, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò008
            {
                { 0, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 }
            }
            ,
            new int[,] // ∆€¡Ò009
            {
                { 1, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò010
            {
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò011
            {
                { 1, 0, 0, 0 },
                { 1, 1, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò012
            {
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò013
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
        GameObject _puzzlePrefab_NoLine = Resources.Load<GameObject>(Path.PuzzlePartPrefab);

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
                        puzzlePart.name = $"PuzzlePart";
                        puzzlePart.transform.rotation = Quaternion.identity;
                        puzzlePart.transform.localPosition = new Vector3(puzzlePart.transform.localScale.x * c + (isLeftOffset ? 0.1f * c : 0), -(puzzlePart.transform.localScale.y * r + (isUpperOffset ? 0.1f * r : 0)), 0);
                        if (puzzlePart.GetComponent<BoxCollider2D>() != null)
                            puzzlePart.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 1.1f);
                    }

                }
            }
            puzzlePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            puzzlePrefab.transform.rotation = Quaternion.identity;

            Util util = new Util();
            util.SetPivotToChildCenter(puzzlePrefab.transform);

            puzzlePrefab.transform.position = new Vector3(i * puzzlePrefab.transform.localScale.x * 4f, -3f, 0f);

            puzzlePrefab.AddComponent<Rigidbody2D>();
            if (puzzlePrefab.GetComponent<Rigidbody2D>() != null)
            {
                puzzlePrefab.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            puzzlePrefab.AddComponent<Puzzle>();
            puzzlePrefab.tag = "Puzzle";
        }
    }

} // end of class
