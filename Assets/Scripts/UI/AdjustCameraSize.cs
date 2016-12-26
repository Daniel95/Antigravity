using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraSize : MonoBehaviour {

    [SerializeField]
    private int pCCameraSize = 10;

    [SerializeField]
    private int mobileCameraSize = 15;

    private void Start()
    {
        Camera cam = GetComponent<Camera>();

        if (Platform.PlatformIsMobile()) {
            cam.orthographicSize = mobileCameraSize;
        }
        else
        {
            cam.orthographicSize = pCCameraSize;
        }
    }
}
