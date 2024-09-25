using UnityEngine;

public class ArrayTest : MonoBehaviour
{
    private void Start()
    {
        //   Debug.Log(Util.ConvertDoubleArrayToString(PZArrResource.PZArrArr[0]));
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[0]))); // 2, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[1]))); // 2, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[2]))); // 0, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[3]))); // 0, 3
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[4]))); // 2, 0
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[5]))); // 3, 0
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[6]))); // 2, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[7]))); // 2, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[8]))); // 2, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[9]))); // 2, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[10]))); // 2, 1
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[11]))); // 1, 2
        //Debug.Log(Util.ConvertArrayToString(GetLastIdx(PZArrResource.PZArrArr[12]))); // 1, 1
    }

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
} // end of class
