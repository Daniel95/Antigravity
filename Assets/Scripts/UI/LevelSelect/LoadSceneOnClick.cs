using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadSceneOnClick : MonoBehaviour {

    private SceneLoader sceneLoader;

    private int levelIndex;

    private void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        levelIndex = int.Parse(GetComponentInChildren<TextMesh>().text);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            sceneLoader.LoadNewSceneName("Lvl" + levelIndex);
        }
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
