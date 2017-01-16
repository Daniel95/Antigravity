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

    private Vector2 _velocity;
    private Camera _cam;

    private float _camHeightOffset, _camWidthOffset;

    void Start()
    {
        _cam = GetComponent<Camera>();

        _camHeightOffset = _cam.orthographicSize;
        _camWidthOffset = _camHeightOffset * _cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = target.position - _cam.transform.position;
        Vector2 destination = (Vector2)transform.position + delta;
        Vector2 destinationInBounds = new Vector2(Mathf.Clamp(destination.x, leftbound.position.x + _camWidthOffset,  rightBound.position.x - _camWidthOffset), Mathf.Clamp(destination.y, downBound.position.y + _camHeightOffset, upBound.position.y - _camHeightOffset));
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, destinationInBounds, ref _velocity, smoothness, Mathf.Infinity, Time.deltaTime); 
        transform.position = new Vector3(nextPos.x, nextPos.y, yPos);
    }
}
