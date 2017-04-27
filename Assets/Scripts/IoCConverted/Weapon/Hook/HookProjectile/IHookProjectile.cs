using UnityEngine;

public interface IHookProjectile {

    int HookedLayerIndex { get; set; }
    Transform AttachedTransform { get; set; }

    void GoToDestination(Vector2 destination);
    void CheckIfHooked();
}
