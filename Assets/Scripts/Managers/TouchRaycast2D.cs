using UnityEngine;

public class TouchRaycast2D : MonoBehaviour
{
    [SerializeField]
    private PuzzlePlaceManager _puzzlePlaceManager = null;

    public static GameObject TouchingGo = null;
    private Vector3 _selectedGoInitialPos = Vector3.zero;
    //private void Start()
    //{
    //    InvokeRepeating(nameof(ShotRay), 0f, 0.1f);
    //}
    private void Update()
    {
        ShotRay();
    }
    public void ShotRay()
    {
#if UNITY_EDITOR || UNITY_STANDALONE 
        Vector3 mouseWorldPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayOrigin2 = new Vector2(mouseWorldPos2.x, mouseWorldPos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, Vector2.zero);

        if (hit2.collider != null && hit2.transform.GetComponentInParent<Puzzle>() != null)
        {
            Transform puzzleTr = hit2.transform.GetComponentInParent<Puzzle>().transform;

            if (Input.GetMouseButtonDown(0))
                SetTouchBegin(puzzleTr);

            else if (Input.GetMouseButtonUp(0))
            {
                if (_puzzlePlaceManager.CheckPlacable_TouchingGo(TouchRaycast2D.TouchingGo))
                    _puzzlePlaceManager.PlacePuzzle();
                else
                    SetTouchEnd_PuzzleReturn();
            }
            else
                SetTouchEnd_PuzzleReturn();
        }
        else
            SetTouchEnd_PuzzleReturn();

        if (Input.GetMouseButton(0))
            SetTouchMoved(hit2);
#endif
        //#region Mobile Touch
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

        //    Vector2 rayOrigin = new Vector2(touchWorldPos.x, touchWorldPos.y);

        //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

        //    if (hit.collider != null && hit.transform.GetComponentInParent<Puzzle>() != null)
        //    {
        //        Transform puzzleTr = hit.transform.GetComponentInParent<Puzzle>().transform;
        //        if (touch.phase == TouchPhase.Began)
        //            SetTouchBegin(puzzleTr);
        //        else if (touch.phase == TouchPhase.Moved)
        //            SetTouchMoved(puzzleTr, hit);
        //        else if (touch.phase == TouchPhase.Ended)
        //        {
        //            if (_puzzlePlaceManager.CheckPlacable_TouchingGo(TouchRaycast2D.TouchingGo))
        //                _puzzlePlaceManager.PlacePuzzle();
        //            else
        //                SetTouchEnd_PuzzleReturn();
        //        }
        //        else
        //            SetTouchEnd_PuzzleReturn();
        //    }
        //    else
        //        SetTouchEnd_PuzzleReturn();
        //} 
        //#endregion
    }


    private void SetTouchBegin(Transform puzzleTr)
    {
        if (TouchingGo == puzzleTr.gameObject) return;
        TouchingGo = puzzleTr.gameObject;
        _selectedGoInitialPos = TouchingGo.transform.position;
        TouchingGo.transform.localScale = Factor.ScalePuzzleNormal;
    }
    private void SetTouchMoved(RaycastHit2D hit)
    {
        if (TouchingGo == null) return;
        TouchingGo.transform.position = hit.point;

        // TODO
        // placable check & mark
        // completable check & mark
    }
    public void SetTouchEnd_PuzzleReturn()
    {
        // TODO 
        // Puzzle Grid에 할당되지 않았다면
        if (TouchingGo == null) return;
        TouchingGo.transform.localScale = Factor.ScalePuzzleSmall;
        TouchingGo.transform.position = _selectedGoInitialPos;
        TouchingGo = null;
    }
} // end of class