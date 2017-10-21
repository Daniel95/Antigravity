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

    [Inject] private CameraZoomedEvent cameraZoomedEvent;

    private Vector2 moveVelocity;
    private Coroutine moveUpdateCoroutine;

    private float zoomVelocity;
    private Coroutine zoomUpdateCoroutine;

    private Vector2 previousTouchViewportPosition;

    public override void Initialize() {
        cameraVelocityRef.Set(this);
    }

    public void ResetPosition() {
        transform.localPosition = Vector2.zero;
    }

    public void UpdatePreviousTouchScreenPosition(Vector2 touchScreenPosition) {
        previousTouchViewportPosition = Camera.main.ScreenToViewportPoint(touchScreenPosition);
    }

    public void Zoom(Vector2 position, float delta) {
        zoomVelocity = zoomSpeed * delta;

        if (zoomUpdateCoroutine == null) {
            zoomUpdateCoroutine = StartCoroutine(ZoomUpdate(position));
        }
    }

    public void StartSwipe(Vector2 touchScreenStartPosition) {
        Vector2 touchViewportPosition = Camera.main.ScreenToViewportPoint(touchScreenStartPosition);
        previousTouchViewportPosition = touchViewportPosition;
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

    private IEnumerator ZoomUpdate(Vector2 zoomScreenPosition) {
        ICamera camera = cameraRef.Get();

        Vector2 zoomWorldPosition = Camera.main.ScreenToWorldPoint(zoomScreenPosition);

        while (Mathf.Abs(zoomVelocity) > 0.001f) {
            zoomVelocity /= mass;
            float orthographicSize = Camera.main.orthographicSize - zoomVelocity;
            float clampedOrthographicSize = Mathf.Clamp(orthographicSize, camera.MinOrthographicSize, camera.MaxOrthographicSize);
            camera.OrthographicSize = clampedOrthographicSize;

            float speed = (1.0f / Camera.main.orthographicSize * zoomVelocity);
            Vector3 offset = ((Vector3)zoomWorldPosition - transform.localPosition);
            transform.localPosition += offset * speed;

            cameraZoomedEvent.Dispatch();

            yield return null;
        }

        zoomUpdateCoroutine = null;
    }

}