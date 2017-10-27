using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField] private Transform other;

    private void Update() {
        Vector2 offset = other.position - transform.position;
        Vector2 direction = offset.normalized;
        /*
        float targetAngle = VectorHelper.DirectionToAngle(offset);
        float directionAngle = VectorHelper.DirectionToAngle(direction);
        Vector3 rotateAxis = directionAngle > targetAngle ? -Vector3.forward : Vector3.forward;

        Debug.Log("___________");
        Debug.Log("targetAngle " + targetAngle);
        Debug.Log("directionAngle " + directionAngle);
        Debug.Log("rotateAxis " + rotateAxis);
        */

        if (Vector3.Dot(transform.right, offset) < 0) {
            Debug.Log("Left");
        } else {
            Debug.Log("Right");
        }

        Debug.Log("DirectionIsToTheRight " + VectorHelper.DirectionIsToTheRight(transform.right, offset));
    }

}
