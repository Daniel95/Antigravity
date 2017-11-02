using System.Collections.Generic;
using UnityEngine;

public interface IHookProjectile {

    float DistanceFromOwner { get; set; }
    List<int> CollidingLayers { get; }
    List<GameObject> CollidingGameObjects { get; set; }
    Transform Transform { get; }

    void ActivateHookProjectile(Vector2 startPosition);
    void DeactivateHookProjectile();
    void SetParent(Transform parent);
    void SetPosition(Vector2 position);
    void SetColliderEnabled(bool enabled);
    void DestroyProjectile();
}