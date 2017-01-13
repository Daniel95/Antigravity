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

    private Coroutine updatingKeyInputs;

    public override void SetInputs(bool _input)
    {
        base.SetInputs(_input);

        if (_input)
        {
            inputUpdate = StartCoroutine(InputUpdate());
        }
        else if (inputUpdate != null)
        {
            StopCoroutine(inputUpdate);
        }
    }

    IEnumerator InputUpdate()
    {
        while (true)
        {
            //key inputs
            if (Input.GetKeyDown(jumpInput))
            {
                if (action != null)
                {
                    action();
                }
            }
            else if (Input.GetKeyDown(reverseSpeedInput))
            {
                if (reverse != null)
                {
                    reverse();
                }
            }

            //mouse inputs
            if (Input.GetKeyDown(aimInput) && !InputDetect.CheckUICollision(Input.mousePosition))
            {
                touchState = TouchStates.Tapped;

                startDownTime = Time.time;
            }

            if (touchState != TouchStates.None)
            {
                //not yet released
                if (!Input.GetKeyUp(aimInput))
                {
                    if (Time.time - startDownTime > timebeforeTappedExpired)
                    {
                        if(touchState == TouchStates.Tapped && tappedExpired != null)
                        {
                            tappedExpired();
                        }

                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > minDistFromPlayer)
                        {

                            touchState = TouchStates.Dragging;

                            if (dragging != null)
                                dragging(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                        }
                        else if (touchState != TouchStates.Holding)
                        {

                            if (touchState == TouchStates.Dragging)
                            {
                                if (cancelDrag != null)
                                {
                                    cancelDrag();
                                }
                            }

                            touchState = TouchStates.Holding;

                            if (holding != null)
                            {
                                holding();
                            }
                        }
                    }
                }
                else //released
                {
                    if (release != null)
                        release();

                    if (touchState != TouchStates.Holding)
                    {
                        if (release != null)
                            releaseInDir(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                    }

                    touchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }
}