using UnityEngine;
using System;
using System.Collections;

public class PCInputs : InputsBase {

    [SerializeField]
    private KeyCode jumpInput = KeyCode.Space;

    [SerializeField]
    private KeyCode aimInput = KeyCode.Mouse0;

    [SerializeField]
    private KeyCode cancelAimInput = KeyCode.Mouse2;

    private bool holding;

    public void StartUpdatingInputs()
    {
        StartCoroutine(UpdateInputs());
    }

    IEnumerator UpdateInputs()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (space != null)
                {
                    space();
                }
            }

            if (Input.GetKeyDown(aimInput) && !InputDetect.CheckUICollision(Input.mousePosition))
            {
                holding = true;
            }
            else if (Input.GetKeyDown(cancelAimInput))
            {
                if (cancelDrag != null)
                    cancelDrag();
            }

            if (holding)
            {
                //release
                if (Input.GetKeyUp(aimInput))
                {
                    holding = false;

                    if (release != null)
                        release((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
                }
                else //dragging
                {
                    if (dragging != null)
                        dragging((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
                }
            }

            yield return null;
        }
    }

    private bool Jump()
    {
        return Input.GetKeyDown(jumpInput);
    }
}
