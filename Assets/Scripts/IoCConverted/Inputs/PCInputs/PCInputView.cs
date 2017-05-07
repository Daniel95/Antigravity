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

    private Coroutine inputUpdate;

    private enum TouchStates { Holding, Dragging, Tapped, None }

    private TouchStates TouchState = TouchStates.None;

    private float StartDownTime;

    public override void Initialize() {
        pcInputRef.Set(this);
    }

    public void ResetTouched() {
        rawCancelDragInputEvent.Dispatch();
    }

    public void EnableInput(bool enable) {
        if (enable) {
            inputUpdate = StartCoroutine(InputUpdate());
        } else if (inputUpdate != null) {
            StopCoroutine(inputUpdate);
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
                TouchState = TouchStates.Tapped;

                StartDownTime = Time.time;
            }

            if (TouchState != TouchStates.None) {
                //not yet released
                if (!Input.GetKeyUp(aimInput)) {
                    if (Time.time - StartDownTime > TimebeforeTappedExpired) {
                        if (TouchState == TouchStates.Tapped) {
                            rawTappedExpiredInputEvent.Dispatch();
                        }

                        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > minDistFromPlayer) {

                            TouchState = TouchStates.Dragging;
                            rawDraggingInputEvent.Dispatch(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                        } else if (TouchState != TouchStates.Holding) {

                            if (TouchState == TouchStates.Dragging) {
                                rawCancelDragInputEvent.Dispatch();
                            }

                            TouchState = TouchStates.Holding;

                            rawHoldingInputEvent.Dispatch();
                        }
                    }
                } else //released
                {
                    rawReleaseInputEvent.Dispatch();

                    if (TouchState != TouchStates.Holding) {
                        rawReleaseInDirectionInputEvent.Dispatch(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized);
                    }

                    TouchState = TouchStates.None;
                }
            }

            yield return null;
        }
    }

}