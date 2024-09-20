using UnityEngine;

public class TouchRaycast2D : MonoBehaviour
{
    public GameObject TouchingGo = null;
    private Vector3 _selectedGoInitialPos = Vector3.zero;
    public void ShotRay(Enum.eTouchFunc eTouchFunc)
    { 
#if UNITY_EDITOR || UNITY_STANDALONE
        Vector3 mouseWorldPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayOrigin2 = new Vector2(mouseWorldPos2.x, mouseWorldPos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, Vector2.zero);

        if (hit2.collider != null && hit2.transform.GetComponentInParent<Puzzle>() != null)
        {
            Transform puzzleTr = hit2.transform.GetComponentInParent<Puzzle>().transform; 
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
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
             
            Vector2 rayOrigin = new Vector2(touchWorldPos.x, touchWorldPos.y);
             
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
             
            if (hit.collider != null && hit.transform.GetComponentInParent<Puzzle>() != null)
            {
                Transform puzzleTr = hit.transform.GetComponentInParent<Puzzle>().transform; 
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
        _selectedGoInitialPos = TouchingGo.transform.position;
        TouchingGo.transform.localScale = Factor.PZNormal;
    }
    private void SetTouchMoved(Transform puzzleTr, RaycastHit2D hit)
    {
        if (TouchingGo == null) return; 
        TouchingGo.transform.position = hit.point; 
    }
    public void SetTouchEnd()
    {
        if (TouchingGo == null) return;
        TouchingGo.transform.localScale = Factor.PZSmall;
        TouchingGo.transform.position = _selectedGoInitialPos;
        TouchingGo = null;
    }
} // end of class