using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    [SerializeField]
    public Transform _puzzleParentTr = null;
    private GameObject[] PuzzlePrefabArr = null;

    private void Awake()
    {
        GameObject[] tempPuzzlePrefabArr = Resources.LoadAll<GameObject>($"{Path.Puzzles}");
        PuzzlePrefabArr = new GameObject[tempPuzzlePrefabArr.Length];
        for (int i = 0; i < PuzzlePrefabArr.Length; i++)
        {
            foreach (GameObject go in tempPuzzlePrefabArr)
            {
                if (go.name == $"Puzzle_{i}")
                    PuzzlePrefabArr[i] = go;
            }
        }
    }
    public void DestroyChilds()
    {
        if (_puzzleParentTr.childCount > 0 && _puzzleParentTr.childCount > 0)
            foreach (Puzzle child in _puzzleParentTr.GetComponentsInChildren<Puzzle>())
                DestroyImmediate(child.gameObject);
    }
    public GameObject SpawnPuzzle(int puzzleGoIdx)
    {
        if (PuzzlePrefabArr == null || PuzzlePrefabArr.Length == 0)
        { Debug.Log("No Prefabs"); return null; }


        int puzzlePrefabArrIdx = Random.Range(0, PuzzlePrefabArr.Length);
        GameObject puzzleGo = Instantiate(PuzzlePrefabArr[puzzlePrefabArrIdx], _puzzleParentTr);
        puzzleGo.tag = "Puzzle";
        puzzleGo.transform.localScale = Factor.ScalePuzzleSmall;
        puzzleGo.transform.rotation = Quaternion.identity;

        Vector3 pos = Vector3.zero;
        if (puzzleGoIdx == 0)
            pos = Factor.PosPuzzleSpawn0;
        else if (puzzleGoIdx == 1)
            pos = Factor.PosPuzzleSpawn1;
        else
            pos = Factor.PosPuzzleSpawn2;

        puzzleGo.transform.position = pos;

        // Rigidbody component
        //Util.CheckAndAddComponent<Rigidbody>(puzzleGo);
        //Rigidbody rigidbody = puzzleGo.GetComponent<Rigidbody>();
        //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        //rigidbody.useGravity = false;

        // Puzzle component
        Util.CheckAndAddComponent<Puzzle>(puzzleGo);
        if (puzzleGo.GetComponent<Puzzle>() == null)
            puzzleGo.AddComponent<Puzzle>();

        Puzzle puzzle = puzzleGo.GetComponent<Puzzle>();
        puzzle.Data = PuzzleArrayRepository.PZArrArr[puzzlePrefabArrIdx];
        foreach (PZPart pzPart in puzzleGo.GetComponentsInChildren<PZPart>())
        {
            SpriteRenderer spr = pzPart.gameObject.GetComponent<SpriteRenderer>();
            switch (ThemaManager.ETheme)
            {
                case Enum.eTheme.Grey:
                    spr.color = Factor.Grey3;
                    break;
                case Enum.eTheme.Green:
                    spr.color = Factor.Green3;
                    break;
                case Enum.eTheme.LightPurple:
                    spr.color = Factor.LightPurple3;
                    break;
                case Enum.eTheme.LightBlue:
                    spr.color = Factor.LightBlue3;
                    break;
                case Enum.eTheme.Pink:
                    spr.color = Factor.Pink3;
                    break;
                case Enum.eTheme.Yellow:
                    spr.color = Factor.Yellow3;
                    break;
                case Enum.eTheme.Mint:
                    spr.color = Factor.Green3;
                    break;
            } 
             
            pzPart.ParentPuzzle = puzzle;
            pzPart.Spr = spr;

            puzzle.ChildPZPartList.Add(pzPart);
            puzzle.ChildColor = spr.color;
        }
        puzzle.SpawnPos = pos;
        return puzzleGo;
    }
} // end of class
