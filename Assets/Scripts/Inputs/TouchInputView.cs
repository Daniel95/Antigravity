using HedgehogTeam.EasyTouch;
using IoCPlus;
using UnityEngine;

public class TouchInputView : View {

    [Inject] private TapEvent tapEvent;
    [Inject] private DragStartedEvent dragStartedEvent;
    [Inject] private DragMovedEvent dragMovedEvent;
    [Inject] private DragStoppedEvent dragStoppedEvent;
    [Inject] private EmptyTapEvent emptyTapEvent;
    [Inject] private SwipeMovedEvent swipeMovedEvent;
    [Inject] private SwipedLeftEvent swipedLeftEvent;
    [Inject] private SwipedRightEvent swipedRightEvent;
    [Inject] private SwipeEndEvent swipeEndEvent;
    [Inject] private TouchDownEvent touchDownEvent;
    [Inject] private TouchStartEvent touchStartEvent;
    [Inject] private OutsideUITouchStartEvent outsideUITouchStartEvent;
    [Inject] private TouchUpEvent touchUpEvent;
    [Inject] private UITouchUpEvent uiTouchUpEvent;
    [Inject] private SingleTouchDownEvent singleTouchDownEvent;
    [Inject] private SingleTouchStartEvent singleTouchStartEvent;
    [Inject] private SingleTouchUpEvent singleTouchUpEvent;
    [Inject] private SingleTouchCancelEvent singleTouchCancelEvent;
    [Inject] private MultiTouchStartEvent multiTouchStartEvent;
    [Inject] private MultiTouchDownEvent multiTouchDownEvent;
    [Inject] private MultiTouchUpEvent multiTouchUpEvent;
    [Inject] private PinchStartedEvent pinchStartedEvent;
    [Inject] private PinchMovedEvent pinchMovedEvent;
    [Inject] private PinchStoppedEvent pinchStoppedEvent;
    [Inject] private TwistEvent twistEvent;

    private bool isPinching;

    private int uiLayer;

    public override void Initialize() {
        EasyTouch.On_DragStart += OnDragStart;
        EasyTouch.On_Drag += OnDrag;
        EasyTouch.On_DragEnd += OnDragEnd;
        EasyTouch.On_SimpleTap += OnSimpleTap;
        EasyTouch.On_Swipe += OnSwipe;
        EasyTouch.On_SwipeEnd += OnSwipeEnd;
        EasyTouch.On_TouchDown += OnTouchDown;
        EasyTouch.On_TouchStart += OnTouchStart;
        EasyTouch.On_TouchUp += OnTouchUp;
        EasyTouch.On_Pinch += OnPinch;
        EasyTouch.On_UIElementTouchUp += EasyTouch_On_UIElementTouchUp;
        EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
        EasyTouch.On_TouchDown2Fingers += On_TouchDown2Fingers;
        EasyTouch.On_TouchUp2Fingers += On_TouchUp2Fingers;
        EasyTouch.On_Twist += On_Twist;
    }

    public override void Dispose(){
        EasyTouch.On_DragStart -= OnDragStart;
        EasyTouch.On_Drag -= OnDrag;
        EasyTouch.On_DragEnd -= OnDragEnd;
        EasyTouch.On_SimpleTap -= OnSimpleTap;
        EasyTouch.On_Swipe -= OnSwipe;
        EasyTouch.On_SwipeEnd -= OnSwipeEnd;
        EasyTouch.On_TouchDown -= OnTouchDown;
        EasyTouch.On_TouchStart -= OnTouchStart;
        EasyTouch.On_TouchUp -= OnTouchUp;
        EasyTouch.On_Pinch -= OnPinch;
        EasyTouch.On_UIElementTouchUp -= EasyTouch_On_UIElementTouchUp;
        EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
        EasyTouch.On_TouchDown2Fingers -= On_TouchDown2Fingers;
        EasyTouch.On_TouchUp2Fingers -= On_TouchUp2Fingers;
        EasyTouch.On_Twist -= On_Twist;
    }

    private void OnDragStart(Gesture gesture) {
        dragStartedEvent.Dispatch(gesture.position);
    }

    private void OnDrag(Gesture gesture) {
        if (isPinching) { return; }
        dragMovedEvent.Dispatch(new DragMovedEvent.Parameter() {
            DeltaPosition = gesture.deltaPosition,
            Position = gesture.position
        });
    }

    private void OnDragEnd(Gesture gesture) {
        if (isPinching) {
            isPinching = false;
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

    private void OnSwipe(Gesture gesture) {
        if (isPinching) { return; }

        swipeMovedEvent.Dispatch(new SwipeMovedEvent.Parameter() {
            DeltaPosition = gesture.deltaPosition,
            Position = gesture.position
        });
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

        swipeEndEvent.Dispatch(swipeVector);
    }

    private void OnTouchStart(Gesture gesture) {
        touchStartEvent.Dispatch(gesture.position);
        if (gesture.touchCount == 1) {
            singleTouchStartEvent.Dispatch(gesture.position);
        }
        if (gesture.touchCount >= 2) {
            singleTouchCancelEvent.Dispatch();
        }
        if (gesture.pickedObject == null || gesture.pickedObject.layer != uiLayer) {
            outsideUITouchStartEvent.Dispatch(gesture.position);
        }
    }

    private void OnTouchDown(Gesture gesture) {
        touchDownEvent.Dispatch(gesture.position);
        if (gesture.touchCount == 1) {
            singleTouchDownEvent.Dispatch(gesture.position);
        }
    }

    private void OnTouchUp(Gesture gesture) {
        touchUpEvent.Dispatch(gesture.position);
        if (gesture.touchCount == 1) {
            singleTouchUpEvent.Dispatch(gesture.position);
        }
    }

    private void OnPinch(Gesture gesture) {
        if (!isPinching) {
            isPinching = true;
            dragStoppedEvent.Dispatch(gesture.position);
            pinchStartedEvent.Dispatch(gesture.position);
        }
        pinchMovedEvent.Dispatch(gesture.position, gesture.deltaPinch);
    }

    private void EasyTouch_On_UIElementTouchUp(Gesture gesture) {
        uiTouchUpEvent.Dispatch(gesture.position);
    }
    
    private void On_Twist(Gesture gesture) {
        twistEvent.Dispatch(gesture.twistAngle);
    }

    private void On_TouchStart2Fingers(Gesture gesture) {
        multiTouchStartEvent.Dispatch(gesture.position);
    }

    private void On_TouchDown2Fingers(Gesture gesture) {
        multiTouchDownEvent.Dispatch(gesture.position);

    }

    private void On_TouchUp2Fingers(Gesture gesture) {
        multiTouchUpEvent.Dispatch(gesture.position);
    }

    private void Awake() {
        uiLayer = LayerMask.NameToLayer("UI");
    }
}