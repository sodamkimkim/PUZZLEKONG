using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private int[,] _data;
    public int[,] Data
    {
        get => _data;
        set
        {
            _data = value;
            LastIdx = GetLastIdx(_data);
        }
    }

    private int[] _lastIdx;
    public int[] LastIdx { get => _lastIdx; private set => _lastIdx = value; }
    private int[] GetLastIdx(int[,] dbArr)
    {
        int[] rcIdxArr = new int[2] { 0, 0 };

        int row = dbArr.GetLength(0);
        int col = dbArr.GetLength(1);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                if (dbArr[r, c] == 1)
                {
                    if (rcIdxArr[0] < r)
                        rcIdxArr[0] = r;

                    // 이전 값이랑 비교해서 크면 넣어야 함
                    if (rcIdxArr[1] < c)
                        rcIdxArr[1] = c;
                }
            }
        }
        return rcIdxArr;
    }

    private Vector3 _spawnPos;
    public Vector3 SpawnPos { get => _spawnPos; set => _spawnPos = value; }
} // end of class
