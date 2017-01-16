using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    public void UpdateLookAt(Vector2 lookAtPos)
    {
        transform.right = lookAtPos - (Vector2)transform.position;
    }
}
