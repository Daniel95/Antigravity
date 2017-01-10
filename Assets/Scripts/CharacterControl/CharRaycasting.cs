using UnityEngine;
using System.Collections;

public class CharRaycasting : MonoBehaviour {

    [SerializeField]
    private LayerMask layers;

    [SerializeField]
    private bool showDebugRays = false;

    [SerializeField]
    private Transform topRightCornerPoint;
    [SerializeField]
    private Transform topLeftCornerPoint;
    [SerializeField]
    private Transform bottomRightCornerPoint;
    [SerializeField]
    private Transform bottomLeftCornerPoint;

    [SerializeField]
    private float cornerRayLength = 0.15f;

    [SerializeField]
    private float middleRayLength = 0.4f;

    //a class that makes it simple to use raycasting in other scripts

    public bool CheckRayCornersUp()
    {
        if (CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.up, topRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.up, cornerRayLength))
            return true;

        return false;
    }

    public bool CheckRayCornersDown()
    {
        if (CheckIntersectionRaycast(bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.up, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.up, cornerRayLength))
            return true;

        return false;
    }

    public bool CheckRayCornerLeft()
    {
        if (CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.right, bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.right, cornerRayLength))
            return true;

        return false;
    }

    public bool CheckRayCornerRight()
    {
        if (CheckIntersectionRaycast(topRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.right, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.right, cornerRayLength))
            return true;

        return false;
    }

    private bool CheckDoubleRaycast(Vector2 _rayOrigin1, Vector2 _rayOrigin2, Vector2 _direction, float _rayLength) {
        if (Physics2D.Raycast(_rayOrigin1, _direction, _rayLength, layers) || Physics2D.Raycast(_rayOrigin2, _direction, _rayLength, layers))
            return true;

        return false;
    }

    private bool CheckIntersectionRaycast(Vector2 _rayOrigin1, Vector2 _direction1, Vector2 _rayOrigin2, Vector2 _direction2, float _rayLength)
    {
        if (Physics2D.Raycast(_rayOrigin1, _direction1, _rayLength, layers) || Physics2D.Raycast(_rayOrigin2, _direction2, _rayLength, layers))
            return true;
        
        return false;
    }

    //returns 1, 0 or -1 for the raycast results
    public int CheckVerticalCornersDir() {

        if (CheckRayCornersUp())
            return 1;
        else if (CheckRayCornersDown())
            return -1;
        else
            return 0;
    }

    //returns 1, 0 or -1 for the raycast results
    public int CheckHorizontalCornersDir()
    {
        if (CheckRayCornerRight())
            return 1;
        else if (CheckRayCornerLeft())
            return -1;
        else
            return 0;
    }

    //returns 1, 0 or -1 for the raycast results
    public int CheckVerticalMiddleDir()
    {
        if (Physics2D.Raycast(transform.position, new Vector2(0, 1), middleRayLength, layers))
            return 1;
        if (Physics2D.Raycast(transform.position, new Vector2(0, -1), middleRayLength, layers))
            return -1;
        else
            return 0;
    }

    //returns 1, 0 or -1 for the raycast results
    public int CheckHorizontalMiddleDir()
    {
        if (Physics2D.Raycast(transform.position, new Vector2(1, 0), middleRayLength, layers))
            return 1;
        if (Physics2D.Raycast(transform.position, new Vector2(-1, 0), middleRayLength, layers))
            return -1;
        else
            return 0;
    }

    void Start() {
        if(showDebugRays)
            StartCoroutine(DebugRays());
    }

    IEnumerator DebugRays()
    {
        while (true)
        {
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
