using UnityEngine;

public class TouchRaycast : MonoBehaviour
{
    #region Dependency Injection
    [SerializeField]
    private PuzzlePlaceManager _puzzlePlaceManager = null;
    [SerializeField]
    private CompleteManager _completeManager = null;
    #endregion

    public static Puzzle TouchingPuzzle = null;
    private Vector3 _selectedGoInitialPos = Vector3.zero;
    private Vector3 _puzzlePosBackUp = Vector3.zero;
    private void Update()
    {
    //    if (InGameManager.IsGameOver) return;
        if (CompleteManager.IsProcessing) return; 
        if (ItemManager.IsProcessing) return;
        ShotRay();
    }
    public void ShotRay()
    { 
#if UNITY_EDITOR || UNITY_STANDALONE 
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        RaycastHit hit;

        if (Physics.Raycast(mouseWorldPos, transform.forward, out hit, 1000f))
        { 
            if (hit.transform.gameObject.tag == "PuzzlePart")
            { 
                if (Input.GetMouseButtonDown(0))
                {
                    Puzzle puzzle = hit.transform.GetComponentInParent<Puzzle>();
                    if (puzzle.ActiveSelf == true)
                        SetTouchBegin(puzzle);
                }
            }

        }

        if (Input.GetMouseButton(0) && TouchingPuzzle != null)
            SetTouchMoved();
        if (Input.GetMouseButtonUp(0) && TouchingPuzzle != null)
            _puzzlePlaceManager.PlacePuzzle(TouchingPuzzle, SetTouchEndPuzzleReturn);
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


    private void SetTouchBegin(Puzzle puzzle)
    {
        if (TouchingPuzzle == puzzle) return;

        TouchingPuzzle = puzzle;
        _selectedGoInitialPos = puzzle.SpawnPos;
        TouchingPuzzle.transform.localScale = Factor.ScalePuzzleNormal;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Factor.TouchingObjOffset;
        pos.z = Factor.PosPuzzleSpawn0.z;
        _puzzlePosBackUp = pos;

        TouchingPuzzle.transform.position = pos;
    }
    private void SetTouchMoved()
    {
        if (TouchingPuzzle == null) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Factor.TouchingObjOffset;
        pos.z = Factor.PosPuzzleSpawn0.z;
        TouchingPuzzle.transform.position = pos;

        if (pos == _puzzlePosBackUp) return;

        _puzzlePosBackUp = pos;

        _completeManager.MarkCompletableReset();
        if (_puzzlePlaceManager.MarkPlacable(TouchingPuzzle))
            _completeManager.MarkCompletable(TouchingPuzzle);
    }


    public void SetTouchEndPuzzleReturn()
    {
        _puzzlePlaceManager.MarkPlacableReset();
        _completeManager.MarkCompletableReset();
        if (TouchingPuzzle == null) return;

        TouchingPuzzle.transform.localScale = Factor.ScalePuzzleSmall;
        TouchingPuzzle.transform.position = _selectedGoInitialPos;
        TouchingPuzzle = null;
    }
} // end of class