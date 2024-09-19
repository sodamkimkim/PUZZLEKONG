using UnityEngine;

public class TouchRaycast2D : MonoBehaviour
{
    private Ray _ray;
    private RaycastHit2D _hit;
    private static GameObject _rayTargetGo = null;
    private float _rayMaxDist = 10000f;

    private void Update()
    {
        ShotRay();
    }
    private void ShotRay()
    {
        // PC���� ���콺�� ��ġó�� ó���ϴ� �κ�
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 rayOrigin = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit 2D object: " + hit.collider.name);
            }
        }
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // ��ġ ��ġ�� ���� ��ǥ�� ��ȯ
                Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

                // Raycast ���� ��ġ ���� (z�� ����)
                Vector2 rayOrigin = new Vector2(touchWorldPos.x, touchWorldPos.y);

                // Raycast�߻� (��ġ�� ��ġ �������� Ray �߻�)
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

                // Raycast�� �浹�� ������Ʈ�� �ִ��� Ȯ��
                if (hit.collider != null)
                {
                    // �浹�� ������Ʈ�� �̸� ���
                    Debug.Log("Hit 2D object: " + hit.collider.name);
                }

            }
        }
    }
} // end of class