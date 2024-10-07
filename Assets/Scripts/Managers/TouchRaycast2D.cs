using UnityEngine;

public class TouchRaycast2D : MonoBehaviour
{
    #region Hidden Private Variables 
    #endregion

    [SerializeField]
    private PuzzlePlaceManager _puzzlePlaceManager = null;
    public static Puzzle TouchingPuzzle = null;
    private Vector3 _selectedGoInitialPos = Vector3.zero;
    private Vector3 _mousePosBackUp = Vector3.zero;
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
            Puzzle puzzle = hit2.transform.GetComponentInParent<Puzzle>();

            if (Input.GetMouseButtonDown(0))
                SetTouchBegin(puzzle, hit2);
        }

        if (Input.GetMouseButton(0))
        {
            SetTouchMoved(hit2);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _puzzlePlaceManager.GetIdxDic(false, TouchingPuzzle);
            _puzzlePlaceManager.GetTriggeredPlacableIdxReset();

            if (_puzzlePlaceManager.CheckPlacable(TouchingPuzzle))
                _puzzlePlaceManager.PuzzlePlace();
            else
                SetTouchEnd_PuzzleReturn();
        }
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


    private void SetTouchBegin(Puzzle puzzle, RaycastHit2D hit)
    {
        if (TouchingPuzzle == puzzle) return;

        Vector3 mousePos = Input.mousePosition;
        _mousePosBackUp = mousePos;

        TouchingPuzzle = puzzle;
        _selectedGoInitialPos = puzzle.SpawnPos;
        TouchingPuzzle.transform.localScale = Factor.ScalePuzzleNormal;

        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = Factor.PosPuzzleSpawn0.z;
        TouchingPuzzle.transform.position = pos;
        _puzzlePlaceManager.GetIdxDic(true, TouchingPuzzle);
    }
    private void SetTouchMoved(RaycastHit2D hit)
    {
        if (TouchingPuzzle == null) return;
        Vector3 mousePos = Input.mousePosition;
        //  Vector3 deffer = mousePos - _mousePosBackUp;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = Factor.PosPuzzleSpawn0.z;
        //if (pos == _mousePosBackUp /*|| Mathf.Abs(deffer.x) < 3f || Mathf.Abs(deffer.y) < 3f*/)
        //{
        //    TouchingPuzzle.transform.position = pos;
        //    return;
        //}
        //if (pos != _mousePosBackUp)
        //{
        //    _mousePosBackUp = pos;
            TouchingPuzzle.transform.position = pos;

            //  _puzzlePlaceManager.GetTriggeredPlacableIdxReset();
            _puzzlePlaceManager.GetTriggeredPlacableIdx(TouchingPuzzle);
        //}

        // TODO
        // placable check & mark 
        // completable check & mark
    }
    public void SetTouchEnd_PuzzleReturn()
    {
        // TODO 
        // Puzzle Grid에 할당되지 않았다면
        if (TouchingPuzzle == null) return;
        TouchingPuzzle.transform.localScale = Factor.ScalePuzzleSmall;
        TouchingPuzzle.transform.position = _selectedGoInitialPos;
        TouchingPuzzle = null;
    }
} // end of class