using UnityEngine;
using System;
using System.Collections;

public class MobileInputs : InputsBase {

    [SerializeField]
    private GameObject joyStickPrefab;

    private GameObject joyStickGObj;

    private DragDirIndicator dragDirIndicator;

    [SerializeField]
    private float minDistToDrag = 0.75f;

    private Vector2 startTouchPosition;

    private float horScreenCenter;

    private void Start()
    {
        horScreenCenter = Screen.width / 2;
    }

    public override void SetInputs(bool _input)
    {
        base.SetInputs(_input);

        if(_input)
        {
            joyStickGObj = Instantiate(joyStickPrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
            dragDirIndicator = joyStickGObj.GetComponent<DragDirIndicator>();
            joyStickGObj.SetActive(false);

            inputUpdate = StartCoroutine(InputUpdate());
        }
        else {
            if (joyStickGObj != null)
            {
                joyStickGObj.SetActive(false);
            }

            if (inputUpdate != null)
            {
                StopCoroutine(inputUpdate);
            }
        }
    }

    IEnumerator InputUpdate()
    {
        while (true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
            {
                touchState = TouchStates.Tapped;

                startTouchPosition = Input.GetTouch(0).position;
                startDownTime = Time.time;
            }

            if (touchState != TouchStates.None)
            {
                //if we haven't released yet
                if (Input.GetTouch(0).phase != TouchPhase.Ended)
                {
                    if(Time.time - startDownTime > timebeforeTappedExpired)
                    {
                        if (touchState == TouchStates.Tapped)
                        {
                            if (tappedExpired != null)
                            {
                                tappedExpired();
                            }
                        }

                        //check the distane between the touch position and the start position to check if we are dragging 
                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.ScreenToWorldPoint(startTouchPosition)) > minDistToDrag)
                        {
                            joyStickGObj.SetActive(true);
                            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(startTouchPosition);

                            //z = 0
                            joyStickGObj.transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, -4);

                            Vector2 dir = (Input.GetTouch(0).position - startTouchPosition).normalized;
                            dragDirIndicator.SetDragDir(dir);

                            touchState = TouchStates.Dragging;

                            if (dragging != null)
                                dragging(dir);
                        }
                        //if the distance is too small, and we are touched the screen for a certain amount of time, we are holding
                        else if (Time.time - startDownTime > timebeforeTappedExpired && touchState != TouchStates.Holding)
                        {
                            if (touchState == TouchStates.Dragging)
                            {
                                if (cancelDrag != null)
                                {
                                    cancelDrag();
                                }
                            }

                            joyStickGObj.SetActive(false);

                            touchState = TouchStates.Holding;

                            if (holding != null)
                            {
                                holding();
                            }
                        }
                    }
                } 
                //if we released
                else
                {
                    if (release != null)
                        release();

                    //if we are dragging, use the normalized value of the start and end pos
                    if (touchState == TouchStates.Dragging)
                    {
                        joyStickGObj.SetActive(false);

                        if (releaseInDir != null)
                            releaseInDir((Input.GetTouch(0).position - startTouchPosition).normalized);
                    }
                    //if we aren't dragging, check if we tapped or are holding
                    else 
                    {
                        if (touchState == TouchStates.Tapped)
                        {
                            //check if we tap left or right of the screen.
                            if (Input.GetTouch(0).position.x >= horScreenCenter)
                            {
                                if (action != null)
                                    action();
                            }
                            else
                            {
                                if (reverse != null)
                                    reverse();
                            }
                        }
                    }

                    touchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }
}
