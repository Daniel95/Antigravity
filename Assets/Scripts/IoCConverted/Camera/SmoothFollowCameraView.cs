using UnityEngine;
using IoCPlus;
using System.Collections;

public class SmoothFollowCameraView : View {

    [SerializeField] private float smoothness = 0.375f;

    [Inject] private PlayerModel playerModel;

    private Transform target;
    private float yStartPos;
    private Vector2 velocity;
    private BoundsCamera boundsCamera;

    public override void Initialize() {
        Debug.Log("init cam");
        target = playerModel.Player.transform;
        transform.position = new Vector3(target.position.x, target.position.y, yStartPos);
    }

    private void LateUpdate() {
        if (target == null) { return; }
        Vector2 delta = target.position - transform.position;
        Vector2 destination = (Vector2)transform.position + delta;
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, boundsCamera.GetBoundsPosition(destination), ref velocity, smoothness, Mathf.Infinity, Time.deltaTime); 
        transform.position = new Vector3(nextPos.x, nextPos.y, yStartPos);
    }

    private void Awake() {
        boundsCamera = GetComponent<BoundsCamera>();
        yStartPos = transform.position.z;
    }
}
