﻿using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionDirectionView : View, ICharacterCollisionDirection {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;

    //saves the collider and the rounded direction
    private Dictionary<Collider2D, Vector2> savedCollisions = new Dictionary<Collider2D, Vector2>();

    public override void Initialize() {
        characterCollisionDirectionRef.Set(this);
    }

    //get the rounded direction of the collisions
    //each collision will be repesented by its highest axis (x or y)
    public Vector2 GetUpdatedCollisionDirection(Collision2D collision, Vector2 cornersDirection) {
        //save the new collision we recieved
        SaveNewCollision(collision);

        return GetCurrentCollisionDirection(cornersDirection);
    }

    //get the current rounded direction of each
    public Vector2 GetCurrentCollisionDirection(Vector2 cornersDirection) {
        Vector2 combinedRoundedCollDir = new Vector2();

        //check each saved collision for a direction
        foreach (KeyValuePair<Collider2D, Vector2> collision in savedCollisions) {
            combinedRoundedCollDir += collision.Value;
        }

        //make sure the combinedRoundedCollDir axises are never above 1 or below -1
        combinedRoundedCollDir = new Vector2(Mathf.Clamp(combinedRoundedCollDir.x, -1, 1), Mathf.Clamp(combinedRoundedCollDir.y, -1, 1));

        //when collision overshoots and we no longer collide with a object, even if it looks if we do,
        //use raycasting instead as a backup plan
        if(combinedRoundedCollDir == Vector2.zero) {
            combinedRoundedCollDir = cornersDirection;
        }

        return combinedRoundedCollDir;
    }

    public void RemoveCollisionDirection(Vector2 collisionDirection) {
        foreach (KeyValuePair<Collider2D, Vector2> keyValuePair in savedCollisions) {
            if (keyValuePair.Value == collisionDirection) {
                savedCollisions.Remove(keyValuePair.Key);
                break;
            }
        }
    }

    public void ResetCollisionDirection() {
        savedCollisions.Clear();
    }

    //save a new collision in the savedCollisions dictionary
    //the collider and the collisionDirection is saved
    private void SaveNewCollision(Collision2D collision) {
        Vector2 roundedCollDir = new Vector2();
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 offset = (contactPoint - (Vector2)transform.position).normalized;

        //the collision will be repesented by its highest axis (x or y)
        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y)) {
            roundedCollDir.x = Rounding.InvertOnNegativeCeil(offset.x);
        }
        else {
            roundedCollDir.y = Rounding.InvertOnNegativeCeil(offset.y);
        }

        if (!savedCollisions.ContainsKey(collision.collider)) {
            savedCollisions.Add(collision.collider, roundedCollDir);
        }
    }

    //remove a saved collision from savedCollisions once we exit
    private void OnCollisionExit2D(Collision2D collision) {
        savedCollisions.Remove(collision.collider);
    }
}
