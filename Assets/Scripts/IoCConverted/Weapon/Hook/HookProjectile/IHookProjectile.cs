using UnityEngine;

public interface IHookProjectile {

    int HookedLayerIndex { get; set; }
    Transform AttachedTransform { get; set; }

    void SetParent(Transform paren);
    void GoToDestination(Vector2 destination);
    void CheckIfHooked();
}
