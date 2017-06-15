using UnityEngine;
using System;
using System.ComponentModel;
using System.Diagnostics;
using SRDebugger;
using SRDebugger.Services;
using SRF;
using SRF.Service;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public partial class SROptions {

    [Category("Delete on startup :")]
    public bool DeleteGameSaveOnStart {
        get {
            return Convert.ToBoolean(PlayerPrefs.GetInt("DeleteGameSaveOnStart", 0));
        }
        set {
            PlayerPrefs.SetInt("DeleteGameSaveOnStart", Convert.ToInt16(value));
        }
    }
}
