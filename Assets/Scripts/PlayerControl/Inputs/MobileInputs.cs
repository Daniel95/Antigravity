using UnityEngine;
using System;
using System.Collections;

public class MobileInputs : InputsBase {

    [SerializeField]
    private GameObject joyStickPrefab;

    private GameObject _joyStickGObj;

    private DragDirIndicator _dragDirIndicator;

    [SerializeField]
    private float minDistToDrag = 0.75f;

    private Vector2 _startTouchPosition;

    private float _horScreenCenter;

    private void Start()
    {
        _horScreenCenter = Screen.width / 2;
    }

    public override void SetInputs(bool input)
    {
        base.SetInputs(input);

        if(input)
        {
            _joyStickGObj = Instantiate(joyStickPrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
            _dragDirIndicator = _joyStickGObj.GetComponent<DragDirIndicator>();
            _joyStickGObj.SetActive(false);

            inputUpdate = StartCoroutine(InputUpdate());
        }
        else {
            if (_joyStickGObj != null)
            {
                _joyStickGObj.SetActive(false);
            }

            if (inputUpdate != null)
            {
                StopCoroutine(inputUpdate);
            }
        }
    }

    private IEnumerator InputUpdate()
    {
        while (true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
            {
                TouchState = TouchStates.Tapped;

                _startTouchPosition = Input.GetTouch(0).position;
                StartDownTime = Time.time;
            }

            if (TouchState != TouchStates.None)
            {
                //if we haven't released yet
                if (Input.GetTouch(0).phase != TouchPhase.Ended)
                {
                    if(Time.time - StartDownTime > TimebeforeTappedExpired)
                    {
                        if (TouchState == TouchStates.Tapped)
                        {
                            if (TappedExpired != null)
                            {
                                TappedExpired();
                            }
                        }

                        //check the distane between the touch position and the start position to check if we are dragging 
                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.ScreenToWorldPoint(_startTouchPosition)) > minDistToDrag)
                        {
                            _joyStickGObj.SetActive(true);
                            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(_startTouchPosition);

                            //z = 0
                            _joyStickGObj.transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, -4);

                            Vector2 dir = (Input.GetTouch(0).position - _startTouchPosition).normalized;
                            _dragDirIndicator.SetDragDir(dir);

                            TouchState = TouchStates.Dragging;

                            if (Dragging != null)
                                Dragging(dir);
                        }
                        //if the distance is too small, and we are touched the screen for a certain amount of time, we are holding
                        else if (Time.time - StartDownTime > TimebeforeTappedExpired && TouchState != TouchStates.Holding)
                        {
                            if (TouchState == TouchStates.Dragging)
                            {
                                if (CancelDrag != null)
                                {
                                    CancelDrag();
                                }
                            }

                            _joyStickGObj.SetActive(false);

                            TouchState = TouchStates.Holding;

                            if (Holding != null)
                            {
                                Holding();
                            }
                        }
                    }
                } 
                //if we released
                else
                {
                    if (Release != null)
                        Release();

                    //if we are dragging, use the normalized value of the start and end pos
                    if (TouchState == TouchStates.Dragging)
                    {
                        _joyStickGObj.SetActive(false);

                        if (ReleaseInDir != null)
                            ReleaseInDir((Input.GetTouch(0).position - _startTouchPosition).normalized);
                    }
                    //if we aren't dragging, check if we tapped or are holding
                    else 
                    {
                        if (TouchState == TouchStates.Tapped)
                        {
                            //check if we tap left or right of the screen.
                            if (Input.GetTouch(0).position.x >= _horScreenCenter)
                            {
                                if (Jump != null)
                                    Jump();
                            }
                            else
                            {
                                if (Reverse != null)
                                    Reverse();
                            }
                        }
                    }

                    TouchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }
}
