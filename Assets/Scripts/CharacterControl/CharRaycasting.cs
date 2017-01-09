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
    private Transform midRightPoint;
    [SerializeField]
    private Transform midLeftPoint;
    [SerializeField]
    private Transform midTopPoint;
    [SerializeField]
    private Transform midBottomPoint;

    [SerializeField]
    private float rayLength = 0.1f;

    //a class that makes it simple to use raycasting in other scripts

    public bool CheckRayCornersUp()
    {
        if (CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.up, topRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.up))
            return true;

        return false;
    }

    public bool CheckRayCornersDown()
    {
        if (CheckIntersectionRaycast(bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.up, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.up))
            return true;

        return false;
    }

    public bool CheckRayCornerLeft()
    {
        if (CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.right, bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.right))
            return true;

        return false;
    }

    public bool CheckRayCornerRight()
    {
        if (CheckIntersectionRaycast(topRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.right, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.right))
            return true;

        return false;
    }

    private bool CheckDoubleRaycast(Vector2 _rayOrigin1, Vector2 _rayOrigin2, Vector2 _direction) {
        if (Physics2D.Raycast(_rayOrigin1, _direction, rayLength, layers) || Physics2D.Raycast(_rayOrigin2, _direction, rayLength, layers))
            return true;

        return false;
    }

    private bool CheckIntersectionRaycast(Vector2 _rayOrigin1, Vector2 _direction1, Vector2 _rayOrigin2, Vector2 _direction2)
    {
        if (Physics2D.Raycast(_rayOrigin1, _direction1, rayLength, layers) || Physics2D.Raycast(_rayOrigin2, _direction2, rayLength, layers))
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
        if (Physics2D.Raycast(midTopPoint.position, new Vector2(0, 1), rayLength, layers))
            return 1;
        if (Physics2D.Raycast(midBottomPoint.position, new Vector2(0, -1), rayLength, layers))
            return -1;
        else
            return 0;
    }

    //returns 1, 0 or -1 for the raycast results
    public int CheckHorizontalMiddleDir()
    {
        if (Physics2D.Raycast(midRightPoint.position, new Vector2(1, 0), rayLength, layers))
            return 1;
        if (Physics2D.Raycast(midLeftPoint.position, new Vector2(-1, 0), rayLength, layers))
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
            Debug.DrawRay(topLeftCornerPoint.position, (Quaternion.Euler(0, 0, -45) * transform.up) * rayLength);
            Debug.DrawRay(topRightCornerPoint.position, (Quaternion.Euler(0, 0, 45) * transform.up) * rayLength);

            //down
            Debug.DrawRay(bottomLeftCornerPoint.position, (Quaternion.Euler(0, 0, 45) * -transform.up) * rayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, (Quaternion.Euler(0, 0, -45) * -transform.up) * rayLength);

            //left
            Debug.DrawRay(topLeftCornerPoint.position, (Quaternion.Euler(0, 0, 45) * -transform.right) * rayLength);
            Debug.DrawRay(bottomLeftCornerPoint.position, (Quaternion.Euler(0, 0, -45) * -transform.right) * rayLength);

            //right
            Debug.DrawRay(topRightCornerPoint.position, (Quaternion.Euler(0, 0, -45) * transform.right) * rayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, (Quaternion.Euler(0, 0, 45) * transform.right) * rayLength);

            //middle
            Debug.DrawRay(midRightPoint.position, new Vector2(1, 0) * rayLength);
            Debug.DrawRay(midLeftPoint.position, new Vector2(-1, 0) * rayLength);
            Debug.DrawRay(midTopPoint.position, new Vector2(0, 1) * rayLength);
            Debug.DrawRay(midBottomPoint.position, new Vector2(0, -1) * rayLength);
            yield return new WaitForFixedUpdate();
        }
    }
}
