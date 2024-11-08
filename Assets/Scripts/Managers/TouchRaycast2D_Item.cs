using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRaycast2D_Item : MonoBehaviour
{
    public static Item TouchingItem = null;

    private void Update()
    {
        ShotRay();
    }
    public void ShotRay()
    {
//        if (CompleteManager.IsProcessing) return;
//#if UNITY_EDITOR || UNITY_STANDALONE
//        Vector3 mouseWorldPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector2 rayOrigin2 = new Vector2(mouseWorldPos2.x, mouseWorldPos2.y);

//        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, Vector2.zero);

//        if (hit2.collider != null && hit2.transform.GetComponentInParent<Puzzle>() != null)
//        {
//            Item item = hit2.transform.GetComponentInParent<Item>();

//            if (Input.GetMouseButtonDown(0))
//            {
//                if (item.ActiveSelf == true)
//                    SetTouchBegin(puzzle, hit2);
//            }
//        }

//        if (Input.GetMouseButton(0))
//            SetTouchMoved(hit2);
//        else if (Input.GetMouseButtonUp(0))
//            _puzzlePlaceManager.PlacePuzzle(TouchingPuzzle, SetTouchEndPuzzleReturn);
//#endif
    }
    private void SetTouchBegin()
    {

    }
    private void SetTouchMoved()
    {

    }
    public void SetTouchEndItemReturn()
    {

    }
} // end of class
