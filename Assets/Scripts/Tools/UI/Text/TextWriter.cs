using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour {

    [Serializable]
    public class ExceptionCharacters {
        public char Character;
        public float PauzeTime;
    }

    private string textToWrite;
    private Text textField;
    private Action onFinishedWriting;
    private bool fastForwarding;

    /// <summary>
    /// Slowly write text in a existing textfield.
    /// </summary>
    /// <param name="textField"></param>
    /// <param name="textToWrite"></param>
    /// <param name="exceptionCharacters"></param>
    /// <param name="secondsToWait"></param>
    public void StartWritingText(Text textField, string textToWrite, List<ExceptionCharacters> exceptionCharacters, float secondsToWait, Action onFinishedWriting = null) {
        this.textToWrite = textToWrite;
        this.textField = textField;
        this.onFinishedWriting = onFinishedWriting;
        StartCoroutine(WriteText(textField, textToWrite, exceptionCharacters, secondsToWait));
    }

    public void StopWritingText() {
        StopAllCoroutines();
    }

    public void FastForward() {
        fastForwarding = true;
    }

    public void FinishWritingText() {
        StopAllCoroutines();

        fastForwarding = false;

        textField.text = textToWrite;

        if (onFinishedWriting != null) {
            onFinishedWriting.Invoke();
        }
    }

    private IEnumerator WriteText(Text textField, string textToWrite, List<ExceptionCharacters> exceptionCharacters, float defaultCharacterInterval) {
        int charCounter = 0;
        float timeTillNextCharacter = 0.0f;

        while (charCounter < textToWrite.Length - 1) {
            timeTillNextCharacter -= Time.deltaTime;

            while (timeTillNextCharacter <= 0.0f) {
                float characterInterval = defaultCharacterInterval;

                ExceptionCharacters intervalException = exceptionCharacters.Find(x => x.Character == textToWrite[charCounter]);
                if (intervalException != null) {
                    characterInterval = intervalException.PauzeTime;
                }

                textField.text += textToWrite[charCounter];
                charCounter++;

                timeTillNextCharacter += characterInterval;
            }

            yield return new WaitForEndOfFrame();

            if (fastForwarding) {
                for (int i = 0; i < 4; i++) {
                    if (charCounter < textToWrite.Length) {
                        textField.text += textToWrite[charCounter];
                        charCounter++;
                    }
                }

                yield break;
            }
        }

        FinishWritingText();
    }

}
