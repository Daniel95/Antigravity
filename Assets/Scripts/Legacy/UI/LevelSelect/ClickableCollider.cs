using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickableCollider : MonoBehaviour {

    public Action OnClicked;
 
    private bool downOnCollider = false;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
             downOnCollider = true;
        }

        if (Input.GetMouseButtonUp(0) && downOnCollider) {
            if(OnClicked != null) {
                OnClicked();
            }
        }
    }

    private void OnMouseExit() {
        downOnCollider = false;
    }
}
