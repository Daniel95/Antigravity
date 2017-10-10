using HedgehogTeam.EasyTouch;
using IoCPlus;
using UnityEngine;

public class TouchInputView : View, ITouchInput {

    public bool TouchStarted2FingersAfterIdle { get { return touchStarted2FingersAfterIdle; } }
    public bool SwipeMoving { get { return swipeMoving; } }
    public bool SwipeMoving2Fingers { get { return swipe2FingersDelta > pinchDelta; } }
    public bool Pinching { get { return pinching; } }
    public bool Idling { get { return idling; } }

    [Inject] private Ref<ITouchInput> touchInputViewRef;

    [Inject] private TapEvent tapEvent;
    [Inject] private DragStartedEvent dragStartedEvent;
    [Inject] private DragMovedEvent dragMovedEvent;
    [Inject] private DragStoppedEvent dragStoppedEvent;
    [Inject] private EmptyTapEvent emptyTapEvent;
    [Inject] private SwipeStartEvent swipeStartEvent;
    [Inject] private SwipeMovedEvent swipeMovedEvent;
    [Inject] private SwipedLeftEvent swipedLeftEvent;
    [Inject] private SwipedRightEvent swipedRightEvent;
    [Inject] private SwipeEndEvent swipeEndEvent;
    [Inject] private TouchDownEvent touchDownEvent;
    [Inject] private TouchStartEvent touchStartEvent;
    [Inject] private OutsideUITouchStartEvent outsideUITouchStartEvent;
    [Inject] private TouchUpEvent touchUpEvent;
    [Inject] private UITouchUpEvent uiTouchUpEvent;
    [Inject] private TouchDown1FingerEvent touchDown1FingerEvent;
    [Inject] private TouchStart1FingerEvent touchStart1FingerEvent;
    [Inject] private TouchUp1FingerEvent touchUp1FingerEvent;
    [Inject] private Touch1FingerCancelEvent touch1FingerCancelEvent;
    [Inject] private TouchStart2FingersEvent touchStart2FingersEvent;
    [Inject] private TouchDown2FingersEvent touchDown2FingersEvent;
    [Inject] private TouchUp2FingersEvent touchUp2FingersEvent;
    [Inject] private SwipeStart2FingersEvent swipeStart2FingersEvent;
    [Inject] private SwipeMoved2FingersEvent swipeMoved2FingersEvent;
    [Inject] private SwipeEnd2FingersEvent swipeEnd2FingersEvent;
    [Inject] private PinchStartedEvent pinchStartedEvent;
    [Inject] private PinchMovedEvent pinchMovedEvent;
    [Inject] private PinchStoppedEvent pinchStoppedEvent;
    [Inject] private TwistEvent twistEvent;
    [Inject] private IdleStartEvent idleStartEvent;

    private static bool instance;

    private float swipe2FingersDelta;
    private float pinchDelta;

    private static bool touchStarted2FingersAfterIdle;
    private bool pinching;
    private bool swipeMoving;
    private bool swipeMoving2Fingers;
    private bool idling;

    private int uiLayer;

    private Vector2 lastSwipePosition;
    private Vector2 lastSwipe2FingersPosition;

