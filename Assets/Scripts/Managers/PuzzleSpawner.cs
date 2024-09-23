using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _puzzleParentTr = null;
    public GameObject[] PuzzlePrefabArr = null;
    public GameObject[] PuzzleArr = new GameObject[3];

    private void Start()
    {
        LazyStart();
    }
    private void LazyStart()
    {
        PuzzlePrefabArr = Resources.LoadAll<GameObject>(new Path().Puzzles);
        if (PuzzlePrefabArr == null || PuzzlePrefabArr.Length == 0) { Debug.Log("?"); return; }
       // Debug.Log(PuzzlePrefabArr.Length);
        PuzzleArr[0] = InstantiatePuzzle(0, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleArr[1] = InstantiatePuzzle(1, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleArr[2] = InstantiatePuzzle(2, Random.Range(0, PuzzlePrefabArr.Length));
    }
    private GameObject InstantiatePuzzle(int instantiateIdx, int puzzleArrIdx)
    {
        GameObject puzzleGo = Instantiate(PuzzlePrefabArr[puzzleArrIdx], _puzzleParentTr);

        puzzleGo.transform.localScale = Factor.ScalePuzzleSmall;
        puzzleGo.transform.rotation = Quaternion.identity;

        Vector3 pos = Vector3.zero;
        if (instantiateIdx == 0)
            pos = new Vector3(-1.5f, -3.22f, 0f);
        else if (instantiateIdx == 1)
            pos = new Vector3(0f, -3.22f, 0f);
        else
            pos = new Vector3(1.5f, -3.22f, 0f);

        puzzleGo.transform.position = pos;
         
        foreach (SpriteRenderer spr in puzzleGo.GetComponentsInChildren<SpriteRenderer>())
        {
            switch (ThemeManager.ETheme)
            {
                case Enum.eTheme.Grey:
                    spr.color = Factor.Grey4;
                    break;
                case Enum.eTheme.Green:
                    spr.color = Factor.Green4;
                    break; 
                case Enum.eTheme.LightPurple:
                    spr.color = Factor.LightPurple4;
                    break;
                case Enum.eTheme.LightBlue:
                    spr.color = Factor.LightBlue4;
                    break;
                case Enum.eTheme.Pink:
                    spr.color = Factor.Pink4;
                    break;
                case Enum.eTheme.Mint:
                    spr.color = Factor.Grey4;
                    break;
            }
        }
        return puzzleGo;
    }



} // end of class
