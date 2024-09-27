using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _puzzleParentTr = null;
    [SerializeField]
    private GameObject[] PuzzlePrefabArr = new GameObject[13];

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
        Util.CheckAndAddComponent<Rigidbody2D>(puzzleGo);
        Rigidbody2D rigidbody2D = puzzleGo.GetComponent<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        // Puzzle component
        Util.CheckAndAddComponent<Puzzle>(puzzleGo);
        if (puzzleGo.GetComponent<Puzzle>() == null)
            puzzleGo.AddComponent<Puzzle>();

        Puzzle puzzle = puzzleGo.GetComponent<Puzzle>();
        puzzle.Data = PuzzleArrayRepository.PZArrArr[puzzlePrefabArrIdx];
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
            Util.CheckAndAddComponent<PZPart>(spr.gameObject);

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