    public override void Initialize() {
        touchInputViewRef.Set(this);

        EasyTouch.On_DragStart += OnDragStart;
        EasyTouch.On_Drag += OnDrag;
        EasyTouch.On_DragEnd += OnDragEnd;
        EasyTouch.On_SimpleTap += OnSimpleTap;
        EasyTouch.On_SwipeStart += OnSwipeStart;
        EasyTouch.On_Swipe += OnSwipe;
        EasyTouch.On_SwipeEnd += OnSwipeEnd;
        EasyTouch.On_TouchDown += OnTouchDown;
        EasyTouch.On_TouchStart += OnTouchStart;
        EasyTouch.On_TouchUp += OnTouchUp;
        EasyTouch.On_Pinch += OnPinch;
        EasyTouch.On_PinchEnd += OnPinchEnd;
        EasyTouch.On_UIElementTouchUp += EasyTouch_On_UIElementTouchUp;
        EasyTouch.On_TouchStart2Fingers += OnTouchStart2Fingers;
        EasyTouch.On_TouchDown2Fingers += OnTouchDown2Fingers;
        EasyTouch.On_TouchUp2Fingers += OnTouchUp2Fingers;
        EasyTouch.On_Twist += OnTwist;
        EasyTouch.On_SwipeStart2Fingers += OnSwipeStart2Fingers;
        EasyTouch.On_Swipe2Fingers += OnSwipe2Fingers;
        EasyTouch.On_SwipeEnd2Fingers += OnSwipeEnd2Fingers;
    }

    public override void Dispose(){
        EasyTouch.On_DragStart -= OnDragStart;
        EasyTouch.On_Drag -= OnDrag;
        EasyTouch.On_DragEnd -= OnDragEnd;
        EasyTouch.On_SimpleTap -= OnSimpleTap;
        EasyTouch.On_SwipeStart -= OnSwipeStart;
        EasyTouch.On_Swipe -= OnSwipe;
        EasyTouch.On_SwipeEnd -= OnSwipeEnd;
        EasyTouch.On_TouchDown -= OnTouchDown;
        EasyTouch.On_TouchStart -= OnTouchStart;
        EasyTouch.On_TouchUp -= OnTouchUp;
        EasyTouch.On_Pinch -= OnPinch;
        EasyTouch.On_PinchEnd -= OnPinchEnd;
        EasyTouch.On_UIElementTouchUp -= EasyTouch_On_UIElementTouchUp;
        EasyTouch.On_TouchStart2Fingers -= OnTouchStart2Fingers;
        EasyTouch.On_TouchDown2Fingers -= OnTouchDown2Fingers;
        EasyTouch.On_TouchUp2Fingers -= OnTouchUp2Fingers;
        EasyTouch.On_Twist -= OnTwist;
        EasyTouch.On_SwipeStart2Fingers -= OnSwipeStart2Fingers;
        EasyTouch.On_Swipe2Fingers -= OnSwipe2Fingers;
        EasyTouch.On_SwipeEnd2Fingers -= OnSwipeEnd2Fingers;
    }

    private void OnDragStart(Gesture gesture) {
        dragStartedEvent.Dispatch(gesture.position);
    }

    private void OnDrag(Gesture gesture) {
        if (pinching) { return; }

        dragMovedEvent.Dispatch(new DragMovedEvent.Parameter() {
            DeltaPosition = gesture.deltaPosition,
            Position = gesture.position
        });
    }

    private void OnDragEnd(Gesture gesture) {
        if (pinching) {
            pinching = false;
            pinchStoppedEvent.Dispatch(gesture.position);
        } else {
            dragStoppedEvent.Dispatch(gesture.position);
        }
    }

    private void OnSimpleTap(Gesture gesture) {
        tapEvent.Dispatch(gesture);
        if (gesture.pickedObject == null) {
            emptyTapEvent.Dispatch(gesture.position);
        }
    }

    private void OnSwipeStart(Gesture gesture) {
        lastSwipePosition = gesture.position;
        swipeStartEvent.Dispatch(gesture.position);
    }

    private void OnSwipe(Gesture gesture) {
        if (gesture.position == lastSwipePosition) { return; }

        swipeMoving = true;

        swipeMovedEvent.Dispatch(new SwipeMovedEvent.Parameter() {
            DeltaPosition = gesture.deltaPosition,
            Position = gesture.position
        });

        lastSwipePosition = gesture.position;
    }

    private void OnSwipeEnd(Gesture gesture) {
        Vector2 swipeVector = gesture.swipeVector;

        bool isHorizontalSwipe = Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y);
        if (isHorizontalSwipe) {
            bool isRightSwipe = swipeVector.x > 0;
            if (isRightSwipe) {
                swipedRightEvent.Dispatch();
            } else {
                swipedLeftEvent.Dispatch();
            }
        }

