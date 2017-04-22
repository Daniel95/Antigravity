using IoCPlus;
using UnityEngine;

public class ShootView : View, IShoot {

    public Vector2 SpawnPosition { get { return spawnTransform.position; } }

    [Inject] private Ref<IShoot> weaponInputRef;

    [Inject] private FireWeaponEvent fireWeaponEvent;
    [Inject] private AimWeaponEvent aimWeaponEvent;
    [Inject] private CancelAimWeaponEvent cancelAimWeaponEvent;

    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private int maxRaycastDistance = 40;
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform spawnTransform;

    public override void Initialize() {
        weaponInputRef.Set(this);
    }

    public Vector2 GetDestinationPoint(Vector2 direction) {

        Vector2 targetPos = (Vector2)transform.position + (direction * maxRaycastDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRaycastDistance, rayLayers);

        if (hit.collider != null) {
            targetPos = hit.point;
        }

        return targetPos;
    }
}
