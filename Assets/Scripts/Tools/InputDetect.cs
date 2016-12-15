using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputDetect {

    /*
    public static bool CheckUICollision(Vector2 _point)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(_point.x, _point.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }*/

    public static bool CheckUICollision(Vector2 _point)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(_point.x, _point.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    /*
    public static bool CheckLayerCollision(Vector2 _point, LayerMask _layerToCheck)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(_point.x, _point.y, 50), new Vector3(_point.x, _point.y, -50), 100, _layerToCheck);

        if(hit.collider != null)
            return true;
        else
            return false;

        //return Physics2D.Raycast(new Vector3(_point.x, _point.y, 50), new Vector3(_point.x, _point.y, -50), 100, _layerToCheck).collider != null;
    }*/
}