        swipeMoving = false;
        swipeEndEvent.Dispatch(swipeVector);
    }

    private void OnTouchStart(Gesture gesture) {
        idling = false;

        touchStartEvent.Dispatch(gesture.position);
        if (gesture.touchCount == 1) {
            touchStart1FingerEvent.Dispatch(gesture.position);
        }
        if (gesture.touchCount >= 2) {
            touch1FingerCancelEvent.Dispatch();
        }
        if (gesture.pickedObject == null || gesture.pickedObject.layer != uiLayer) {
            outsideUITouchStartEvent.Dispatch(gesture.position);
        }
    }

    private void OnTouchDown(Gesture gesture) {
        touchDownEvent.Dispatch(gesture.position);
        if (gesture.touchCount == 1) {
            touchDown1FingerEvent.Dispatch(gesture.position);
        }
    }

    private void OnTouchUp(Gesture gesture) {
        if(pinching) {
            pinching = false;
            pinchStoppedEvent.Dispatch(gesture.position);
        }
        touchUpEvent.Dispatch(gesture.position);
        if (gesture.touchCount == 1) {
            touchUp1FingerEvent.Dispatch(gesture.position);
        }
    }

    private void OnPinch(Gesture gesture) {
        pinchDelta = Mathf.Abs(gesture.deltaPinch);
        if (!pinching) {
            dragStoppedEvent.Dispatch(gesture.position);
            pinchStartedEvent.Dispatch(gesture.position);
            pinching = true;
        }
        pinchMovedEvent.Dispatch(gesture.position, gesture.deltaPinch);
    }

    private void OnPinchEnd(Gesture gesture) {
        pinchDelta = 0;
        pinchStoppedEvent.Dispatch(gesture.position);
    }

    private void EasyTouch_On_UIElementTouchUp(Gesture gesture) {
        uiTouchUpEvent.Dispatch(gesture.position);
    }
    
    private void OnTwist(Gesture gesture) {
        twistEvent.Dispatch(gesture.twistAngle);
    }

    private void OnTouchStart2Fingers(Gesture gesture) {
        touchStart2FingersEvent.Dispatch(gesture.position);
        touchStarted2FingersAfterIdle = true;
    }

    private void OnTouchDown2Fingers(Gesture gesture) {
        touchDown2FingersEvent.Dispatch(gesture.position);
    }

    private void OnTouchUp2Fingers(Gesture gesture) {
        touchUp2FingersEvent.Dispatch(gesture.position);
    }

    private void OnSwipeStart2Fingers(Gesture gesture) {
        lastSwipe2FingersPosition = gesture.position;
        swipeStart2FingersEvent.Dispatch(gesture.position);
    }

    private void OnSwipe2Fingers(Gesture gesture) {
        swipe2FingersDelta = Vector2.Distance(lastSwipe2FingersPosition, gesture.position);

        if (gesture.position == lastSwipe2FingersPosition) { return; }
        swipeMoving2Fingers = true;
        swipeMoved2FingersEvent.Dispatch(new SwipeMoved2FingersEvent.Parameter() {
            DeltaPosition = gesture.deltaPosition,
            Position = gesture.position
        });

        lastSwipe2FingersPosition = gesture.position;
    }

    private void OnSwipeEnd2Fingers(Gesture gesture) {
        swipe2FingersDelta = 0;
        swipeMoving2Fingers = false;
        swipeEnd2FingersEvent.Dispatch(gesture.swipeVector);
    }

    private void Awake() {
        uiLayer = LayerMask.NameToLayer("UI");
    }

    private void Update() {
        if (!idling && EasyTouch.GetTouchCount() <= 0) {
            idleStartEvent.Dispatch();
            touchStarted2FingersAfterIdle = false;
            idling = true;
        }
    }
}