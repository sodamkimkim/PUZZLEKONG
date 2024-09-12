using UnityEngine;

public class AdjustSafeArea : MonoBehaviour
{
    private void Start()
    {
        ApplySafeArea();
    }
    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        RectTransform rtr = GetComponent<RectTransform>();

        rtr.offsetMin = safeArea.position;
        rtr.offsetMax = safeArea.position + safeArea.size - new Vector2(Screen.width, Screen.height);
        
        
    }
} // end of class