using IoCPlus;
using UnityEngine;

public class AimDestinationView : View, IAimDestination {

    public Vector2 SpawnPosition { get { return spawnTransform.position; } }

    [Inject] private Ref<IAimDestination> aimDestinationRef;

    [SerializeField] private LayerMask rayLayers;
    [SerializeField] private int maxDistance = 40;
    [SerializeField] private Transform spawnTransform;

    public override void Initialize() {
        aimDestinationRef.Set(this);
    }

    public Vector2 GetDestinationPoint(Vector2 direction) {
        Vector2 targetPos = (Vector2)transform.position + (direction * maxDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, rayLayers);

        if (hit.collider != null) {
            targetPos = hit.point;
        }

        return targetPos;
    }
}
