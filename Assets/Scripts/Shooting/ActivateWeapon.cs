using UnityEngine;
using UnityEngine.EventSystems;
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

    private List<IWeapon> _weapons = new List<IWeapon>();

    private int _weaponIndex;

    private FutureDirectionIndicator _futureDirIndicator;

    private void Awake()
    {
        _gunLookAt = gun.GetComponent<LookAt>();
        _futureDirIndicator = GetComponent<FutureDirectionIndicator>();

        foreach (var weapon in GetComponents<IWeapon>())
        {
            _weapons.Add(weapon);
        }
    }

    public void Aiming(Vector2 dir)
    {
        if (!TimeManagement.isPauzed())
        {
            _weapons[_weaponIndex].Dragging(GetDestinationPoint(dir), spawnTransform.position);
            _gunLookAt.UpdateLookAt((Vector2)transform.position + dir);
        }
    }

    public void CancelAiming()
    {
        if (!TimeManagement.isPauzed())
        {
            _weapons[_weaponIndex].CancelDragging();
            _futureDirIndicator.PointToCeilVelocityDir();
        }
    }

    public void Shoot(Vector2 dir)
    {
        if (!TimeManagement.isPauzed())
        {
            _weapons[_weaponIndex].Release(GetDestinationPoint(dir), spawnTransform.position);
            _gunLookAt.UpdateLookAt((Vector2)transform.position + dir);
        }
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

    void ChangeWeapon(int _change)
    {
        _weaponIndex += _change;

        if (_weaponIndex < 0)
            _weaponIndex = _weapons.Count - 1;

        else if (_weaponIndex > _weapons.Count - 1)
            _weaponIndex = 0;
    }
}
