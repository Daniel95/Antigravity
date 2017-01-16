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

    void Start()
    {
        if (showDebugRays)
            StartCoroutine(DebugRays());
    }

    //a class that makes it simple to use raycasting in other scripts

    private bool CheckRayCornersUp()
    {
        return CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.up, topRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.up, cornerRayLength);
    }

    private bool CheckRayCornersDown()
    {
        return CheckIntersectionRaycast(bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.up, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.up, cornerRayLength);
    }

    private bool CheckRayCornerLeft()
    {
        return CheckIntersectionRaycast(topLeftCornerPoint.position, Quaternion.Euler(0, 0, 45) * -transform.right, bottomLeftCornerPoint.position, Quaternion.Euler(0, 0, -45) * -transform.right, cornerRayLength);
    }

    private bool CheckRayCornerRight()
    {
        return CheckIntersectionRaycast(topRightCornerPoint.position, Quaternion.Euler(0, 0, -45) * transform.right, bottomRightCornerPoint.position, Quaternion.Euler(0, 0, 45) * transform.right, cornerRayLength);
    }

    private bool CheckDoubleRaycast(Vector2 rayOrigin1, Vector2 rayOrigin2, Vector2 direction, float rayLength)
    {
        return CheckRaycastOther(rayOrigin1, direction, rayLength, layers) || CheckRaycastOther(rayOrigin2, direction, rayLength, layers);
    }

    private bool CheckIntersectionRaycast(Vector2 rayOrigin1, Vector2 direction1, Vector2 rayOrigin2, Vector2 direction2, float rayLength)
    {
        return CheckRaycastOther(rayOrigin1, direction1, rayLength, layers) || CheckRaycastOther(rayOrigin2, direction2, rayLength, layers);
    }

    /// <summary>
    /// check the raycast results of our vertical corners, between -1 and 1.
    /// </summary>
    /// <returns></returns>
    public int CheckVerticalCornersDir() {

        if (CheckRayCornersUp())
            return 1;
        else if (CheckRayCornersDown())
            return -1;
        else
            return 0;
    }

    /// <summary>
    /// check the raycast results of our horizontal corners, between -1 and 1.
    /// </summary>
    /// <returns></returns>
    public int CheckHorizontalCornersDir()
    {
        if (CheckRayCornerRight())
            return 1;
        else if (CheckRayCornerLeft())
            return -1;
        else
            return 0;
    }

    /// <summary>
    /// check the raycast results of our vertical middle, between -1 and 1.
    /// </summary>
    /// <returns></returns>
    public int CheckVerticalMiddleDir()
    {
        if (CheckRaycastOther(transform.position, new Vector2(0, 1), middleRayLength, layers))
            return 1;
        if (CheckRaycastOther(transform.position, new Vector2(0, -1), middleRayLength, layers))
            return -1;
        else
            return 0;
    }

    /// <summary>
    /// check the raycast results of our horizontal middle, between -1 and 1.
    /// </summary>
    /// <returns></returns>
    public int CheckHorizontalMiddleDir()
    {
        if (CheckRaycastOther(transform.position, new Vector2(1, 0), middleRayLength, layers))
            return 1;
        if (CheckRaycastOther(transform.position, new Vector2(-1, 0), middleRayLength, layers))
            return -1;
        else
            return 0;
    }

    /// <summary>
    /// checks if the raycast doesn't hit myself, and if it hits something.
    /// </summary>
    /// <param name="_startPos"></param>
    /// <param name="_dir"></param>
    /// <param name="_length"></param>
    /// <param name="_layers"></param>
    /// <returns></returns>
    private bool CheckRaycastOther(Vector2 _startPos, Vector2 _dir, float _length, LayerMask _layers)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(_startPos, _dir, _length, _layers);

        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i].collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
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
