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

    public int GetVerticalCornersDirection() {
        if (CheckRayCornersUp()) {
            return 1;
        } else if (CheckRayCornersDown()) {
            return -1;
        } else {
            return 0;
        }
    }

    public int GetHorizontalCornersDirection() {
        if (CheckRayCornerRight()) {
            return 1;
        } else if (CheckRayCornerLeft()) {
            return -1;
        } else {
            return 0;
        }
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

    public Vector2 GetMiddleDirection() {
        return new Vector2(GetHorizontalMiddleDirection(), GetVerticalMiddleDirection());
    }

    public Vector2 GetCornersDirection() {
        return new Vector2(GetHorizontalCornersDirection(), GetVerticalCornersDirection());
    }

    void Start() {
        if (showDebugRays) {
            StartCoroutine(DebugRays());
        }
    }

    private bool CheckRayCornersUp() {
        return CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.up, topRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.up, cornerRayLength);
    }

    private bool CheckRayCornersDown() {
        return CheckIntersectionRaycast(bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.up, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.up, cornerRayLength);
    }

    private bool CheckRayCornerLeft() {
        return CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.right, bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.right, cornerRayLength);
    }

    private bool CheckRayCornerRight() {
        return CheckIntersectionRaycast(topRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.right, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.right, cornerRayLength);
    }

    private bool CheckDoubleRaycast(Vector2 rayOrigin1, Vector2 rayOrigin2, Vector2 direction, float rayLength) {
        return CheckRaycastOther(rayOrigin1, direction, rayLength, layers) || CheckRaycastOther(rayOrigin2, direction, rayLength, layers);
    }

    private bool CheckIntersectionRaycast(Vector2 rayOrigin1, Vector2 direction1, Vector2 rayOrigin2, Vector2 direction2, float rayLength) {
        return CheckRaycastOther(rayOrigin1, direction1, rayLength, layers) || CheckRaycastOther(rayOrigin2, direction2, rayLength, layers);
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

    IEnumerator DebugRays() {
        while (true) {
            //up
            Debug.DrawRay(topLeftCornerPoint.position, (Quaternion.Euler(0, 0, -45) * transform.up) * cornerRayLength);
            Debug.DrawRay(topRightCornerPoint.position, (Quaternion.Euler(0, 0, 45) * transform.up) * cornerRayLength);

            //down
            Debug.DrawRay(bottomLeftCornerPoint.position, (Quaternion.Euler(0, 0, 45) * -transform.up) * cornerRayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, (Quaternion.Euler(0, 0, -45) * -transform.up) * cornerRayLength);

            //left
            Debug.DrawRay(topLeftCornerPoint.position, (Quaternion.Euler(0, 0, 45) * -transform.right) * cornerRayLength);
            Debug.DrawRay(bottomLeftCornerPoint.position, (Quaternion.Euler(0, 0, -45) * -transform.right) * cornerRayLength);

            //right
            Debug.DrawRay(topRightCornerPoint.position, (Quaternion.Euler(0, 0, -45) * transform.right) * cornerRayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, (Quaternion.Euler(0, 0, 45) * transform.right) * cornerRayLength);

            //middle
            Debug.DrawRay(transform.position, new Vector2(1, 0) * middleRayLength);
            Debug.DrawRay(transform.position, new Vector2(-1, 0) * middleRayLength);
            Debug.DrawRay(transform.position, new Vector2(0, 1) * middleRayLength);
            Debug.DrawRay(transform.position, new Vector2(0, -1) * middleRayLength);
            yield return new WaitForFixedUpdate();
        }
    }
}
