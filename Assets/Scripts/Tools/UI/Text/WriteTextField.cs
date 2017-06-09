using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteTextField : MonoBehaviour {

    [SerializeField] private float secondsToWait = 0.3f;
    private bool finishIsFastForward = false;

    [SerializeField] private List<TextWriter.ExceptionCharacters> exceptionCharacters = new List<TextWriter.ExceptionCharacters>();

    private Text textField;
    private TextWriter textWriter;

    public void StartWriting(string text, System.Action onFinishedWriting) {
        textField.text = "";
        textWriter.StartWritingText(textField, text, exceptionCharacters, secondsToWait, onFinishedWriting);
    }

    public void StopWriting() {
        textWriter.StopWritingText();
    }

    public void FinishWriting() {
        if (finishIsFastForward) {
            textWriter.FastForward();
        } else {
            textWriter.FinishWritingText();
        }
    }

    private void Awake() {
        if (textField == null) {
            textField = GetComponent<Text>();
        }
        if (textWriter == null) {
            textWriter = GetComponent<TextWriter>();
        }
    }

    private void OnDisable() {
        if (textWriter != null) {
            textWriter.StopWritingText();
        }
    }

    private void OnDestroy() {
        if (textWriter != null) {
            textWriter.StopWritingText();
        }
    }
}