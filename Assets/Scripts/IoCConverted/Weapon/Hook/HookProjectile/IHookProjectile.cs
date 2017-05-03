using UnityEngine;

public interface IHookProjectile {

    int HookedLayerIndex { get; set; }
    Transform AttachedTransform { get; set; }
    Transform Transform { get; set; }
    int ReachedAnchorsIndex { get; set; }
    bool IsMovingTowardsOwner { get; set; }

    void SetParent(Transform paren);
}
