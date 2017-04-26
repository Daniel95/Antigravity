using UnityEngine;

public interface IHookProjectile {

    int HookedLayer { get; set; }

    void GoToDestination(Vector2 destination);
    void CheckIfHooked();
}
