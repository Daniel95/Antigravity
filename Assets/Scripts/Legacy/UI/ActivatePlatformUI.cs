using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatformUI : MonoBehaviour {

    [SerializeField]
    private GameObject[] pcUI;

    [SerializeField]
    private GameObject[] mobileUI;

    // Use this for initialization
    void Start () {

        //activate the right UI for the active platform

        for (int i = 0; i < pcUI.Length; i++)
        {
            pcUI[i].SetActive(!Platform.PlatformIsMobile());
        }

        for (int i = 0; i < mobileUI.Length; i++)
        {
            mobileUI[i].SetActive(Platform.PlatformIsMobile());
        }
	}
}
