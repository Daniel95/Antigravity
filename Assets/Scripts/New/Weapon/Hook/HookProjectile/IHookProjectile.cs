using UnityEngine;

public interface IHookProjectile {

    int HookedLayerIndex { get; set; }
    Transform AttachedTransform { get; set; }
    Transform Transform { get; }
    int AnchorsIndex { get; set; }
    bool IsMovingTowardsOwner { get; set; }

    void ActivateHookProjectile(Vector2 startPosition);
    void DeactivateHookProjectile();
    void SetParent(Transform parent);
    void DestroyProjectile();
}