﻿using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterCollisionDirectionView : View, ICharacterCollisionDirection {

    public Vector2 CollisionDirection { get { return collisionDirection; } }
    public int SavedCollisionsCount { get { return collisions.Count; } }

    private Dictionary<Collider2D, Vector2> collisions = new Dictionary<Collider2D, Vector2>();
    private Vector2 collisionDirection;

    private void OnCollisionEnter2D(Collision2D collision) {
        UpdateCollisions(collision);
        collisionDirection = GetCollisionDirection();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        UpdateCollisions(collision);
        collisionDirection = GetCollisionDirection();
    }

    private void OnCollisionExit2D(Collision2D collision) {
        collisions.Remove(collision.collider);
        collisionDirection = GetCollisionDirection();
    }

    private Vector2 GetCollisionDirection() {
        Vector2 combinedCollisionDirection = collisions.Values.ToList().CombineVectors();
        combinedCollisionDirection = VectorHelper.Clamp(combinedCollisionDirection, -1, 1);
        return combinedCollisionDirection;
    }

    private void UpdateCollisions(Collision2D collision) {
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

        if(collisions.ContainsKey(collision.collider)) {
            collisions[collision.collider] = combinedDirection;
        } else {
            collisions.Add(collision.collider, combinedDirection);
        }
    }

    public void ResetCollisions() {
        collisions.Clear();
    }

}
