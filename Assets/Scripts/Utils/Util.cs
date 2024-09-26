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

        // �ڽĵ��� ���� ��ǥ�� �ջ��Ͽ� �߽� ���
        Vector3 center = Vector3.zero;
        foreach (Transform child in parentTr)
        {
            center += child.position;
        }
        center /= parentTr.childCount;

        // �θ��� ��ġ�� �߽����� �̵��ϰ�, �ڽĵ��� ��ġ�� ���ο� �θ� ��ġ�� �°� ����
        Vector3 parentPos = parentTr.position;
        Vector3 offset = parentPos - center;

        // �θ� ������Ʈ�� �ڽĵ��� �߽����� �̵�
        parentTr.position = center;

        // �ڽĵ��� ��ġ�� ���ο� �θ� ��ġ�� �°� �̵�
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
    /// ���� �迭�� ���ڿ��� ��ȯ�ϴ� �޼���
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
                    sb.Append(", "); // �� ���� ���̿� ��ǥ �߰�
            }
            sb.AppendLine(); // �� �� ���� �ٹٲ� �߰�
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
                sb.Append(", ");   // �� ���� ���̿� ��ǥ �߰�
        }

        return sb.ToString();
    }
}
