using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorSelectionFieldBoxColors : MonoBehaviour {

    public static LevelEditorSelectionFieldBoxColors Instance { get { return GetInstance(); } }

    private static LevelEditorSelectionFieldBoxColors instance;

    [SerializeField] private List<LevelEditorSelectionFieldBoxColorWithType> boxColorsWithType;

    public Color GetColorByType(LevelEditorSelectionFieldBoxColorType levelEditorSelectionFieldBoxColorType) {
        LevelEditorSelectionFieldBoxColorWithType boxColorWithType = boxColorsWithType.Find(x => x.Type == levelEditorSelectionFieldBoxColorType);
        return boxColorWithType.Color;
    }

    private static LevelEditorSelectionFieldBoxColors GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorSelectionFieldBoxColors>();
        }
        return instance;
    }

}

[Serializable]
public class LevelEditorSelectionFieldBoxColorWithType {
    public Color Color;
    public LevelEditorSelectionFieldBoxColorType Type;
}

public enum LevelEditorSelectionFieldBoxColorType {
    Default,
    Error,
}
