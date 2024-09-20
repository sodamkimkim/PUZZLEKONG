using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    public GameObject[] PuzzlePrefabArr = null;
    public GameObject[] PuzzleArr = new GameObject[3]; 
 
    private void Start()
    {
        PuzzlePrefabArr = Resources.LoadAll<GameObject>(Path.Puzzles);
        if (PuzzlePrefabArr == null || PuzzlePrefabArr.Length == 0) { Debug.Log("?"); return; }
        Debug.Log(PuzzlePrefabArr.Length);
        PuzzleArr[0] = InstantiatePuzzle(0, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleArr[1] = InstantiatePuzzle(1, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleArr[2] = InstantiatePuzzle(2, Random.Range(0, PuzzlePrefabArr.Length));
    }
    private GameObject InstantiatePuzzle(int instantiateIdx, int puzzleArrIdx)
    {
        GameObject puzzleGo = Instantiate(PuzzlePrefabArr[puzzleArrIdx]);

        puzzleGo.transform.localScale = Factor.PZSmall;
        puzzleGo.transform.rotation = Quaternion.identity;

        Vector3 pos = Vector3.zero;
        if (instantiateIdx == 0)
            pos = new Vector3(-1.5f, -3.22f, 0f);
        else if (instantiateIdx == 1)
            pos = new Vector3(0f, -3.22f, 0f);
        else
            pos = new Vector3(1.5f, -3.22f, 0f);

        puzzleGo.transform.position = pos;
        return puzzleGo;
    }



} // end of class
