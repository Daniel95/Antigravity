﻿using IoCPlus;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButtonInput : View {

    [SerializeField] private KeyCode keyInput;

    private Button button;
    private Coroutine updateKeyInputCoroutine;

    public override void Initialize() {
        updateKeyInputCoroutine = StartCoroutine(UpdateKeyInput());
    }

    public override void Dispose() {
        StopCoroutine(UpdateKeyInput());
    }

    private void Awake () {
        button = GetComponent<Button>();
	}

    private IEnumerator UpdateKeyInput() {
        while (true) {
            if (Input.GetKeyDown(keyInput)) {
                button.onClick.Invoke();
            }
            yield return null;
        }
    }
}
