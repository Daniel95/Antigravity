using UnityEngine;

public class AimWeaponData {

    public Vector2 destination;
    public Vector2 spawnPosition;

    public AimWeaponData(Vector2 destination, Vector2 spawnPosition) {
        this.destination = destination;
        this.spawnPosition = spawnPosition;
    }
}
