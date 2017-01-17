using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraSize : MonoBehaviour {

    [SerializeField]
    private int pcCameraSize = 10;

    [SerializeField]
    private int mobileCameraSize = 15;

    private void Start()
    {
        Camera cam = GetComponent<Camera>();

        cam.orthographicSize = Platform.PlatformIsMobile() ? mobileCameraSize : pcCameraSize;
    }
}
