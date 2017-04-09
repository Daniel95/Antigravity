using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{
    [SerializeField]
    private float dragSpeed = 4.6f;

    [SerializeField]
    private float velocityDivider = 1.3f;

    private Vector2 _velocity;

    private Vector2 _lastMousePos;

    private BoundsCamera _boundsCamera;

    private float zStartPos;

    private void Start()
    {
        _boundsCamera = GetComponent<BoundsCamera>();
        zStartPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 _offset = -Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - _lastMousePos);
            _velocity += _offset * dragSpeed;

            _lastMousePos = Input.mousePosition;
        }

        transform.position = _boundsCamera.GetBoundsPosition((Vector2)transform.position + _velocity);
        transform.position = new Vector3(transform.position.x, transform.position.y, zStartPos);
        _velocity /= velocityDivider;
    }
}