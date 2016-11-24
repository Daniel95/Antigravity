using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    public void UpdateLookAt(Vector2 _lookAtPos)
    {
        transform.right = _lookAtPos - (Vector2)transform.position;
    }
}
