using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteTextField : MonoBehaviour {

    [SerializeField] private float secondsToWait = 0.03f;

    [SerializeField] private List<TextWriter.ExceptionCharacters> exceptionCharacters = new List<TextWriter.ExceptionCharacters>();

    [SerializeField] private bool writeOnEnable = true;

    private Text textField;

    private TextWriter textWriter;

    private void OnEnable() {

        if(!writeOnEnable) { return; }

        if (textField == null) {
            textField = GetComponent<Text>(); 
        }
        if (textWriter == null) {
            textWriter = GetComponent<TextWriter>();
        }

        StartWrite(textField.text);
    }

    public void StartWrite(string text) {
        if (textField == null) {
            textField = GetComponent<Text>(); 
        }
        if (textWriter == null) {
            textWriter = GetComponent<TextWriter>();
        }

        textField.text = "";

        textWriter.StartWritingText(textField, text, exceptionCharacters, secondsToWait);
    }

    public void StopWrite() {
        textWriter.StopWritingText();
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
