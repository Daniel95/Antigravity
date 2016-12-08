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
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
                holding = true;

            if (holding && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                holding = false;

                if (release != null)
                    release(Input.GetTouch(0).deltaPosition.normalized);
            }
            else if (holding)
            {
                if(dragging != null)
                    dragging(Input.GetTouch(0).deltaPosition.normalized);
            }

            yield return null;
        }
    }

    /*

    public bool Tab()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Began && Input.GetTouch(0).phase != TouchPhase.Moved && !InputDetect.CheckUICollision(Input.GetTouch(0).position);
    }

    private Vector2 Dragging()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
            holding = true;

        if (holding)
        {
            return Input.GetTouch(0).deltaPosition.normalized;
        }
        else return Vector2.zero;
    }

    private Vector2 Release()
    {
        if (holding && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            holding = false;
            return Input.GetTouch(0).deltaPosition.normalized;
        }
        else return Vector2.zero;
    }
    */
}
