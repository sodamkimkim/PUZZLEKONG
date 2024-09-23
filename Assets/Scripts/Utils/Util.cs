using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
}
