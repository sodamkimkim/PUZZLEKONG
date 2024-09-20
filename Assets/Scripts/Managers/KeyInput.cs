using UnityEngine;

public class KeyInput : MonoBehaviour
{
    private TouchRaycast2D _touchRaycast2D = null;
    private void Awake()
    {
        _touchRaycast2D = this.GetComponent<TouchRaycast2D>();
    }
    private void Update()
    {
        KeyInputFunc();
    }
    private void KeyInputFunc()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButtonDown(0))
            _touchRaycast2D.ShotRay(Enum.eTouchFunc.TouchBegin);
        if (Input.GetMouseButton(0))
            _touchRaycast2D.ShotRay(Enum.eTouchFunc.TouchMoved);
        if (Input.GetMouseButtonUp(0))
            _touchRaycast2D.SetTouchEnd();
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                _touchRaycast2D.ShotRay(Enum.eTouchFunc.TouchBegin);
            if (touch.phase == TouchPhase.Moved)
                _touchRaycast2D.ShotRay(Enum.eTouchFunc.TouchMoved);
            if (touch.phase == TouchPhase.Ended)
                _touchRaycast2D.SetTouchEnd();
        }
    }
} // end of class