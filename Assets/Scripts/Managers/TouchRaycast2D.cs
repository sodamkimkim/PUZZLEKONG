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
        // PC에서 마우스를 터치처럼 처리하는 부분
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
                // 터치 위치를 월드 좌표로 변환
                Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

                // Raycast 시작 위치 설정 (z축 제거)
                Vector2 rayOrigin = new Vector2(touchWorldPos.x, touchWorldPos.y);

                // Raycast발사 (터치된 위치 기준으로 Ray 발사)
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

                // Raycast에 충돌한 오브젝트가 있는지 확인
                if (hit.collider != null)
                {
                    // 충돌한 오브젝트의 이름 출력
                    Debug.Log("Hit 2D object: " + hit.collider.name);
                }

            }
        }
    }
} // end of class