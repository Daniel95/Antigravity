using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(LookAt))]
public class CharacterDirectionPointerView : View, ICharacterDirectionPointer {

    private LookAt lookAt;

    public void PointToDirection(Vector2 lookdirection) {
        lookAt.UpdateLookAt((Vector2)transform.position + lookdirection);
    }

    private void Awake() {
        lookAt = GetComponent<LookAt>();
    }
}
