using UnityEngine;

public interface IHookProjectile {

    int CollidingTransformLayer { get; }
    Transform CollidingTransform { get; set; }
    Transform Transform { get; }

    void ActivateHookProjectile(Vector2 startPosition);
    void DeactivateHookProjectile();
    void SetParent(Transform parent);
    void DestroyProjectile();
}