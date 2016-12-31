using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatformUI : MonoBehaviour {

    [SerializeField]
    private GameObject pcUI;

    [SerializeField]
    private GameObject mobileUI;

    // Use this for initialization
    void Start () {

        if(pcUI != null && mobileUI != null)
        {
            //activate the right UI for the active platform
            pcUI.SetActive(!Platform.PlatformIsMobile());
            mobileUI.SetActive(Platform.PlatformIsMobile());
        }
	}
}
