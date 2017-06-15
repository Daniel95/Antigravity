using UnityEngine;
using System.Collections;
using IoCPlus;

public class DragCameraView : View, IDragCamera {

    [SerializeField] private float dragSpeed = 4.6f;
    [SerializeField] private float velocityDivider = 1.3f;

    [Inject] private Ref<ICamera> cameraRef;
    [Inject] private Ref<IDragCamera> dragCameraRef;

    private Vector2 velocity;
    private Vector2 lastMousePos;

    private Coroutine dragUpdateCoroutine;

    public override void Initialize() {
        dragCameraRef.Set(this);
    }

    public void EnableDragCamera(bool enable) {
        if(enable) {
            dragUpdateCoroutine = StartCoroutine(DragUpdate());
        } else if(dragUpdateCoroutine != null) {
            StopCoroutine(dragUpdateCoroutine);
            dragUpdateCoroutine = null;
        }
    }

    private IEnumerator DragUpdate() {
        while(true) {
            if (Input.GetMouseButtonDown(0)) {
                lastMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0)) {
                Vector2 _offset = -Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - lastMousePos);
                velocity += _offset * dragSpeed;

                lastMousePos = Input.mousePosition;
            }

            transform.position = cameraRef.Get().CameraBounds.GetClampedBoundsPosition((Vector2)transform.position + velocity);
            transform.position = new Vector3(transform.position.x, transform.position.y, cameraRef.Get().StartPosition.z);
            velocity /= velocityDivider;

            yield return null;
        }
    }
}