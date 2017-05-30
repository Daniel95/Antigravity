using IoCPlus;
using System.Collections;
using UnityEngine;

public class PCInputView : View, IPCInput {

    [SerializeField] private KeyCode jumpInput = KeyCode.Space;
    [SerializeField] private KeyCode aimInput = KeyCode.Mouse0;
    [SerializeField] private float minDistFromPlayer = 6;
    [SerializeField] private float TimebeforeTappedExpired = 0.15f;

    [Inject] private Ref<IPCInput> pcInputRef;

    [Inject] private RawCancelDragInputEvent rawCancelDragInputEvent;
    [Inject] private RawDraggingInputEvent rawDraggingInputEvent;
    [Inject] private RawHoldingInputEvent rawHoldingInputEvent;
    [Inject] private RawJumpInputEvent rawJumpInputEvent;
    [Inject] private RawReleaseInDirectionInputEvent rawReleaseInDirectionInputEvent;
    [Inject] private RawReleaseInputEvent rawReleaseInputEvent;
    [Inject] private RawTappedExpiredInputEvent rawTappedExpiredInputEvent;

    private enum TouchStates { Holding, Dragging, Tapped, None }

    private TouchStates touchState = TouchStates.None;
    private float startDownTime;
    private Coroutine inputUpdateCoroutine;

    public override void Initialize() {
        pcInputRef.Set(this);
    }

    public void ResetTouched() {
        rawCancelDragInputEvent.Dispatch();
    }

    public void EnableInput(bool enable) {
        if (enable) {
            inputUpdateCoroutine = StartCoroutine(InputUpdate());
        } else if (inputUpdateCoroutine != null) {
            StopCoroutine(inputUpdateCoroutine);
            inputUpdateCoroutine = null;
        }
    }

    private IEnumerator InputUpdate() {
        while (true) {
            //key inputs
            if (Input.GetKeyDown(jumpInput)) {
                rawJumpInputEvent.Dispatch();
            }

            //mouse inputs
            if (Input.GetKeyDown(aimInput) && !InputDetect.CheckUICollision(Input.mousePosition)) {
                touchState = TouchStates.Tapped;

                startDownTime = Time.time;
            }

            if (touchState != TouchStates.None) {
                //not yet released
                if (!Input.GetKeyUp(aimInput)) {
                    if (Time.time - startDownTime > TimebeforeTappedExpired) {
                        if (touchState == TouchStates.Tapped) {
                            rawTappedExpiredInputEvent.Dispatch();
                        }

                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > minDistFromPlayer) {

                            touchState = TouchStates.Dragging;
                            rawDraggingInputEvent.Dispatch(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                        } else if (touchState != TouchStates.Holding) {

                            if (touchState == TouchStates.Dragging) {
                                rawCancelDragInputEvent.Dispatch();
                            }

                            touchState = TouchStates.Holding;

                            rawHoldingInputEvent.Dispatch();
                        }
                    }
                } else { //released 
                    rawReleaseInputEvent.Dispatch();

                    if (touchState != TouchStates.Holding) {
                        rawReleaseInDirectionInputEvent.Dispatch(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                    }

                    touchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }

}