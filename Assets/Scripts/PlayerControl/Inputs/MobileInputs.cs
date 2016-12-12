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

                if (release != null)
                    release((Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position).normalized);
            }
            else if (holding)
            {
                if(dragging != null)
                    dragging((Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position).normalized);
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
