using UnityEngine;
using System.Collections;
using IoCPlus;
using System.Collections.Generic;

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

    public RaycastData GetCombinedDirectionAndCenterDistances() {
        RaycastData centerRaycastData = GetCenterRaycastData();
        Vector2 cornersDirection = GetCornersDirection();

        Vector2 combinedDirection = centerRaycastData.Direction + cornersDirection;

        Vector2 raycastDataDirection = VectorHelper.Clamp(combinedDirection, -1, 1);

        return new RaycastData() {
            Direction = raycastDataDirection,
            Distance = centerRaycastData.Distance
        };
    }

    public Vector2 GetCenterDirection() {
        Vector2 centerDirection = new Vector2();

        Vector2 horizontalDirection = GetHorizontalMiddleRaycastData().Direction;
        Vector2 verticalDirection = GetVerticalMiddleRaycastData().Direction;
        centerDirection = horizontalDirection + verticalDirection;

        return centerDirection;
    }

    public RaycastData GetCenterRaycastData() {
        Vector2 horizontalDirection = GetHorizontalMiddleRaycastData().Direction;
        Vector2 verticalDirection = GetVerticalMiddleRaycastData().Direction;
        Vector2 horizontalDistance = GetHorizontalMiddleRaycastData().Distance;
        Vector2 verticalDistance = GetVerticalMiddleRaycastData().Distance;

        return new RaycastData() {
            Direction = horizontalDirection + verticalDirection,
            Distance = horizontalDistance + verticalDistance
        };
    }

    public Vector2 GetCornersDirection() {
        Vector2 combinedCornerDirection = GetCornerTopRightRaycastData().Direction +
            GetCornerTopLeftRaycastData().Direction +
            GetCornerBottomRightRaycastData().Direction +
            GetCornerBottomLeftRaycastData().Direction;

        Vector2 cornerDirection = VectorHelper.Clamp(combinedCornerDirection, -1, 1);
        return cornerDirection;
    }

    public RaycastData GetCornersRaycastData() {
        RaycastData topRightRaycastData = GetCornerTopRightRaycastData();
        RaycastData topLeftRaycastData = GetCornerTopLeftRaycastData();
        RaycastData bottomRightRaycastData = GetCornerBottomRightRaycastData();
        RaycastData bottomLeftRaycastData = GetCornerBottomLeftRaycastData();

        Vector2 combinedCornerDirection = topRightRaycastData.Direction +
            topLeftRaycastData.Direction +
            bottomRightRaycastData.Direction +
            bottomLeftRaycastData.Direction;

        Vector2 raycastDataDirection = VectorHelper.Clamp(combinedCornerDirection, -1, 1);

        List<Vector2> distances = new List<Vector2>() {
            topRightRaycastData.Distance,
            topLeftRaycastData.Distance,
            bottomRightRaycastData.Distance,
            bottomLeftRaycastData.Distance,
        };

        Vector2 raycastDataDistance = new Vector2();

        foreach (Vector2 distance in distances) {
            if(distance.x != 0) {
                raycastDataDistance = new Vector2(distance.x, raycastDataDistance.y);
            } else if(distance.y != 0) {
                raycastDataDistance = new Vector2(raycastDataDistance.x, distance.y);
            }
        }

        return new RaycastData() {
            Direction = raycastDataDirection,
            Distance = raycastDataDistance
        };
    }

    public RaycastData GetHorizontalMiddleRaycastData() {
        Vector2 direction = new Vector2();
        float distance = 0;

        if (CheckRaycastOther(transform.position, Vector2.right, middleRayLength, layers, out distance)) {
            direction = Vector2.right;
        } else if (CheckRaycastOther(transform.position, Vector2.left, middleRayLength, layers, out distance)) {
            direction = Vector2.left;
        } else {
            direction = Vector2.zero;
        }

        return new RaycastData() {
            Direction = direction,
            Distance = new Vector2(distance, 0)
        };
    }

    public RaycastData GetVerticalMiddleRaycastData() {
        Vector2 direction = new Vector2();
        float distance = 0;

        if (CheckRaycastOther(transform.position, Vector2.up, middleRayLength, layers, out distance)) {
            direction = Vector2.up;
        } else if (CheckRaycastOther(transform.position, Vector2.down, middleRayLength, layers, out distance)) {
            direction = Vector2.down;
        } else {
            direction = Vector2.zero;
        }

        return new RaycastData() {
            Direction = direction,
            Distance = new Vector2(0, distance)
        };
    }

    private RaycastData GetCornerTopRightRaycastData() {
        RaycastData topRightCornerRaycastData = GetDominantCornerAxis(topRightCornerPoint.position, rightDownDirection, leftUpDirection, cornerRayLength);
        return topRightCornerRaycastData;
    }

    private RaycastData GetCornerTopLeftRaycastData() {
        RaycastData topLeftCornerRaycastData = GetDominantCornerAxis(topLeftCornerPoint.position, leftDownDirection, rightUpDirection, cornerRayLength);

        Vector2 topLeftCornerDirection = new Vector2(topLeftCornerRaycastData.Direction.x * -1, topLeftCornerRaycastData.Direction.y);

        return new RaycastData() {
            Direction = topLeftCornerDirection,
            Distance = topLeftCornerRaycastData.Distance
        };
    }

    private RaycastData GetCornerBottomRightRaycastData() {
        RaycastData bottomRightCornerRaycastData = GetDominantCornerAxis(bottomRightCornerPoint.position, rightUpDirection, leftDownDirection, cornerRayLength);

        Vector2 bottomRightCornerDirection = new Vector2(bottomRightCornerRaycastData.Direction.x, bottomRightCornerRaycastData.Direction.y * -1);

        return new RaycastData() {
            Direction = bottomRightCornerDirection,
            Distance = bottomRightCornerRaycastData.Distance
        };
    }

    private RaycastData GetCornerBottomLeftRaycastData() {
        RaycastData bottomLeftCornerRaycastData = GetDominantCornerAxis(bottomLeftCornerPoint.position, leftUpDirection, rightDownDirection, cornerRayLength);

        Vector2 bottomLeftCornerDirection = new Vector2(bottomLeftCornerRaycastData.Direction.x * -1, bottomLeftCornerRaycastData.Direction.y * -1);

        return new RaycastData() {
            Direction = bottomLeftCornerDirection,
            Distance = bottomLeftCornerRaycastData.Distance
        };
    }

    private RaycastData GetDominantCornerAxis(Vector2 rayOrigin, Vector2 rayXDirection, Vector2 rayYDirection, float rayLength) {
        float rayXOverlapFraction = GetRayOverlapFraction(rayOrigin, rayXDirection, rayLength);
        float rayYOverlapFraction = GetRayOverlapFraction(rayOrigin, rayYDirection, rayLength);

        if(rayXOverlapFraction == 0 && rayYOverlapFraction == 0) {
            return new RaycastData();
        }

        RaycastData raycastData = new RaycastData();
        float distance = 0;
        bool rayXIsDominant = rayXOverlapFraction > rayYOverlapFraction;

        if(rayXIsDominant) {
            CheckRaycastOther(rayOrigin, rayXDirection, rayLength, layers, out distance);
            raycastData.Distance = new Vector2(distance, 0);
            raycastData.Direction = Vector2.right;
        } else {
            CheckRaycastOther(rayOrigin, rayXDirection, rayLength, layers, out distance);
            raycastData.Distance = new Vector2(0, distance);
            raycastData.Direction = Vector2.up;
        }

        return raycastData;
    }

    private float GetRayOverlapFraction(Vector2 rayOrigin, Vector2 direction, float rayDistance) {
        RaycastHit2D raycastHit;

        Vector2 inversedRayOrigin = rayOrigin + (direction * rayDistance);
        bool rayHit = CheckRaycastOther(inversedRayOrigin, -direction, rayDistance, layers, out raycastHit);
        float inversedRayHitDistance = raycastHit.distance;

        if (!rayHit) { return 0; }

        float overlapFraction = rayDistance - inversedRayHitDistance;
        return overlapFraction;
    }

    /// <summary>
    /// checks if the raycast doesn't hit myself, and if it hits something.
    /// </summary>
    /// <param name="startPostion"></param>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    /// <param name="layers"></param>
    /// <returns></returns>
    private bool CheckRaycastOther(Vector2 startPostion, Vector2 direction, float length, LayerMask layers, out float distance) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPostion, direction, length, layers);

        for (int i = 0; i < hits.Length; i++) {
            if(hits[i].collider.gameObject != gameObject) {
                distance = hits[i].distance;
                return true;
            }
        }

        distance = 0;
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
