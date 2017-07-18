using UnityEngine;
using System.Collections;
using IoCPlus;

public class CharacterRaycastDirectionView : View, ICharacterRaycastDirection {

    [SerializeField] private LayerMask layers;
    [SerializeField] private bool showDebugRays = false;
    [SerializeField] private Transform topRightCornerPoint;
    [SerializeField] private Transform topLeftCornerPoint;
    [SerializeField] private Transform bottomRightCornerPoint;
    [SerializeField] private Transform bottomLeftCornerPoint;
    [SerializeField] private float cornerRayLength = 0.15f;
    [SerializeField] private float middleRayLength = 0.4f;

    private Vector2 rightUpDirection = new Vector2(1, 1);
    private Vector2 leftUpDirection = new Vector2(-1, 1);
    private Vector2 rightDownDirection = new Vector2(1, -1);
    private Vector2 leftDownDirection = new Vector2(-1, -1);

    public Vector2 GetCenterDirection() {
        return new Vector2(GetHorizontalMiddleDirection(), GetVerticalMiddleDirection());
    }

    public Vector2 GetCornersDirection() {
        Vector2 combinedCornerAxises = GetRayCornerTopRightAxis() +
            GetRayCornerTopLeftAxis() +
            GetRayCornerBottomRightAxis() +
            GetRayCornerBottomLeftAxis();

        Debug.Log("GetRayCornerTopRightAxis " + GetRayCornerTopRightAxis());
        Debug.Log("GetRayCornerTopLeftAxis " + GetRayCornerTopLeftAxis());
        Debug.Log("GetRayCornerBottomRightAxis " + GetRayCornerBottomRightAxis());
        Debug.Log("GetRayCornerBottomLeftAxis " + GetRayCornerBottomLeftAxis());

        Vector2 cornerDirection = new Vector2(Mathf.Clamp(combinedCornerAxises.x, -1, 1), Mathf.Clamp(combinedCornerAxises.y, -1, 1));
        Debug.Log("cornerDirection " + cornerDirection);
        return cornerDirection;
    }

    public int GetVerticalMiddleDirection() {
        if (CheckRaycastOther(transform.position, new Vector2(0, 1), middleRayLength, layers)) {
            return 1;
        } else if (CheckRaycastOther(transform.position, new Vector2(0, -1), middleRayLength, layers)) {
            return -1;
        } else {
            return 0;
        }
    }

    public int GetHorizontalMiddleDirection() {
        if (CheckRaycastOther(transform.position, new Vector2(1, 0), middleRayLength, layers)) {
            return 1;
        } else if (CheckRaycastOther(transform.position, new Vector2(-1, 0), middleRayLength, layers)) {
            return -1;
        } else {
            return 0;
        }
    }

    private Vector2 GetRayCornerTopRightAxis() {
        Vector2 topRightCornerAxis = GetDominantCornerAxis(topRightCornerPoint.position, rightDownDirection, leftUpDirection, cornerRayLength);
        return topRightCornerAxis;
    }

    private Vector2 GetRayCornerTopLeftAxis() {
        Vector2 cornerAxis = GetDominantCornerAxis(topLeftCornerPoint.position, leftDownDirection, rightUpDirection, cornerRayLength);
        Vector2 topLeftCornerAxis = new Vector2(cornerAxis.x * -1, cornerAxis.y);
        return topLeftCornerAxis;
    }

    private Vector2 GetRayCornerBottomRightAxis() {
        Vector2 cornerAxis = GetDominantCornerAxis(bottomRightCornerPoint.position, rightUpDirection, leftDownDirection, cornerRayLength);
        Vector2 bottomRightCornerAxis = new Vector2(cornerAxis.x, cornerAxis.y * -1);
        return bottomRightCornerAxis;
    }

    private Vector2 GetRayCornerBottomLeftAxis() {
        Vector2 cornerAxis = GetDominantCornerAxis(bottomLeftCornerPoint.position, leftUpDirection, rightDownDirection, cornerRayLength);
        Vector2 bottomLeftCornerAxis = new Vector2(cornerAxis.x * -1, cornerAxis.y * -1);
        return bottomLeftCornerAxis;
    }

    private Vector2 GetDominantCornerAxis(Vector2 rayOrigin, Vector2 rayXDirection, Vector2 rayYDirection, float rayLength) {
        float rayXOverlapFraction = GetRayOverlapFraction(rayOrigin, rayXDirection, rayLength);
        float rayYOverlapFraction = GetRayOverlapFraction(rayOrigin, rayYDirection, rayLength);

        if(rayXOverlapFraction == 0 && rayYOverlapFraction == 0) {
            return Vector2.zero;
        }

        bool rayXIsDominant = rayXOverlapFraction > rayYOverlapFraction;

        if(rayXIsDominant) {
            return Vector2.right;
        } else {
            return Vector2.up;
        }
    }

    private float GetRayOverlapFraction(Vector2 rayOrigin, Vector2 direction, float rayDistance) {
        RaycastHit2D raycastHit;

        Vector2 inversedRayOrigin = rayOrigin + (direction * rayDistance);
        bool rayHit = CheckRaycastOther(inversedRayOrigin, -direction, rayDistance, layers, out raycastHit);
        float inversedRayHitDistance = raycastHit.distance;

        if(rayHit) {
            float overlapFraction = rayDistance - inversedRayHitDistance;
            return overlapFraction;
        } else {
            return 0;
        }
    }

    /// <summary>
    /// checks if the raycast doesn't hit myself, and if it hits something.
    /// </summary>
    /// <param name="startPostion"></param>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    /// <param name="layers"></param>
    /// <returns></returns>
    private bool CheckRaycastOther(Vector2 startPostion, Vector2 direction, float length, LayerMask layers) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPostion, direction, length, layers);

        for (int i = 0; i < hits.Length; i++) {
            if(hits[i].collider.gameObject != gameObject) {
                return true;
            }
        }

        return false;
    }

    private bool CheckRaycastOther(Vector2 startPostion, Vector2 direction, float length, LayerMask layers, out RaycastHit2D raycastHit2D) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPostion, direction, length, layers);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider.gameObject != gameObject) {
                raycastHit2D = hits[i];
                return true;
            }
        }

        raycastHit2D = new RaycastHit2D();
        return false;
    }

    private void Start() {
        if (showDebugRays) {
            StartCoroutine(DebugRays());
        }
    }

    IEnumerator DebugRays() {
        while (true) {
            //topright
            Debug.DrawRay(topRightCornerPoint.position, rightDownDirection * cornerRayLength);
            Debug.DrawRay(topRightCornerPoint.position, leftUpDirection * cornerRayLength);

            //topleft
            Debug.DrawRay(topLeftCornerPoint.position, leftDownDirection * cornerRayLength);
            Debug.DrawRay(topLeftCornerPoint.position, rightUpDirection * cornerRayLength);

            //bottomright
            Debug.DrawRay(bottomRightCornerPoint.position, rightUpDirection * cornerRayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, leftDownDirection * cornerRayLength);

            //bottomleft
            Debug.DrawRay(bottomLeftCornerPoint.position, leftUpDirection * cornerRayLength);
            Debug.DrawRay(bottomLeftCornerPoint.position, rightDownDirection * cornerRayLength);

            //middle
            Debug.DrawRay(transform.position, new Vector2(1, 0) * middleRayLength);
            Debug.DrawRay(transform.position, new Vector2(-1, 0) * middleRayLength);
            Debug.DrawRay(transform.position, new Vector2(0, 1) * middleRayLength);
            Debug.DrawRay(transform.position, new Vector2(0, -1) * middleRayLength);
            yield return null;
        }
    }
}
