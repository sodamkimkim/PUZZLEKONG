using UnityEngine;
[DefaultExecutionOrder(-8)]
public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private Transform _puzzleParentTr = null;
    [SerializeField]
    private GameObject[] PuzzlePrefabArr = new GameObject[13];
    public GameObject[] PuzzleGoArr { get; private set; }

    private void Awake()
    {
        PuzzleGoArr = new GameObject[3];
    }

    private void Start()
    {
        LazyStart();
    }

    private void LazyStart()
    {
        if (PuzzlePrefabArr == null || PuzzlePrefabArr.Length == 0)
        { Debug.Log("No Prefabs"); return; }
        InstantiateNewPuzzleGo(); 
    }
    private void InstantiateNewPuzzleGo()
    {
        PuzzleGoArr[0] = InstantiatePuzzle(0, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleGoArr[1] = InstantiatePuzzle(1, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleGoArr[2] = InstantiatePuzzle(2, Random.Range(0, PuzzlePrefabArr.Length));
        _checkPlacableCallback?.Invoke();
    }
    public delegate void CheckPlacable();
    private CheckPlacable _checkPlacableCallback;
    public void Iniit(CheckPlacable checkPlacableCallback)
    {
        _checkPlacableCallback = checkPlacableCallback;
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
            if (spr.gameObject.GetComponent<PZPart>() == null)
                spr.gameObject.AddComponent<PZPart>();
        }
        Puzzle puzzle = puzzleGo.GetComponent<Puzzle>();
        puzzle.Data = PuzzleArrayRepository.PZArrArr[puzzleArrIdx];
      //  Debug.Log($"idx:{instantiateIdx}: puzzleArrIdx_{puzzleArrIdx}: {Util.ConvertDoubleArrayToString(puzzle.Data)}");
        return puzzleGo;
    }
} // end of class
