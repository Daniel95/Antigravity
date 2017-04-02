using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInput : MonoBehaviour {

    private LevelLoader _levelLoader;

    public bool Unlocked { get; set; }

    public int LevelIndex { get; set; }

    private bool _gotMouseDown = false;

    private void Start()
    {
        _levelLoader = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
    }

    private void OnMouseOver()
    {
        if (!Unlocked)
            return;

        if(Input.GetMouseButtonDown(0))
             _gotMouseDown = true;

        if (_gotMouseDown && Input.GetMouseButtonUp(0)) {
            _levelLoader.LoadLevel(LevelIndex);
        }
    }

    private void OnMouseExit() {
        _gotMouseDown = false;
    }
}
