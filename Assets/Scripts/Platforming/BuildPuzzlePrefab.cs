using UnityEngine;
public class BuildPuzzlePrefab : MonoBehaviour
{
    private void Start()
    {
        BuildPuzzlePrefabFunc();
    }
    private void BuildPuzzlePrefabFunc()
    {
        GameObject _puzzlePrefab_NoLine = Resources.Load<GameObject>(Path.PuzzlePartPrefab);

        for (int i = 0; i < PuzzleArrayRepository.PZArrArr.Length; i++)
        {
            int[,] selectedPuzzle = PuzzleArrayRepository.PZArrArr[i];
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
                                isLeftOffset = true;
                            else if (r != 0 && c == 0)
                                isUpperOffset = true;
                            else
                            {
                                isUpperOffset = true;
                                isLeftOffset = true;
                            }
                        }

                        GameObject puzzlePartGo = Instantiate(_puzzlePrefab_NoLine, puzzlePrefab.transform);
                        puzzlePartGo.name = $"{r},{c}";
                        puzzlePartGo.transform.rotation = Quaternion.identity;
                        puzzlePartGo.transform.localPosition = new Vector3(puzzlePartGo.transform.localScale.x * c + (isLeftOffset ? 0.1f * c : 0), -(puzzlePartGo.transform.localScale.y * r + (isUpperOffset ? 0.1f * r : 0)), 0);

                        BoxCollider boxCollider = Util.CheckAndAddComponent<BoxCollider>(puzzlePartGo);
                        boxCollider.size = new Vector2(1.1f, 1.1f);

                        Util.CheckAndAddComponent<PZPart>(puzzlePartGo);
                    }

                }
            }
            puzzlePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            puzzlePrefab.transform.rotation = Quaternion.identity;

            Util.SetPivotToChildCenter(puzzlePrefab.transform);

            puzzlePrefab.transform.position = new Vector3(i * puzzlePrefab.transform.localScale.x * 4f, -3f, 1f);

            //Rigidbody rigidbody = Util.CheckAndAddComponent<Rigidbody>(puzzlePrefab);
            //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            //rigidbody.useGravity = false;
            puzzlePrefab.tag = "Puzzle";
        }
    }
} // end of class
