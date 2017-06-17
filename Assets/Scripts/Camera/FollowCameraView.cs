using IoCPlus;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
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
        transform.position = new Vector3(target.position.x, target.position.y, cameraRef.Get().StartPosition.z);
    }

    private IEnumerator FollowUpdate() {
        while (target != null && cameraRef.Get().CameraBounds != null) {
            Vector2 delta = target.position - transform.position;
            Vector2 destination = (Vector2)transform.position + delta;
            Vector2 nextPos = Vector2.SmoothDamp(transform.position, cameraRef.Get().CameraBounds.GetClampedBoundsPosition(destination), ref velocity, smoothness, Mathf.Infinity, Time.deltaTime);
            transform.position = new Vector3(nextPos.x, nextPos.y, cameraRef.Get().StartPosition.z);

            yield return null;
        }

        followUpdateCoroutine = null;
    }
}
