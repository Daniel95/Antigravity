using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ActivateWeapon : MonoBehaviour, IEventSystemHandler {

    [SerializeField]
    private LayerMask rayLayers;
    
    [SerializeField]
    private int maxRaycastDistance = 40;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Transform spawnTransform;

    private LookAt gunLookAt;

    private PlayerInputs playerInputs;

    private List<IWeapon> weapons = new List<IWeapon>();

    private int weaponIndex;

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();

        gunLookAt = gun.GetComponent<LookAt>();

        foreach (IWeapon weapon in GetComponents<IWeapon>())
        {
            weapons.Add(weapon);
        }
    }

    //assign input functions to the input delegates of playerInput
    void OnEnable()
    {
        GetComponent<Frames>().ExecuteAfterDelay(1, Subscribe);
    }

    void Subscribe()
    {
        playerInputs.InputController.dragging += Dragging;
        playerInputs.InputController.cancelDrag += CancelDrag;
        playerInputs.InputController.release += Release;
    }

    void OnDisable() {
        playerInputs.InputController.dragging -= Dragging;
        playerInputs.InputController.cancelDrag -= CancelDrag;
        playerInputs.InputController.release -= Release;
    }

    private void Dragging(Vector2 _dir)
    {
        weapons[weaponIndex].Dragging(GetHitPoint(_dir), spawnTransform.position);
        gunLookAt.UpdateLookAt((Vector2)transform.position + _dir);
    }

    private void CancelDrag()
    {
        weapons[weaponIndex].Cancel();
    }

    private void Release(Vector2 _dir)
    {
        weapons[weaponIndex].Release(GetHitPoint(_dir), spawnTransform.position);
        gunLookAt.UpdateLookAt((Vector2)transform.position + _dir);
    }

    
    private Vector2 GetHitPoint(Vector2 _dir) {

        Vector2 targetPos = (Vector2)transform.position + (_dir * maxRaycastDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir, maxRaycastDistance, rayLayers);

        //if we hit a collider between the shooter and targetPos, than that collision point is the targetPos
        if (hit.collider != null)
        {
            targetPos = hit.point;
        }

        return targetPos;
    }

    void ChangeWeapon(int _change)
    {
        weaponIndex += _change;

        if (weaponIndex < 0)
            weaponIndex = weapons.Count - 1;

        else if (weaponIndex > weapons.Count - 1)
            weaponIndex = 0;
    }
}
