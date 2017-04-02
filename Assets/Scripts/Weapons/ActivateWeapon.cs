using System;
using UnityEngine;
using System.Collections.Generic;

public class ActivateWeapon : MonoBehaviour {

    [SerializeField]
    private LayerMask rayLayers;
    
    [SerializeField]
    private const int maxRaycastDistance = 40;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Transform spawnTransform;

    private LookAt _gunLookAt;

    //A seperate list of weapons for each type.
    private List<List<IWeaponOutput>> _allWeaponTypesWeapons = new List<List<IWeaponOutput>>();

    private int _weaponIndex;

    private FutureDirectionIndicator _futureDirIndicator;

    void Start()
    {
        _gunLookAt = gun.GetComponent<LookAt>();
        _futureDirIndicator = GetComponent<FutureDirectionIndicator>();

        //save a list<IWeapon> for each type of weapon
        Dictionary<Type, List<IWeaponOutput>> weaponCollectionsOfTypes = new Dictionary<Type, List<IWeaponOutput>>();

        foreach (IWeaponOutput weapon in GetComponents<IWeaponOutput>())
        {
            if (!weaponCollectionsOfTypes.ContainsKey(weapon.GetType().BaseType))
            {
                weaponCollectionsOfTypes.Add(weapon.GetType().BaseType, new List<IWeaponOutput>());
            }

            weaponCollectionsOfTypes[weapon.GetType().BaseType].Add(weapon);
        }

        foreach (KeyValuePair<Type, List<IWeaponOutput>> keyValuePair in weaponCollectionsOfTypes)
        {
            _allWeaponTypesWeapons.Add(keyValuePair.Value);
        }
    }

    public void Aiming(Vector2 dir)
    {
        if (TimeManagement.isPauzed()) return;

        foreach (IWeaponOutput weapon in _allWeaponTypesWeapons[_weaponIndex])
        {
            weapon.Aiming(GetDestinationPoint(dir), spawnTransform.position);
        }

        _gunLookAt.UpdateLookAt((Vector2)transform.position + dir);
    }

    public void CancelAiming()
    {
        if (TimeManagement.isPauzed()) return;

        foreach (IWeaponOutput weapon in _allWeaponTypesWeapons[_weaponIndex])
        {
            weapon.CancelAiming();
        }

        _futureDirIndicator.PointToCeilVelocityDir();
    }

    public void Shoot(Vector2 dir)
    {
        if (TimeManagement.isPauzed()) return;

        foreach (IWeaponOutput weapon in _allWeaponTypesWeapons[_weaponIndex])
        {
            weapon.Fire(GetDestinationPoint(dir), spawnTransform.position);
        }

        _gunLookAt.UpdateLookAt((Vector2)transform.position + dir);
    }

   
    /// <summary>
    /// Get the destination of the projectile,  
    /// if it hits a wall then the point of collision with the wall will be its destination, 
    /// otherwise the destination will be decided by the maxDistance.
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private Vector2 GetDestinationPoint(Vector2 dir) {

        Vector2 targetPos = (Vector2)transform.position + (dir * maxRaycastDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxRaycastDistance, rayLayers);

        //if we hit a collider between the shooter and targetPos, than that collision point is the targetPos
        if (hit.collider != null)
        {
            targetPos = hit.point;
        }

        return targetPos;
    }

    void ChangeWeapon(int change)
    {
        _weaponIndex += change;

        if (_weaponIndex < 0)
            _weaponIndex = _allWeaponTypesWeapons.Count - 1;

        else if (_weaponIndex > _allWeaponTypesWeapons.Count - 1)
            _weaponIndex = 0;
    }
}
