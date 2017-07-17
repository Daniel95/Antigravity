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

    [Category("Go To Scene :")]
    public void TestLevel() {
        debugInputView.GoToScene(Scenes.TestLvl);
        SRDebug.Instance.HideDebugPanel();
    }

    [Category("Unlock :")]
    public void CompleteAllLevels() {
        debugInputView.CompleteAllLevels();
        SRDebug.Instance.HideDebugPanel();
    }

    [Category("Delete :")]
    public void DeleteCompletedLevelsSave() {
        debugInputView.DeleteCompletedLevelsSave();
        SRDebug.Instance.HideDebugPanel();
    }

    [Category("Delete on startup :")]
    public bool DeleteGameSaveOnStart {
        get {
            return Convert.ToBoolean(PlayerPrefs.GetInt("DeleteGameSaveOnStart", 0));
        }
        set {
            PlayerPrefs.SetInt("DeleteGameSaveOnStart", Convert.ToInt16(value));
        }
    }

    private DebugInputView debugInputView {
        get {
            if(DebugInputView.Instance == null) {
                Debug.Log("DebugInputView does not exist.");
                return null;
            }
            return DebugInputView.Instance;
        }
    }
}
