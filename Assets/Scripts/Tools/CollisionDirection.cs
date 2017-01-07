using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDirection : MonoBehaviour {

    [SerializeField]
    private Collider2D colliderToCheck;

    private void OnTriggerEnter2D(Collision2D collision)
    {
        print(collision.contacts[0].point);
    }

    /*
    public Vector2 GetCollisionDirection(Collider2D _collider)
    {
        Vector2 direction = Vector2.zero;


        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = colliderToCheck.bounds.center;

        if (contactPoint.y > center.y && //checks that circle is on top of rectangle
            (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
        {
            collideFromTop = true;
        }
        else if (contactPoint.y < center.y &&
            (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
        {
            collideFromBottom = true;
        }
        else if (contactPoint.x > center.x &&
            (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2))
        {
            collideFromRight = true;
        }
        else if (contactPoint.x < center.x &&
            (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2))
        {
            collideFromLeft = true;
        }

        return direction;
    }*/
}
