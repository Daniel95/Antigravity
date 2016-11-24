using UnityEngine;
using System.Collections;

public class CharRaycasting : MonoBehaviour {

    [SerializeField]
    private LayerMask layers;

    [SerializeField]
    private bool showDebugRays = false;

    [SerializeField]
    private Transform upperLeftCornerPoint;
    [SerializeField]
    private Transform upperRightCornerPoint;
    [SerializeField]
    private Transform bottomRightCornerPoint;
    [SerializeField]
    private Transform bottomLeftCornerPoint;

    [SerializeField]
    private float rayLength = 5;

    //a class that makes it simple to use raycasting in other scripts

    public bool CheckRaycastsUp()
    {
        if (CheckIntersectionRaycast(upperLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.up, upperRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.up))
            return true;

        return false;
    }

    public bool CheckRaycastsDown()
    {
        if (CheckIntersectionRaycast(bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.up, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.up))
            return true;

        return false;
    }

    public bool CheckRaycastsLeft()
    {
        if (CheckIntersectionRaycast(upperLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.right, bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.right))
            return true;

        return false;
    }

    public bool CheckRaycastsRight()
    {
        if (CheckIntersectionRaycast(upperRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.right, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.right))
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
    public int CheckVerticalDir() {

        if (CheckRaycastsUp())
            return 1;
        else if (CheckRaycastsDown())
            return -1;
        else
            return 0;
    }

    //returns 1, 0 or -1 for the raycast results
    public int CheckHorizontalDir()
    {
        if (CheckRaycastsRight())
            return 1;
        else if (CheckRaycastsLeft())
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
            Debug.DrawRay(upperLeftCornerPoint.position, (Quaternion.Euler(0, 0, -45) * transform.up) * rayLength);
            Debug.DrawRay(upperRightCornerPoint.position, (Quaternion.Euler(0, 0, 45) * transform.up) * rayLength);

            //down
            Debug.DrawRay(bottomLeftCornerPoint.position, (Quaternion.Euler(0, 0, 45) * -transform.up) * rayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, (Quaternion.Euler(0, 0, -45) * -transform.up) * rayLength);

            //left
            Debug.DrawRay(upperLeftCornerPoint.position, (Quaternion.Euler(0, 0, 45) * -transform.right) * rayLength);
            Debug.DrawRay(bottomLeftCornerPoint.position, (Quaternion.Euler(0, 0, -45) * -transform.right) * rayLength);

            //right
            Debug.DrawRay(upperRightCornerPoint.position, (Quaternion.Euler(0, 0, -45) * transform.right) * rayLength);
            Debug.DrawRay(bottomRightCornerPoint.position, (Quaternion.Euler(0, 0, 45) * transform.right) * rayLength);
            yield return new WaitForFixedUpdate();
        }
    }
}
