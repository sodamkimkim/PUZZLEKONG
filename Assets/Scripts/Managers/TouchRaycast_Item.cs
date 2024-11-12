using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRaycast_Item : MonoBehaviour
{
    [SerializeField]
    private ItemManager _itemManager = null;
    public static Item TouchingItem = null;
    private Vector3 _itemPosBackUp = Vector3.zero;
    private void Update()
    {
     //   if (InGameManager.IsGameOver) return;
        if (CompleteManager.IsProcessing) return;
        ShotRay();
    }
    public void ShotRay()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Vector3 mouseWorldPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayOrigin2 = new Vector2(mouseWorldPos2.x, mouseWorldPos2.y);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, Vector2.zero);

        if (hit2.collider != null)
        {
            Item item = hit2.transform.GetComponentInParent<Item>();
            if (item != null)
                if (Input.GetMouseButtonDown(0) && item.IsForEffect == false)
                    SetTouchBegin(item);

        }

        if (Input.GetMouseButton(0) && TouchingItem != null && TouchingItem.IsForEffect == false)
            SetTouchMoved();
        if (Input.GetMouseButtonUp(0) && TouchingItem != null && TouchingItem.IsForEffect == false)
            _itemManager.PlaceItem(TouchingItem, SetTouchEndItemReturn);
#endif
        // TODO Mobile Touch case
    }
    private void SetTouchBegin(Item item)
    {
        if (TouchingItem == item) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -0.5f;
        _itemPosBackUp = pos;

        TouchingItem = item;
        TouchingItem.Anim("Anim1", true);
        TouchingItem.SetPos(true, pos);
        TouchingItem.SetScale(Enum.eItemScale.Big, 1f);
    }
    private void SetTouchMoved()
    {
        if (TouchingItem == null) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -0.5f;
        TouchingItem.SetPos(true, pos);

        if (pos == _itemPosBackUp) return;

        _itemPosBackUp = pos;

        _itemManager.CheckUseableReset();
        _itemManager.CheckUseable(TouchingItem);
    }

    // callback
    public void SetTouchEndItemReturn()
    {
        _itemManager.CheckUseableReset();
        if (TouchingItem == null) return;

        TouchingItem.Anim("Anim1", false);
        TouchingItem.SetScale(Enum.eItemScale.Small, 1f);
        TouchingItem.SetPos(false, Vector3.zero);
        TouchingItem = null;
    }
} // end of class
