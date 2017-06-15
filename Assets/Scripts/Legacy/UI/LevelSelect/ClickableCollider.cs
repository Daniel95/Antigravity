using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickableCollider : MonoBehaviour {

    public Action OnClicked;
 
    private bool touching = false;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
             touching = true;
        }

        if (Input.GetMouseButtonUp(0) && touching) {
            if(OnClicked != null) {
                OnClicked();
            }
        }
    }

    private void OnMouseExit() {
        touching = false;
    }
}
