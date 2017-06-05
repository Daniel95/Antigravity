using UnityEngine;

public interface IHookProjectile {

    int CollidingTransformLayer { get; }
    Transform CollidingTransform { get; set; }
    Transform Transform { get; }

    void ActivateHookProjectile(Vector2 startPosition);
    void DeactivateHookProjectile();
    void SetParent(Transform parent);
    void SetPosition(Vector2 position);
    void SetColliderEnabled(bool enabled);
    void DestroyProjectile();
}