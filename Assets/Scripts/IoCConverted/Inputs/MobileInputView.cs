using IoCPlus;
using System.Collections;
using UnityEngine;

public class MobileInputView : View, IInput {

    [Inject] private RawCancelDragInputEvent rawCancelDragInputEvent;
    [Inject] private RawDraggingInputEvent rawDraggingInputEvent;
    [Inject] private RawHoldingInputEvent rawHoldingInputEvent;
    [Inject] private RawJumpInputEvent rawJumpInputEvent;
    [Inject] private RawReleaseInDirectionInputEvent rawReleaseInDirectionInputEvent;
    [Inject] private RawReleaseInputEvent rawReleaseInputEvent;
    [Inject] private RawTappedExpiredInputEvent rawTappedExpiredInputEvent;

    [SerializeField] private GameObject joyStickPrefab;

    [SerializeField] private float minDistToDrag = 0.75f;

    [Inject] IContext context;

    private GameObject _joyStickGObj;

    private DragDirIndicator _dragDirIndicator;

    private Vector2 _startTouchPosition;

    protected Coroutine inputUpdate;

    protected enum TouchStates { Holding, Dragging, Tapped, None }

    protected TouchStates TouchState = TouchStates.None;

    [SerializeField]
    protected float TimebeforeTappedExpired = 0.15f;

    protected float StartDownTime;

    public virtual void ResetTouched() {
        rawCancelDragInputEvent.Dispatch();
    }

    public void EnableInput(bool enable) {

        if (enable) {
            _joyStickGObj = Instantiate(joyStickPrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
            _dragDirIndicator = _joyStickGObj.GetComponent<DragDirIndicator>();
            _joyStickGObj.SetActive(false);

            inputUpdate = StartCoroutine(InputUpdate());
        } else {
            if (_joyStickGObj != null) {
                _joyStickGObj.SetActive(false);
            }

            if (inputUpdate != null) {
                StopCoroutine(inputUpdate);
            }
        }
    }

    private IEnumerator InputUpdate() {
        while (true) {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !InputDetect.CheckUICollision(Input.GetTouch(0).position)) {
                TouchState = TouchStates.Tapped;

                _startTouchPosition = Input.GetTouch(0).position;
                StartDownTime = Time.time;
            }

            if (TouchState != TouchStates.None) {
                //if we haven't released yet
                if (Input.GetTouch(0).phase != TouchPhase.Ended) {
                    if (Time.time - StartDownTime > TimebeforeTappedExpired) {
                        if (TouchState == TouchStates.Tapped) {
                            rawTappedExpiredInputEvent.Dispatch();
                        }

                        //check the distane between the touch position and the start position to check if we are dragging 
                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.ScreenToWorldPoint(_startTouchPosition)) > minDistToDrag) {
                            _joyStickGObj.SetActive(true);
                            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(_startTouchPosition);

                            //z = 0
                            _joyStickGObj.transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, -4);

                            Vector2 dir = (Input.GetTouch(0).position - _startTouchPosition).normalized;
                            _dragDirIndicator.SetDragDir(dir);

                            TouchState = TouchStates.Dragging;

                            rawDraggingInputEvent.Dispatch(dir);
                        }
                        //if the distance is too small, and we are touched the screen for a certain amount of time, we are holding
                        else if (Time.time - StartDownTime > TimebeforeTappedExpired && TouchState != TouchStates.Holding) {
                            if (TouchState == TouchStates.Dragging) {
                                rawCancelDragInputEvent.Dispatch();
                            }

                            _joyStickGObj.SetActive(false);

                            TouchState = TouchStates.Holding;

                            rawHoldingInputEvent.Dispatch();
                        }
                    }
                }
                //if we released
                else {
                    rawReleaseInputEvent.Dispatch();

                    //if we are dragging, use the normalized value of the start and end pos
                    if (TouchState == TouchStates.Dragging) {
                        _joyStickGObj.SetActive(false);

                        rawReleaseInDirectionInputEvent.Dispatch((Input.GetTouch(0).position - _startTouchPosition).normalized);
                    }
                    //if we aren't dragging, check if we tapped or are holding
                    else {
                        if (TouchState == TouchStates.Tapped) {
                            rawJumpInputEvent.Dispatch();
                        }
                    }

                    TouchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }
}