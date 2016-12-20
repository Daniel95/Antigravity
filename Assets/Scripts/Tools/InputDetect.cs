using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputDetect {

    public static bool CheckUICollision(Vector2 _point)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(_point.x, _point.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
