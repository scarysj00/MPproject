using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchLogger : MonoBehaviour
{
    void OnEnable()  { EnhancedTouchSupport.Enable(); }
    void OnDisable() { EnhancedTouchSupport.Disable(); }

    void Update()
    {
        foreach (var t in Touch.activeTouches)
            Debug.Log($"터치 {t.touchId} 위치 {t.screenPosition} 상태 {t.phase}");
    }
}