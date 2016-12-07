using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothness = 0.15f;

    private Vector3 velocity = Vector3.zero;
    private Camera gameCamera;

    void Start()
    {
        gameCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = gameCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothness);
        }
    }
}
