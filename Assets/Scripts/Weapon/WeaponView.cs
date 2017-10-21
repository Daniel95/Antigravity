using IoCPlus;
using UnityEngine;

public class WeaponView : View, IWeapon {

    public GameObject Owner { get { return gameObject; } }
    public Vector2 ShootDirection { get { return destination; } set { destination = value; } }
    public Vector2 SpawnPosition { get { return spawnTransform.position; } }

    [Inject] private Ref<IWeapon> weaponRef;

    [SerializeField] private LayerMask aimRaycastLayerMask;
    [SerializeField] private int maxDistance = 40;
    [SerializeField] private Transform spawnTransform;

    private Vector2 destination;

    public override void Initialize() {
        weaponRef.Set(this);
    }

    public Vector2 GetShootDestinationPoint(Vector2 direction) {
        Vector2 targetPos = (Vector2)transform.position + (direction * maxDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, aimRaycastLayerMask);

        if (hit.collider != null) {
            targetPos = hit.point;
        }

        return targetPos;
    }

}
