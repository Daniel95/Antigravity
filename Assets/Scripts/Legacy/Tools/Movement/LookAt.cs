using UnityEngine;

public class LookAt : MonoBehaviour {

    public void UpdateLookAt(Vector2 lookAtPosition) {
        transform.right = lookAtPosition - (Vector2)transform.position;
    }
}
