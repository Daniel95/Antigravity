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
    [Inject] private RawTapInputEvent rawJumpInputEvent;
    [Inject] private RawReleaseInDirectionInputEvent rawReleaseInDirectionInputEvent;
    [Inject] private RawReleaseInputEvent rawReleaseInputEvent;
    [Inject] private RawTappedExpiredInputEvent rawTappedExpiredInputEvent;

    private Coroutine inputUpdateCoroutine;
    private enum TouchStates { Holding, Dragging, Tapped, None }
    private TouchStates TouchState = TouchStates.None;
    private float touchDownTime;
    private GameObject joyStick;
    private JoyStickUI dragDirIndicator;
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
            dragDirIndicator = joyStick.GetComponent<JoyStickUI>();
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
            bool startedTouching = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
            if (startedTouching && !InputHelper.CheckUICollision(Input.GetTouch(0).position)) {
                TouchState = TouchStates.Tapped;
                startTouchPosition = Input.GetTouch(0).position;
                touchDownTime = Time.time;
            }

            if (TouchState != TouchStates.None) {
                if (Input.GetTouch(0).phase != TouchPhase.Ended) {
                    float timePassedSinceTouchDown = Time.time - touchDownTime;
                    if (timePassedSinceTouchDown > TimebeforeTappedExpired) {
                        if (TouchState == TouchStates.Tapped) {
                            rawTappedExpiredInputEvent.Dispatch();
                        }

                        Vector2 worldCurrentTouchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector2 worldStartTouchPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);
                        float dragDistance = Vector2.Distance(worldStartTouchPosition, worldCurrentTouchPosition);

                        if (dragDistance > minimalDistanceToDrag) {
                            joyStick.SetActive(true);
                            joyStick.transform.position = new Vector3(worldStartTouchPosition.x, worldStartTouchPosition.y, -4);

                            Vector2 direction = (Input.GetTouch(0).position - startTouchPosition).normalized;
                            dragDirIndicator.SetDragDir(direction);

                            TouchState = TouchStates.Dragging;
                            rawDraggingInputEvent.Dispatch(direction);

                        } else if (timePassedSinceTouchDown > TimebeforeTappedExpired && TouchState != TouchStates.Holding) {
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