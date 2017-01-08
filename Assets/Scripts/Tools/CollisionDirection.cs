using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDirection : MonoBehaviour {

    //saves the collider and the rounded direction
    private Dictionary<Collider2D, Vector2> savedCollisions = new Dictionary<Collider2D, Vector2>();

    private CharRaycasting charRaycasting;

    private void Awake()
    {
        charRaycasting = GetComponent<CharRaycasting>();
    }

    //get the rounded direction of the collisions
    //each collision will be repesented by its highest axis (x or y)
    public Vector2 GetUpdatedCollDir(Collision2D _collision)
    {
        //save the new collision we recieveds
        SaveNewCollision(_collision);

        return GetCurrentCollDir();
    }

    //get the current rounded direction of each
    public Vector2 GetCurrentCollDir()
    {
        Vector2 combinedRoundedCollDir = new Vector2();

        //check each saved collision for a direction
        foreach (KeyValuePair<Collider2D, Vector2> collision in savedCollisions)
        {
            combinedRoundedCollDir += collision.Value;
        }

        //when collision overshoots and we no longer collide with a object, even if it looks if we do,
        //use raycasting instead as a backup plan
        if(combinedRoundedCollDir == Vector2.zero)
        {
            combinedRoundedCollDir = new Vector2(charRaycasting.CheckHorizontalDir(), charRaycasting.CheckVerticalDir());
        }

        return combinedRoundedCollDir;
    }

    //save a new collision in the savedCollisions dictionary
    //the collider and the collisionDirection is saved
    private void SaveNewCollision(Collision2D _collision)
    {
        Vector2 roundedCollDir = new Vector2();

        Vector2 contactPoint = _collision.contacts[0].point;

        Vector2 offset = (contactPoint - (Vector2)transform.position).normalized;

        //the collision will be repesented by its highest axis (x or y)
        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            roundedCollDir.x = Rounding.InvertOnNegativeCeil(offset.x);
        }
        else
        {
            roundedCollDir.y = Rounding.InvertOnNegativeCeil(offset.y);
        }

        savedCollisions.Add(_collision.collider, roundedCollDir);
    }

    //remove a saved collision from savedCollisions once we exit
    private void OnCollisionExit2D(Collision2D collision)
    {
        savedCollisions.Remove(collision.collider);
    }
}
