using UnityEngine;
using System.Collections;
using IoCPlus;

public class CameraVelocityView : View, ICameraVelocity {

    public Vector2 PreviousTouchWorldPosition { get { return previousTouchViewportPosition; } }

    [SerializeField] private float swipeSpeed = 4.6f;
    [SerializeField] private float zoomSpeed = 4.6f;
    [SerializeField] private float mass = 1.3f;

    [Inject] private Ref<ICamera> cameraRef;
    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    private Vector2 moveVelocity;
    private Coroutine moveUpdateCoroutine;

    private float zoomVelocity;
    private Coroutine zoomUpdateCoroutine;

    private Vector2 previousTouchViewportPosition;

    public override void Initialize() {
        cameraVelocityRef.Set(this);
    }

    public void UpdatePreviousTouchScreenPosition(Vector2 touchScreenPosition) {
        previousTouchViewportPosition = Camera.main.ScreenToViewportPoint(touchScreenPosition);
    }

    public void Zoom(float delta) {
        zoomVelocity = zoomSpeed * delta;

        if (zoomUpdateCoroutine == null) {
            zoomUpdateCoroutine = StartCoroutine(ZoomUpdate());
        }
    }

    public void Swipe(Vector2 touchScreenPosition) {
        Vector2 touchViewportPosition = Camera.main.ScreenToViewportPoint(touchScreenPosition);
        Vector2 delta = previousTouchViewportPosition - touchViewportPosition;
        Vector2 velocity = swipeSpeed * delta;

        AddVelocity(velocity);
        previousTouchViewportPosition = touchViewportPosition;
    }

    public void AddVelocity(Vector2 velocity) {
        moveVelocity += velocity;

        if (moveUpdateCoroutine == null) {
            moveUpdateCoroutine = StartCoroutine(MoveUpdate());
        }
    }

    public void SetVelocity(Vector2 velocity) {
        moveVelocity = velocity;

        if (moveUpdateCoroutine == null) {
            moveUpdateCoroutine = StartCoroutine(MoveUpdate());
        }
    }

    private IEnumerator MoveUpdate() {
        ICamera camera = cameraRef.Get();

        while (moveVelocity != Vector2.zero) {
            moveVelocity /= mass;
            Vector2 orthographicSizeRelativeMoveVelocity = moveVelocity * (1 + camera.OrthographicSizeRatio);
            Vector2 nextPosition = (Vector2)transform.localPosition + orthographicSizeRelativeMoveVelocity;
            if(camera.CameraBounds != null) {
                nextPosition = camera.CameraBounds.GetClampedBoundsPosition(nextPosition);
            }

            transform.localPosition = new Vector2(nextPosition.x, nextPosition.y);

            yield return null;
        }

        moveUpdateCoroutine = null;
    }

    private IEnumerator ZoomUpdate() {
        ICamera camera = cameraRef.Get();

        while (Mathf.Abs(zoomVelocity) > 0.001f) {
            zoomVelocity /= mass;
            float orthographicSize = Camera.main.orthographicSize + zoomVelocity;
            float clampedOrthographicSize = Mathf.Clamp(orthographicSize, camera.MinOrthographicSize, camera.MaxOrthographicSize);
            camera.OrthographicSize = clampedOrthographicSize;

            yield return null;
        }

        zoomUpdateCoroutine = null;
    }

}