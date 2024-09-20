using UnityEngine;

public class Util  
{
    public void SetPivotToChildCenter(Transform parentTr)
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

}
