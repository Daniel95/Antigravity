using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionDirection : MonoBehaviour {

    //saves the collider and the rounded direction
    private Dictionary<Collider2D, Vector2> _savedCollisions = new Dictionary<Collider2D, Vector2>();

    private CharRaycasting _charRaycasting;

    private void Awake()
    {
        _charRaycasting = GetComponent<CharRaycasting>();
    }

    //get the rounded direction of the collisions
    //each collision will be repesented by its highest axis (x or y)
    public Vector2 GetUpdatedCollDir(Collision2D collision)
    {
        //save the new collision we recieveds
        SaveNewCollision(collision);

        return GetCurrentCollDir();
    }

    //get the current rounded direction of each
    public Vector2 GetCurrentCollDir()
    {
        Vector2 combinedRoundedCollDir = new Vector2();

        //check each saved collision for a direction
        foreach (KeyValuePair<Collider2D, Vector2> collision in _savedCollisions)
        {
            combinedRoundedCollDir += collision.Value;
        }

        //make sure the combinedRoundedCollDir axises are never above 1 or below -1
        combinedRoundedCollDir = new Vector2(Mathf.Clamp(combinedRoundedCollDir.x, -1, 1), Mathf.Clamp(combinedRoundedCollDir.y, -1, 1));

        //when collision overshoots and we no longer collide with a object, even if it looks if we do,
        //use raycasting instead as a backup plan
        if(combinedRoundedCollDir == Vector2.zero)
        {
            combinedRoundedCollDir = new Vector2(_charRaycasting.CheckHorizontalCornersDir(), _charRaycasting.CheckVerticalCornersDir());
        }

        return combinedRoundedCollDir;
    }

    //save a new collision in the savedCollisions dictionary
    //the collider and the collisionDirection is saved
    private void SaveNewCollision(Collision2D collision)
    {
        Vector2 roundedCollDir = new Vector2();

        Vector2 contactPoint = collision.contacts[0].point;

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

        try
        {
            _savedCollisions.Add(collision.collider, roundedCollDir);
        }
        catch
        {
            print("Saved collisions:");
            foreach (var keyValuePair in _savedCollisions)
            {
                print("name: " + keyValuePair.Key.name);
                print("dir: " + keyValuePair.Value);
            }
            print("new collision: " + collision.collider.name + ", dir: " + roundedCollDir);

            EditorApplication.isPaused = true;
        }

    }

    //remove a saved collision from savedCollisions once we exit
    private void OnCollisionExit2D(Collision2D collision)
    {
        _savedCollisions.Remove(collision.collider);
    }
}
