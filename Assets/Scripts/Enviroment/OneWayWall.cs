using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour {

    [SerializeField]
    private Collider2D ourCollider;

    [SerializeField]
    private string defaultLayer;

    [SerializeField]
    private string ignoreRaycastLayer;

    private void Start()
    {
        ourCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger && collision.CompareTag(Tags.Player))
        {
            if(IsToTheRight(collision.transform.position))
            {
                ourCollider.isTrigger = false;
                gameObject.layer = LayerMask.NameToLayer(defaultLayer);
            }
            else
            {
                ourCollider.isTrigger = true;
                gameObject.layer = LayerMask.NameToLayer(ignoreRaycastLayer);
            }
        }
    }

    //returns if the given position is to the right (local), or not
    private bool IsToTheRight(Vector2 _position)
    {
        Vector3 targetDir = _position - (Vector2)transform.position;

        if (VectorMath.AngleDir(transform.forward, targetDir, transform.up) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
