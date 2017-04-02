using UnityEngine;

public interface IWeaponOutput
{
    void Aiming(Vector2 destination, Vector2 spawnPosition);

    void Fire(Vector2 destination, Vector2 spawnPosition);

    void CancelAiming();
}
