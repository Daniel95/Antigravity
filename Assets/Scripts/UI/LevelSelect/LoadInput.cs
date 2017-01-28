using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInput : MonoBehaviour {

    private LevelLoader _levelLoader;

    public bool Unlocked { get; set; }

    public int LevelIndex { get; set; }

    private void Start()
    {
        _levelLoader = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
    }

    private void OnMouseOver()
    {
        if (!Unlocked || !Input.GetMouseButtonUp(0))
            return;

        _levelLoader.LoadLevel(LevelIndex);
    }

    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");
    }

    public void OnPointerStay(PointerEventData eventData)
    {
        print("Stay");
    }*/
}
