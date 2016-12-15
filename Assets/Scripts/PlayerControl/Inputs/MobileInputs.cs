using UnityEngine;
using System;
using System.Collections;

public class MobileInputs : InputsBase {

    private bool holding;

    public void StartUpdatingInputs() {
        StartCoroutine(UpdateInputs());
    }

    IEnumerator UpdateInputs()
    {
        while (true)
        {
            if (Input.touchCount > 0 && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
                holding = true;

            if (holding && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                holding = false;

                //check if we are not releasing on a UI element.
                if (InputDetect.CheckUICollision(Input.GetTouch(0).position))
                {
                    if (release != null)
                        release((Input.GetTouch(0).deltaPosition - (Vector2)transform.position).normalized);
                }
                else {
                    if (cancelDrag != null)
                        cancelDrag();
                }
            }
            else if (holding)
            {
                if(dragging != null)
                    dragging((Input.GetTouch(0).deltaPosition - (Vector2)transform.position).normalized);
            }

            yield return null;
        }
    }

    public void Action() {
        if (action != null)
            action();
    }

    public void FlipSpeed()
    {
        if (flipSpeed != null)
            flipSpeed();
    }
}
