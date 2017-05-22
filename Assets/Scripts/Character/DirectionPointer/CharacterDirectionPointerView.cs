using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(LookAt))]
public class CharacterDirectionPointerView : View, ICharacterDirectionPointer {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointer;

    private Vector2 lookdirection;
    private LookAt lookAt;

    public override void Initialize() {
        directionPointer.Set(this);
    }

    public void PointToDirection(Vector2 direction) {
        lookdirection = direction;
        lookAt.UpdateLookAt((Vector2)transform.position + lookdirection);
    }

    private void Awake() {
        lookAt = GetComponent<LookAt>();
    }
}
