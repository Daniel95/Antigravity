using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(LookAt))]
public class CharacterDirectionPointerView : View, ICharacterDirectionPointer {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointer;

    private LookAt lookAt;

    public override void Initialize() {
        directionPointer.Set(this);
    }

    public void PointToDirection(Vector2 lookdirection) {
        lookAt.UpdateLookAt((Vector2)transform.position + lookdirection);
    }

    private void Awake() {
        lookAt = GetComponent<LookAt>();
    }
}
