using UnityEngine;

public interface IHookProjectile {

    int HookedLayerIndex { get; set; }
    Transform AttachedTransform { get; set; }
    Transform Transform { get; }
    int ReachedAnchorsIndex { get; set; }
    bool IsMovingTowardsOwner { get; set; }

    void SetParent(Transform parent);
    void DestroyProjectile();

}
