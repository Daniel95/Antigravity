using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCamera : MonoBehaviour {

    [SerializeField]
    private Transform rightBound;

    [SerializeField]
    private Transform leftbound;

    [SerializeField]
    private Transform upBound;

    [SerializeField]
    private Transform downBound;

    private Camera _cam;

    private float _camHeightOffset, _camWidthOffset;

    void Start()
    {
        _cam = GetComponent<Camera>();

        _camHeightOffset = _cam.orthographicSize;
        _camWidthOffset = _camHeightOffset * _cam.aspect;
    }

    public Vector2 GetBoundsPosition(Vector2 position)
    {
        return new Vector2(Mathf.Clamp(position.x, leftbound.position.x + _camWidthOffset, rightBound.position.x - _camWidthOffset), Mathf.Clamp(position.y, downBound.position.y + _camHeightOffset, upBound.position.y - _camHeightOffset));
    }
}
