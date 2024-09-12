using UnityEngine;

public class PuzzleManager : MonoBehaviour
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
        //for (int i = 0; i < 3; i++)
        //    InstantiatePuzzle(i, Random.Range(0, PuzzleArr.Length));
    }
    private GameObject InstantiatePuzzle(int instantiateIdx, int puzzleArrIdx)
    {
        int[,] selectedPuzzle = PuzzleArr[puzzleArrIdx];
        GameObject parentGo = new GameObject($"Puzzle_{instantiateIdx}");
 
 



        parentGo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        parentGo.transform.rotation = Quaternion.identity;

        if (instantiateIdx == 0)
            parentGo.transform.position = new Vector3(-1.5f, -3.22f, 0f);
        else if (instantiateIdx == 1)
            parentGo.transform.position = new Vector3(0f, -3.22f, 0f);
        else
            parentGo.transform.position = new Vector3(1.5f, -3.22f, 0f);
        return parentGo;
    }

   

} // end of class
