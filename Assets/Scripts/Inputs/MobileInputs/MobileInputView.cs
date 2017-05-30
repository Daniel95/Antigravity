using IoCPlus;
using System.Collections;
using UnityEngine;

public class MobileInputView : View, IMobileInput {

    [SerializeField] private GameObject joyStickPrefab;
    [SerializeField] private float TimebeforeTappedExpired = 0.15f;
    [SerializeField] private float minimalDistanceToDrag = 0.75f;

    [Inject] private Ref<IMobileInput> mobileInputRef;

    [Inject] private RawCancelDragInputEvent rawCancelDragInputEvent;
    [Inject] private RawDraggingInputEvent rawDraggingInputEvent;
    [Inject] private RawHoldingInputEvent rawHoldingInputEvent;
    [Inject] private RawJumpInputEvent rawJumpInputEvent;
    [Inject] private RawReleaseInDirectionInputEvent rawReleaseInDirectionInputEvent;
    [Inject] private RawReleaseInputEvent rawReleaseInputEvent;
    [Inject] private RawTappedExpiredInputEvent rawTappedExpiredInputEvent;

    private Coroutine inputUpdateCoroutine;
    private enum TouchStates { Holding, Dragging, Tapped, None }
    private TouchStates TouchState = TouchStates.None;
    private float StartDownTime;
    private GameObject joyStick;
    private DragDirIndicator dragDirIndicator;
    private Vector2 startTouchPosition;

    public override void Initialize() {
        mobileInputRef.Set(this);
    }

    public virtual void ResetTouched() {
        rawCancelDragInputEvent.Dispatch();
    }

    public void EnableInput(bool enable) {
        if (enable) {
            joyStick = Instantiate(joyStickPrefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
            dragDirIndicator = joyStick.GetComponent<DragDirIndicator>();
            joyStick.SetActive(false);
            inputUpdateCoroutine = StartCoroutine(InputUpdate());

        } else {
            if (joyStick != null) {
                joyStick.SetActive(false);
            }

            if (inputUpdateCoroutine != null) {
                StopCoroutine(inputUpdateCoroutine);
                inputUpdateCoroutine = null;
            }
        }
    }

    private IEnumerator InputUpdate() {
        while (true) {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !InputDetect.CheckUICollision(Input.GetTouch(0).position)) {
                TouchState = TouchStates.Tapped;
                startTouchPosition = Input.GetTouch(0).position;
                StartDownTime = Time.time;
            }

            if (TouchState != TouchStates.None) {
                if (Input.GetTouch(0).phase != TouchPhase.Ended) {
                    if (Time.time - StartDownTime > TimebeforeTappedExpired) {
                        if (TouchState == TouchStates.Tapped) {
                            rawTappedExpiredInputEvent.Dispatch();
                        }

                        float dragDistance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.ScreenToWorldPoint(startTouchPosition));
                        if (dragDistance > minimalDistanceToDrag) {
                            joyStick.SetActive(true);
                            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(startTouchPosition);
                            joyStick.transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, -4);
                            Vector2 direction = (Input.GetTouch(0).position - startTouchPosition).normalized;
                            dragDirIndicator.SetDragDir(direction);
                            TouchState = TouchStates.Dragging;
                            rawDraggingInputEvent.Dispatch(direction);

                        } else if (Time.time - StartDownTime > TimebeforeTappedExpired && TouchState != TouchStates.Holding) {
                            if (TouchState == TouchStates.Dragging) {
                                rawCancelDragInputEvent.Dispatch();
                            }

                            joyStick.SetActive(false);
                            TouchState = TouchStates.Holding;
                            rawHoldingInputEvent.Dispatch();
                        }
                    }
                } else {
                    rawReleaseInputEvent.Dispatch();

                    if (TouchState == TouchStates.Dragging) {
                        joyStick.SetActive(false);
                        Vector2 direction = (Input.GetTouch(0).position - startTouchPosition).normalized;
                        rawReleaseInDirectionInputEvent.Dispatch(direction);

                    } else if (TouchState == TouchStates.Tapped) {
                        rawJumpInputEvent.Dispatch();
                    }

                    TouchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }
}