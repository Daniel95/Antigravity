using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothness = 0.375f;

    [SerializeField]
    private int yPos = -10;

    [SerializeField]
    private Transform rightBound;

    [SerializeField]
    private Transform leftbound;

    [SerializeField]
    private Transform upBound;

    [SerializeField]
    private Transform downBound;

    private Vector2 velocity;
    private Camera cam;

    private float camHeightOffset, camWidthOffset;

    void Start()
    {
        cam = GetComponent<Camera>();

        camHeightOffset = cam.orthographicSize;
        camWidthOffset = camHeightOffset * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = target.position - cam.transform.position;
        Vector2 destination = (Vector2)transform.position + delta;
        Vector2 destinationInBounds = new Vector2(Mathf.Clamp(destination.x, leftbound.position.x + camWidthOffset,  rightBound.position.x - camWidthOffset), Mathf.Clamp(destination.y, downBound.position.y + camHeightOffset, upBound.position.y - camHeightOffset));
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, destinationInBounds, ref velocity, smoothness, Mathf.Infinity, Time.deltaTime); 
        transform.position = new Vector3(nextPos.x, nextPos.y, yPos);
    }
}
