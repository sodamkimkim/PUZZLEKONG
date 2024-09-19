using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] PuzzlePrefabArr = null;
    public GameObject[] PuzzleArr = new GameObject[3];
    public Vector3 PuzzleInitialSize = new Vector3(0.2f, 0.2f, 1f);
    private void Awake()
    {
        PuzzlePrefabArr = Resources.LoadAll<GameObject>("Prefabs/Puzzles/");
    }
    private void Start()
    {
        if (PuzzlePrefabArr == null || PuzzlePrefabArr.Length == 0) { Debug.Log("?"); return; }
        Debug.Log(PuzzlePrefabArr.Length);
        PuzzleArr[0] = InstantiatePuzzle(0, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleArr[1] = InstantiatePuzzle(1, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleArr[2] = InstantiatePuzzle(2, Random.Range(0, PuzzlePrefabArr.Length));
    }
    private GameObject InstantiatePuzzle(int instantiateIdx, int puzzleArrIdx)
    {
        GameObject puzzleGo = Instantiate(PuzzlePrefabArr[puzzleArrIdx]);

        puzzleGo.transform.localScale = PuzzleInitialSize;
        puzzleGo.transform.rotation = Quaternion.identity;

        if (instantiateIdx == 0)
            puzzleGo.transform.position = new Vector3(-1.5f, -3.22f, 0f);
        else if (instantiateIdx == 1)
            puzzleGo.transform.position = new Vector3(0f, -3.22f, 0f);
        else
            puzzleGo.transform.position = new Vector3(1.5f, -3.22f, 0f);

        return puzzleGo;
    }



} // end of class
