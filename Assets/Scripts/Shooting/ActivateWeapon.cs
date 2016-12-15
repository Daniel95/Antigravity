using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ActivateWeapon : MonoBehaviour, IEventSystemHandler {

    [SerializeField]
    private LayerMask test;

    [SerializeField]
    private LayerMask rayLayers;
    
    [SerializeField]
    private int maxRaycastDistance = 40;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Transform spawnTransform;

    private LookAt gunLookAt;

    private List<IWeapon> weapons = new List<IWeapon>();

    private int weaponIndex;

    void Start()
    {
        foreach (IWeapon weapon in GetComponents<IWeapon>())
        {
            weapons.Add(weapon);
        }

        gunLookAt = gun.GetComponent<LookAt>();
    }

    //assign input functions to the input delegates of playerInput
    void OnEnable()
    {
        PlayerInputs playerInputs = GetComponent<PlayerInputs>();

        playerInputs.dragging += Dragging;
        playerInputs.cancelDrag += CancelDrag;
        playerInputs.release += Release;
    }

    void OnDisable() {
        PlayerInputs playerInputs = GetComponent<PlayerInputs>();

        playerInputs.dragging -= Dragging;
        playerInputs.cancelDrag -= CancelDrag;
        playerInputs.release -= Release;
    }

    private void Dragging(Vector2 _dir)
    {
        weapons[weaponIndex].Dragging(_dir, GetHitPoint(_dir), spawnTransform.position);
        gunLookAt.UpdateLookAt((Vector2)transform.position + _dir);
    }

    private void CancelDrag()
    {
        weapons[weaponIndex].Cancel();
    }

    private void Release(Vector2 _dir)
    {
        weapons[weaponIndex].Release(_dir, GetHitPoint(_dir), spawnTransform.position);
    }

    private Vector2 GetHitPoint(Vector2 _dir) {

        Vector2 targetPos = _dir * maxRaycastDistance;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos, maxRaycastDistance, rayLayers);

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
