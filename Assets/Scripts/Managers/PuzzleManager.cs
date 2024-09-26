using UnityEngine;
[DefaultExecutionOrder(-8)]
public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private Transform _puzzleParentTr = null;

    [SerializeField]
    private GameObject[] PuzzlePrefabArr = new GameObject[13];

    private GameObject[] _puzzleGoArr;

    public GameObject[] PuzzleGoArr { get => _puzzleGoArr; private set => _puzzleGoArr = value; }

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
        PuzzleGoArr[0] = SpawnPuzzle(0, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleGoArr[1] = SpawnPuzzle(1, Random.Range(0, PuzzlePrefabArr.Length));
        PuzzleGoArr[2] = SpawnPuzzle(2, Random.Range(0, PuzzlePrefabArr.Length));
        _checkPlacableCallback?.Invoke();
    }
    public delegate void CheckPlacable();
    private CheckPlacable _checkPlacableCallback { get; set; }
    public void Iniit(CheckPlacable checkPlacableCallback)
    {
        _checkPlacableCallback = checkPlacableCallback;
    }
    private GameObject SpawnPuzzle(int instantiateIdx, int puzzleArrIdx)
    {
        GameObject puzzleGo = Instantiate(PuzzlePrefabArr[puzzleArrIdx], _puzzleParentTr);
        puzzleGo.tag = "Puzzle";
        puzzleGo.transform.localScale = Factor.ScalePuzzleSmall;
        puzzleGo.transform.rotation = Quaternion.identity;

        Vector3 pos = Vector3.zero;
        if (instantiateIdx == 0)
            pos = Factor.PosPuzzleSpawn0;
        else if (instantiateIdx == 1)
            pos = Factor.PosPuzzleSpawn1;
        else
            pos = Factor.PosPuzzleSpawn2;

        puzzleGo.transform.position = pos;

        // Rigidbody component
        Util.AddComponent<Rigidbody2D>(puzzleGo);
        Rigidbody2D rigidbody2D = puzzleGo.GetComponent<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        // Puzzle component
        Util.AddComponent<Puzzle>(puzzleGo);
        if (puzzleGo.GetComponent<Puzzle>() == null)
            puzzleGo.AddComponent<Puzzle>();

        Puzzle puzzle = puzzleGo.GetComponent<Puzzle>();
        puzzle.Data = PuzzleArrayRepository.PZArrArr[puzzleArrIdx];
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
            Util.AddComponent<PZPart>(spr.gameObject);

            puzzle.ChildColor = spr.color;
            PZPart pZPart = spr.gameObject.GetComponent<PZPart>();
            pZPart.ParentPuzzle = puzzle;
            puzzle.ChildPZPartList.Add(pZPart);
            puzzle.ChildSprList.Add(spr);
        }


        puzzle.SpawnPos = pos;
        //  Debug.Log($"idx:{instantiateIdx}: puzzleArrIdx_{puzzleArrIdx}: {Util.ConvertDoubleArrayToString(puzzle.Data)}");
        return puzzleGo;
    }
} // end of class
