using System;
using UnityEngine;

public class LevelObjectLevelEditorInput : MonoBehaviour {

    protected Action OnMove;
    protected Action OnTranslate;
    protected Action OnRotate;
    protected Action OnScale;
    protected Action OnInstantiate;
    protected Action OnPlace;

    public void TranslateInput() {
        if (OnTranslate != null) {
            OnTranslate();
        }
        OnMove();
    }

    public void RotateInput() {
        if (OnRotate != null) {
            OnRotate();
        }
        OnMove();
    }

    public void ScaleInput() {
        if (OnScale != null) {
            OnScale();
        }
        OnMove();
    }

    public void InstantiateInput() {
        if (OnInstantiate != null) {
            OnInstantiate();
        }
    }

    public void PlaceInput() {
        if (OnPlace != null) {
            OnPlace();
        }
    }

    private void MoveInput() {
        if (OnMove != null) {
            OnMove();
        }
    }

}
