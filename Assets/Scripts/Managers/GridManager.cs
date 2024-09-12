using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gridGo = null;

    // 그리드 크기
    public int[,] GridRowsCols = { { 9, 9 } };
} // end of class
