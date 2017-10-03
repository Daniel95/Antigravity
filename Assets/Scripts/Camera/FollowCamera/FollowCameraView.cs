using IoCPlus;
using System.Collections;
using UnityEngine;

public class FollowCameraView : View, IFollowCamera {

    [SerializeField] private float smoothness = 0.375f;

    [Inject] private Ref<ICamera> cameraRef;
    [Inject] private Ref<IFollowCamera> followCameraRef;

    private Transform target;
    private Vector2 velocity;

    private Coroutine followUpdateCoroutine;

    public override void Initialize() {
        followCameraRef.Set(this);
    }

    public void EnableFollowCamera(bool enable) {
        if (enable) {
            followUpdateCoroutine = StartCoroutine(FollowUpdate());
        } else if (followUpdateCoroutine != null) {
            StopCoroutine(followUpdateCoroutine);
            followUpdateCoroutine = null;
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
        Vector2 localTargetPosition = transform.InverseTransformVector(target.position);

        transform.position = localTargetPosition;
    }

    private IEnumerator FollowUpdate() {
        while (target != null) {
            Vector2 localTargetPosition = transform.InverseTransformVector(target.position);
            Vector2 delta = localTargetPosition - (Vector2)transform.localPosition;
            Vector2 destination = (Vector2)transform.localPosition + delta;
            if (cameraRef.Get().CameraBounds != null) {
                destination = cameraRef.Get().CameraBounds.GetClampedBoundsPosition(destination);
            }
            Vector2 nextPos = Vector2.SmoothDamp(transform.localPosition, destination, ref velocity, smoothness, Mathf.Infinity, Time.deltaTime);
            transform.localPosition = nextPos;

            yield return null;
        }

        followUpdateCoroutine = null;
    }

}
