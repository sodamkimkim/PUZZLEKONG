using UnityEngine;

public class TouchRaycast2D : MonoBehaviour
{
    public GameObject TouchingGo = null; 
    public void ShotRay(Enum.eTouchFunc eTouchFunc)
    { 
#if UNITY_EDITOR || UNITY_STANDALONE
        Vector3 mouseWorldPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayOrigin2 = new Vector2(mouseWorldPos2.x, mouseWorldPos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, Vector2.zero);

        if (hit2.collider != null && hit2.transform.GetComponentInParent<Puzzle>() != null)
        {
            Transform puzzleTr = hit2.transform.GetComponentInParent<Puzzle>().transform;
            Debug.Log("Hit 2D object: " + hit2.collider.name);
            switch (eTouchFunc)
            {
                case Enum.eTouchFunc.TouchBegin:
                    SetTouchBegin(puzzleTr);
                    break;
                case Enum.eTouchFunc.TouchMoved:
                    SetTouchMoved(puzzleTr, hit2);
                    break;
            }
        } 
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // 터치 위치를 월드 좌표로 변환
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

            // Raycast 시작 위치 설정 (z축 제거)
            Vector2 rayOrigin = new Vector2(touchWorldPos.x, touchWorldPos.y);

            // Raycast발사 (터치된 위치 기준으로 Ray 발사)
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
             
            if (hit.collider != null && hit.transform.GetComponentInParent<Puzzle>() != null)
            {
                Transform puzzleTr = hit.transform.GetComponentInParent<Puzzle>().transform;
                Debug.Log("Hit 2D object: " + hit.collider.name);
                switch (eTouchFunc)
                {
                    case Enum.eTouchFunc.TouchBegin:
                        SetTouchBegin(puzzleTr);
                        break;
                    case Enum.eTouchFunc.TouchMoved:
                        SetTouchMoved(puzzleTr, hit);
                        break;
                }
            } 
        }
    }

    private void SetTouchBegin(Transform puzzleTr)
    {
        if (TouchingGo == puzzleTr.gameObject) return; 
        TouchingGo = puzzleTr.gameObject;
        TouchingGo.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }
    private void SetTouchMoved(Transform puzzleTr, RaycastHit2D hit)
    {
        if (TouchingGo == null) return; 
        TouchingGo.transform.position = hit.point; 
    }
    public void SetTouchEnd()
    {
        if (TouchingGo == null) return;
        TouchingGo.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        TouchingGo.transform.position = TouchingGo.GetComponent<Puzzle>().InitialPos;
        TouchingGo = null;
    }
} // end of class