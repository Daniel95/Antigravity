using System;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDetection : MonoBehaviour {

    [SerializeField]
    private string ignoreRaycastLayer = "Ignore Raycast";

    public Action<Collider2D> detectedRight;
    public Action<Collider2D> detectedLeft;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(ignoreRaycastLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //fire the right delegates if the collision was to the right, or to the left
        if (IsToTheRight(collision.transform.position))
        {
            if (detectedRight != null)
            {
                detectedRight(collision);
            }
        }
        else
        {
            if (detectedLeft != null)
            {
                detectedLeft(collision);
            }
        }
    }

    //returns if the given position is to the right (local), or not
    private bool IsToTheRight(Vector2 _position)
    {
        Vector3 targetDir = _position - (Vector2)transform.position;

        return VectorMath.AngleDir(transform.forward, targetDir, transform.up) > 0;
    }
}
