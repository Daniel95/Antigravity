using UnityEngine;
using System.Collections;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor {

    void OnSceneGUI() {
        FieldOfView fov = (FieldOfView)target;

        //Draws view reach
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.up, 360, fov.ViewRadius);
    }
}
 #endif