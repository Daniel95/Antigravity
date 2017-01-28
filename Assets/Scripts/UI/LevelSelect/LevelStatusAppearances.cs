using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatusAppearances : MonoBehaviour {

    [SerializeField]
    private Color[] _levelStatusColors;

    public static Color[] LevelStatusColors;

    public void Awake()
    {
        LevelStatusColors = _levelStatusColors;
    }
}
