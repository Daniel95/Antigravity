using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatformUI : MonoBehaviour {

    [SerializeField]
    private GameObject PCStartUI;

    [SerializeField]
    private GameObject MobileStartUI;

    [SerializeField]
    private GameObject PCPlayUI;

    [SerializeField]
    private GameObject MobilePlayUI;

    // Use this for initialization
    void Start () {

        //activate the right UI for the active platform
        MobileStartUI.SetActive(Platform.PlatformIsMobile());
        PCStartUI.SetActive(!Platform.PlatformIsMobile());

        if(PCPlayUI != null && MobilePlayUI != null)
        {
            MobilePlayUI.SetActive(Platform.PlatformIsMobile());
            PCPlayUI.SetActive(!Platform.PlatformIsMobile());
        }
	}
}
