using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothness = 0.375f;

    private float _yStartPos;

    private Vector2 _velocity;

    private BoundsCamera boundsCamera;

    void Start()
    {
        boundsCamera = GetComponent<BoundsCamera>();

        _yStartPos = transform.position.z;

        transform.position = new Vector3(target.position.x, target.position.y, _yStartPos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = target.position - transform.position;
        Vector2 destination = (Vector2)transform.position + delta;
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, boundsCamera.GetBoundsPosition(destination), ref _velocity, smoothness, Mathf.Infinity, Time.deltaTime); 
        transform.position = new Vector3(nextPos.x, nextPos.y, _yStartPos);
    }
}
