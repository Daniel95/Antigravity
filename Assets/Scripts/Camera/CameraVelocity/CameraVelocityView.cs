using UnityEngine;
using System.Collections;
using IoCPlus;

public class CameraVelocityView : View, ICameraVelocity {

    public Vector2 PreviousTouchWorldPosition { get { return previousTouchViewportPosition; } }

    [SerializeField] private float moveSpeed = 4.6f;
    [SerializeField] private float mass = 1.3f;

    [Inject] private Ref<ICamera> cameraRef;
    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    private Vector2 velocity;
    private Coroutine moveUpdateCoroutine;
    private Vector2 previousTouchViewportPosition;

    public override void Initialize() {
        cameraVelocityRef.Set(this);
    }

    public void UpdatePreviousTouchScreenPosition(Vector2 touchScreenPosition) {
        previousTouchViewportPosition = Camera.main.ScreenToViewportPoint(touchScreenPosition);
    }

    public void Swipe(Vector2 touchScreenPosition) {
        Vector2 touchViewportPosition = Camera.main.ScreenToViewportPoint(touchScreenPosition);
        Vector2 delta = previousTouchViewportPosition - touchViewportPosition;
        Vector2 velocity = moveSpeed * delta;

        AddVelocity(velocity);
        previousTouchViewportPosition = touchViewportPosition;
    }

    public void AddVelocity(Vector2 velocity) {
        this.velocity += velocity;

        if (moveUpdateCoroutine == null) {
            moveUpdateCoroutine = StartCoroutine(MoveUpdate());
        }
    }

    public void SetVelocity(Vector2 velocity) {
        this.velocity = velocity;

        if (moveUpdateCoroutine == null) {
            moveUpdateCoroutine = StartCoroutine(MoveUpdate());
        }
    }

    private IEnumerator MoveUpdate() {
        ICamera camera = cameraRef.Get();

        while (velocity != Vector2.zero) {
            velocity /= mass;
            Vector2 nextPosition = (Vector2)transform.localPosition + velocity;
            if(camera.CameraBounds != null) {
                nextPosition = camera.CameraBounds.GetClampedBoundsPosition(nextPosition);
            }

            transform.localPosition = new Vector2(nextPosition.x, nextPosition.y);

            yield return null;
        }

        moveUpdateCoroutine = null;
    }

}