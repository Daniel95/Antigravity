using IoCPlus;
using UnityEngine;

public class CharacterDirectionPointView : View, ICharacterDirectionPointer {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointer;

    [SerializeField] private Transform arrowTransform;

    private Vector2 lookdirection;
    private Frames frames;
    private LookAt lookAt;

    public void PointToDirection(Vector2 direction) {
        lookdirection = direction;
        lookAt.UpdateLookAt((Vector2)transform.position + lookdirection);
    }

    public void PointToCeiledVelocityDirection(Vector2 ceiledVelocityDirection) {
        if (ceiledVelocityDirection.x != 0) {
            lookdirection.x = ceiledVelocityDirection.x;
        }
        if (ceiledVelocityDirection.y != 0) {
            lookdirection.y = ceiledVelocityDirection.y;
        }

        lookAt.UpdateLookAt((Vector2)transform.position + lookdirection);
    }

    private void Awake() {
        frames = GetComponent<Frames>();
        lookAt = GetComponent<LookAt>();
    }
}
