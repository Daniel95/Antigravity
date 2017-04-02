using IoCPlus;
using System;
using UnityEngine;
using System.Collections.Generic;

public class WeaponInputView : View, IWeaponInput {

    [Inject] private SelectedWeaponOutputModel selectedWeaponOutputModel;

    [Inject] private Ref<IWeaponInput> weaponInputRef;

    [Inject] private FireWeaponEvent fireWeaponEvent;
    [Inject] private AimWeaponEvent aimWeaponEvent;
    [Inject] private CancelAimWeaponEvent cancelAimWeaponEvent;

    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private const int maxRaycastDistance = 40;
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform spawnTransform;

    private LookAt gunLookAt;

    private List<IWeaponOutput> weapons = new List<IWeaponOutput>();

    private int weaponIndex;

    private FutureDirectionIndicator _futureDirIndicator;

    public override void Initialize() {
        weaponInputRef.Set(this);
    }

    void Start() {
        gunLookAt = gun.GetComponent<LookAt>();
        _futureDirIndicator = GetComponent<FutureDirectionIndicator>();

        foreach (IWeaponOutput weapon in GetComponents<IWeaponOutput>()) {
            weapons.Add(weapon);
        }
    }

    public void Dragging(Vector2 direction) {

        aimWeaponEvent.Dispatch(GetDestinationPoint(direction), spawnTransform.position);
        
        gunLookAt.UpdateLookAt((Vector2)transform.position + direction);
    }

    public void CancelDragging() {

        cancelAimWeaponEvent.Dispatch();

        _futureDirIndicator.PointToCeilVelocityDir();
    }

    public void ReleaseInDirection(Vector2 direction) {
        fireWeaponEvent.Dispatch(GetDestinationPoint(direction), spawnTransform.position);

        gunLookAt.UpdateLookAt((Vector2)transform.position + direction);
    }

    private Vector2 GetDestinationPoint(Vector2 dir) {

        Vector2 targetPos = (Vector2)transform.position + (dir * maxRaycastDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxRaycastDistance, rayLayers);

        //if we hit a collider between the shooter and targetPos, than that collision point is the targetPos
        if (hit.collider != null) {
            targetPos = hit.point;
        }

        return targetPos;
    }

    void ChangeWeapon(int change) {
        weaponIndex += change;

        if (weaponIndex < 0)
            weaponIndex = weapons.Count - 1;

        else if (weaponIndex > weapons.Count - 1)
            weaponIndex = 0;

        selectedWeaponOutputModel.weaponOutput = weapons[weaponIndex];
    }
}
