using UnityEngine;
using System;
using System.Collections;

public class MobileInputs : InputsBase {

    [SerializeField]
    private GameObject joyStickPrefab;

    private GameObject joyStickGObj;

    private DragDirIndicator dragDirIndicator;

    [SerializeField]
    private float minDistToDrag = 1;

    private bool touched;
    private bool isDragging;

    private Vector2 startTouchPosition;

    public void StartUpdatingStandardInputs()
    {
        updateStandardInputs = StartCoroutine(UpdateStandardInputs());
    }

    public void StopUpdatingStandardInputs()
    {
        if (updateStandardInputs != null)
        {
            StopCoroutine(updateStandardInputs);
        }
    }

    public void StartUpdatingJoyStickInputs()
    {
        joyStickGObj = Instantiate(joyStickPrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        dragDirIndicator = joyStickGObj.GetComponent<DragDirIndicator>();
        joyStickGObj.SetActive(false);

        updateJoyStickInputs = StartCoroutine(UpdateJoyStickInputs());
    }

    public void StopUpdatingJoyStickInputs()
    {
        if (joyStickGObj != null)
        {
            joyStickGObj.SetActive(false);
        }

        if (updateJoyStickInputs != null)
        {
            StopCoroutine(updateJoyStickInputs);
        }
    }

    IEnumerator UpdateJoyStickInputs()
    {
        while (true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
            {
                touched = true;

                startTouchPosition = Input.GetTouch(0).position;
            }

            if (touched)
            {
                //if we released
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    touched = false;

                    //if we release on an UI element, cancel it
                    if (InputDetect.CheckUICollision(Input.GetTouch(0).position))
                    {
                        if (cancelDrag != null)
                            cancelDrag();
                    }
                    else if (release != null)
                    {
                        //if we are dragging, use the normalized value of the start and end pos
                        if (isDragging)
                        {
                            isDragging = false;
                            joyStickGObj.SetActive(false);
                            release((Input.GetTouch(0).position - startTouchPosition).normalized);
                        }
                        else //else use the normalized value of the player and the endpos
                        {
                            release(((Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)transform.position).normalized);
                        }
                    }
                }
                else
                { //if we are still holding
                    if (!isDragging)
                    {
                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.ScreenToWorldPoint(startTouchPosition)) > minDistToDrag)
                        {
                            isDragging = true;
                        }
                    }
                    else
                    {
                        joyStickGObj.SetActive(true);
                        Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(startTouchPosition);

                        //z = 0
                        joyStickGObj.transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, -4);

                        Vector2 dir = (Input.GetTouch(0).position - startTouchPosition).normalized;
                        dragDirIndicator.SetDragDir(dir);


                        if (dragging != null)
                            dragging(dir);
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator UpdateStandardInputs()
    {
        while (true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
            {
                touched = true;
            }

            if (touched) {
                if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    touched = false;

                    //check if we are not releasing on a UI element.
                    if (InputDetect.CheckUICollision(Input.GetTouch(0).position))
                    {
                        if (release != null)
                            release(((Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)transform.position).normalized);
                    }
                    else
                    {
                        if (release != null)
                            release(((Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)transform.position).normalized);
                    }
                }
                else
                {
                    if (dragging != null)
                        dragging(((Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)transform.position).normalized);
                }
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
