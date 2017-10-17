﻿using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCollisionDirectionView : View, ICharacterCollisionDirection {

    [Inject] private CharacterCollisionWithNewDirectionEvent characterCollisionWithNewDirectionEvent;

    public Vector2 CollisionDirection { get { return collisionDirection; } }
    public int SavedCollisionsCount { get { return collisions.Count; } }

    private Dictionary<Collision2D, Vector2> collisions = new Dictionary<Collision2D, Vector2>();
    private Vector2 collisionDirection;

    private void OnCollisionEnter2D(Collision2D collision) {
        AddCollision(collision);
        UpdateCollisionDirection(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        AddCollision(collision);
        UpdateCollisionDirection(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        collisions.Remove(collision);
        UpdateCollisionDirection(collision, false);
    }

    private void LateUpdate() {
        collisions.Clear();
    }

    private void UpdateCollisionDirection(Collision2D collision, bool possiblyDispatchCollisionEvent = true) {
        Vector2 newCollisionDirection = GetCollisionDirection();
        if (collisionDirection != newCollisionDirection) {
            collisionDirection = newCollisionDirection;
            if(possiblyDispatchCollisionEvent) {
                characterCollisionWithNewDirectionEvent.Dispatch(collision, newCollisionDirection, gameObject);
            }
        }
    }

    private Vector2 GetCollisionDirection() {
        Vector2 combinedCollisionDirection = new Vector2();

        foreach (Vector2 collisionDirection in collisions.Values.ToList()) {
            combinedCollisionDirection += collisionDirection;
        }

        combinedCollisionDirection = VectorHelper.Clamp(combinedCollisionDirection, -1, 1);
        return combinedCollisionDirection;
    }

    private void AddCollision(Collision2D collision) {
        Vector2 allDirections = new Vector2();
        foreach (ContactPoint2D contact in collision.contacts) {
            Vector2 direction = (contact.point - (Vector2)transform.position).normalized;
            Vector2 ceiledDirection = new Vector2();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                ceiledDirection.x = RoundingHelper.InvertOnNegativeCeil(direction.x);
            } else {
                ceiledDirection.y = RoundingHelper.InvertOnNegativeCeil(direction.y);
            }
            allDirections += ceiledDirection;
        }

        Vector2 combinedDirection = VectorHelper.Clamp(allDirections, -1, 1);

        collisions.Add(collision, combinedDirection);
    }

    public void ResetCollisions() {
        collisions.Clear();
    }

}
