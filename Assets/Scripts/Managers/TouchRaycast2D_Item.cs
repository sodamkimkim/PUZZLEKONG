using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRaycast2D_Item : MonoBehaviour
{
    [SerializeField]
    private ItemManager _itemManager = null;
    public static Item TouchingItem = null;
    private Vector3 _itemPosBackUp = Vector3.zero;
    private void Update()
    {
        ShotRay();
    }
    public void ShotRay()
    {
        if (CompleteManager.IsProcessing) return;
#if UNITY_EDITOR || UNITY_STANDALONE
        Vector3 mouseWorldPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayOrigin2 = new Vector2(mouseWorldPos2.x, mouseWorldPos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, Vector2.zero);

        if (hit2.collider != null && hit2.transform.GetComponentInParent<Item>() != null)
        {
            Item item = hit2.transform.GetComponentInParent<Item>();

            if (Input.GetMouseButtonDown(0))
                SetTouchBegin(item);
        }

        if (Input.GetMouseButton(0))
            SetTouchMoved();
        if (Input.GetMouseButtonUp(0))
            _itemManager.PlaceItem(TouchingItem, SetTouchEndItemReturn);
#endif
        // TODO Mobile Touch case
    }
    private void SetTouchBegin(Item item)
    {
        if (TouchingItem == item) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Factor.TouchingObjOffset;
        pos.z = 0;
        _itemPosBackUp = pos;

        TouchingItem = item;
        Anim("Anim1", true);
        TouchingItem.transform.position = pos;
        TouchingItem.transform.localScale = TouchingItem.LocalScaleSmall * 3f;
    }
    private void SetTouchMoved()
    {
        if (TouchingItem == null) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Factor.TouchingObjOffset;
        pos.z = 0;
        TouchingItem.transform.position = pos;

        if (pos == _itemPosBackUp) return;

        _itemPosBackUp = pos;

        // TODO Item Useable 
        _itemManager.CheckUseableReset();
        _itemManager.CheckUseable(TouchingItem);
    }

    // callback
    public void SetTouchEndItemReturn()
    {
        _itemManager.CheckUseableReset();
        if (TouchingItem == null) return;
         
        Anim("Anim1", false);
        TouchingItem.transform.localScale = TouchingItem.LocalScaleSmall;
        TouchingItem.transform.localPosition = Vector3.zero;
        TouchingItem = null;
    }

    private void Anim(string anim, bool isAnim)
    {
        if (TouchingItem == null) return;
        if (TouchingItem.Animator == null) return;

        TouchingItem.Animator.SetBool(anim, isAnim);
    }
} // end of class
