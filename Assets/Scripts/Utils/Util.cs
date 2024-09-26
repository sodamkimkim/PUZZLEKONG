using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class Util
{
    public static void SetPivotToChildCenter(Transform parentTr)
    {
        if (parentTr.childCount == 0)
        {
            Debug.LogWarning("This GameObject has no children.");
            return;
        }

        // 자식들의 월드 좌표를 합산하여 중심 계산
        Vector3 center = Vector3.zero;
        foreach (Transform child in parentTr)
        {
            center += child.position;
        }
        center /= parentTr.childCount;

        // 부모의 위치를 중심으로 이동하고, 자식들의 위치를 새로운 부모 위치에 맞게 조정
        Vector3 parentPos = parentTr.position;
        Vector3 offset = parentPos - center;

        // 부모 오브젝트를 자식들의 중심으로 이동
        parentTr.position = center;

        // 자식들의 위치를 새로운 부모 위치에 맞게 이동
        foreach (Transform child in parentTr)
        {
            child.position += offset;
        }
    }
    public static void AddDictionary<T>(Dictionary<string, T> dic, string key, T value)
    {
        if (dic.ContainsKey(key))
            dic[key] = value;
        else
            dic.Add(key, value);
    }
    public static T CheckAndAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component =  go.AddComponent<T>();
   
        return component;
    }
    /// <summary>
    /// 이중 배열을 문자열로 변환하는 메서드
    /// </summary>
    /// <param name="dbArray"></param>
    /// <returns></returns>
    public static string ConvertDoubleArrayToString(int[,] dbArray)
    {
        StringBuilder sb = new StringBuilder();

        int rows = dbArray.GetLength(0);
        int cols = dbArray.GetLength(1);
        sb.AppendLine();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                sb.Append(dbArray[r, c].ToString());
                if (c < cols - 1)
                    sb.Append(", "); // 각 숫자 사이에 쉼표 추가
            }
            sb.AppendLine(); // 각 행 끝에 줄바꿈 추가
        }

        return sb.ToString();
    }
    public static string ConvertArrayToString(int[] array)
    {
        StringBuilder sb = new StringBuilder();

        for (int r = 0; r < array.Length; r++)
        {
            sb.Append(array[r].ToString());
            if (r < array.Length - 1)
                sb.Append(", ");   // 각 숫자 사이에 쉼표 추가
        }

        return sb.ToString();
    }
}
