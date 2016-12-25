using UnityEngine;
using System;
using System.Collections;

public class PCInputs : InputsBase {

    [SerializeField]
    private KeyCode jumpInput = KeyCode.Space;

    [SerializeField]
    private KeyCode flipSpeedInput = KeyCode.X;

    [SerializeField]
    private KeyCode aimInput = KeyCode.Mouse0;

    [SerializeField]
    private KeyCode cancelAimInput = KeyCode.Mouse1;

    private bool holding;

    [SerializeField]
    private GameObject joyStickPrefab;

    private GameObject joyStickGObj;

    private DragDirIndicator dragDirIndicator;

    [SerializeField]
    private float minDistToDrag = 1;

    private bool touched;
    private bool isDragging;

    private Vector2 startTouchPosition;

    public override void StartUpdatingStandardInputs()
    {
        updateStandardInputs = StartCoroutine(UpdateStandardInputs());
    }

    public override void StopUpdatingStandardInputs()
    {
        if(updateStandardInputs != null)
        {
            StopCoroutine(updateStandardInputs);
        }
    }

    public override void StartUpdatingJoyStickInputs()
    {
        joyStickGObj = Instantiate(joyStickPrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        dragDirIndicator = joyStickGObj.GetComponent<DragDirIndicator>();
        joyStickGObj.SetActive(false);

        updateJoyStickInputs = StartCoroutine(UpdateJoyStickInputs());
    }

    public override void StopUpdatingJoyStickInputs()
    {
        if (joyStickGObj != null) {
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
            if (Input.GetKeyDown(aimInput) && !InputDetect.CheckUICollision(Input.mousePosition))
            {
                touched = true;

                startTouchPosition = Input.mousePosition;
            }

            if (touched)
            {
                //if we released
                if (Input.GetKeyUp(aimInput))
                {
                    touched = false;

                    //if we release on an UI element, cancel it
                    if (InputDetect.CheckUICollision(Input.mousePosition))
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
                            release(((Vector2)Input.mousePosition - startTouchPosition).normalized);
                        }
                        else //else use the normalized value of the player and the endpos
                        {
                            release(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                        }
                    }
                }
                else
                { //if we are still holding
                    if (!isDragging)
                    {
                        if (Vector2.Distance((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), (Vector2)Camera.main.ScreenToWorldPoint(startTouchPosition)) > minDistToDrag)
                        {
                            isDragging = true;
                        }
                    }
                    else
                    {
                        joyStickGObj.SetActive(true);
                        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(startTouchPosition);

                        //z = 0
                        joyStickGObj.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, -4);

                        Vector2 dir = ((Vector2)Input.mousePosition - startTouchPosition).normalized;
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
            if (Input.GetKeyDown(jumpInput))
            {
                if (action != null)
                {
                    action();
                }
            }
            else if (Input.GetKeyDown(flipSpeedInput)) {
                if (flipSpeed != null)
                {
                    flipSpeed();
                }
            }

            if (Input.GetKeyDown(aimInput) && !InputDetect.CheckUICollision(Input.mousePosition))
            {
                holding = true;
            }
            if (Input.GetKeyDown(cancelAimInput))
            {
                holding = false;

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
                        release(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                }
                else //dragging
                {
                    if (dragging != null)
                        dragging(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                }
            }

            yield return null;
        }
    }

    public void Action()
    {
        if (action != null)
            action();
    }

    public void FlipSpeed()
    {
        if (flipSpeed != null)
            flipSpeed();
    }
}