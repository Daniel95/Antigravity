using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFieldColors : MonoBehaviour {

    public static SelectionFieldColors Instance { get { return GetInstance(); } }

    private static SelectionFieldColors instance;

    [SerializeField] private List<SelectionFieldColorType> boxColorTypes;

    public Color GetColorByType(LevelEditorSelectionFieldBoxColorType selectionFieldBoxColorType) {
        SelectionFieldColorType boxColorType = boxColorTypes.Find(x => x.Type == selectionFieldBoxColorType);
        return boxColorType.Color;
    }

    private static SelectionFieldColors GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<SelectionFieldColors>();
        }
        return instance;
    }

}

[Serializable]
public class SelectionFieldColorType {
    public Color Color;
    public LevelEditorSelectionFieldBoxColorType Type;
}

public enum LevelEditorSelectionFieldBoxColorType {
    Default,
    Error,
}
