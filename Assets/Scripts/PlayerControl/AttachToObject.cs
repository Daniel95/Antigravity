using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToObject : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D coll) {
        transform.parent = coll.transform;
    }

    private void OnCollisionExit2D(Collision2D coll) {
        transform.parent = null;
    }
}
