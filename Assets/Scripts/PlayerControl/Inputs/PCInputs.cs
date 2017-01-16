using UnityEngine;
using System;
using System.Collections;

public class PCInputs : InputsBase {

    [SerializeField]
    private KeyCode jumpInput = KeyCode.Space;

    [SerializeField]
    private KeyCode reverseSpeedInput = KeyCode.C;

    [SerializeField]
    private KeyCode aimInput = KeyCode.Mouse0;

    [SerializeField]
    private float minDistFromPlayer = 6;

    public override void SetInputs(bool input)
    {
        base.SetInputs(input);

        if (input)
        {
            inputUpdate = StartCoroutine(InputUpdate());
        }
        else if (inputUpdate != null)
        {
            StopCoroutine(inputUpdate);
        }
    }

    private IEnumerator InputUpdate()
    {
        while (true)
        {
            //key inputs
            if (Input.GetKeyDown(jumpInput))
            {
                if (Jump != null)
                {
                    Jump();
                }
            }
            else if (Input.GetKeyDown(reverseSpeedInput))
            {
                if (Reverse != null)
                {
                    Reverse();
                }
            }

            //mouse inputs
            if (Input.GetKeyDown(aimInput) && !InputDetect.CheckUICollision(Input.mousePosition))
            {
                TouchState = TouchStates.Tapped;

                StartDownTime = Time.time;
            }

            if (TouchState != TouchStates.None)
            {
                //not yet released
                if (!Input.GetKeyUp(aimInput))
                {
                    if (Time.time - StartDownTime > TimebeforeTappedExpired)
                    {
                        if(TouchState == TouchStates.Tapped && TappedExpired != null)
                        {
                            TappedExpired();
                        }

                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > minDistFromPlayer)
                        {

                            TouchState = TouchStates.Dragging;

                            if (Dragging != null)
                                Dragging(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                        }
                        else if (TouchState != TouchStates.Holding)
                        {

                            if (TouchState == TouchStates.Dragging)
                            {
                                if (CancelDrag != null)
                                {
                                    CancelDrag();
                                }
                            }

                            TouchState = TouchStates.Holding;

                            if (Holding != null)
                            {
                                Holding();
                            }
                        }
                    }
                }
                else //released
                {
                    if (Release != null)
                        Release();

                    if (TouchState != TouchStates.Holding)
                    {
                        if (Release != null)
                            ReleaseInDir(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                    }

                    TouchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }
}