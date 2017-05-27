using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class HookableLayer {

    public static int GrappleSurface = LayerMask.NameToLayer("GrappleHook");
    public static int PullSurface = LayerMask.NameToLayer("PullHook");

    public static List<int> GetHookableLayers() {
        Type hookableLayers = typeof(HookableLayer);
        FieldInfo[] fields = hookableLayers.GetFields();

        List<int> layers = new List<int>();
        
        foreach (FieldInfo field in fields) {
            layers.Add(System.Int32.Parse(field.GetValue(hookableLayers).ToString()));
        }
        return layers;
    }
}