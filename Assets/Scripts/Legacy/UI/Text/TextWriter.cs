using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtraStringExtensions;

public class TextWriter : MonoBehaviour {

    private const string breakLine = "(break)";

    /// <summary>
    /// Slowly write text in a existing textfield.
    /// </summary>
    /// <param name="textField"></param>
    /// <param name="textToWrite"></param>
    /// <param name="exceptionCharacters"></param>
    /// <param name="secondsToWait"></param>
    public void StartWritingText(Text textField, string textToWrite, List<ExceptionCharacters> exceptionCharacters, float secondsToWait) {
        StartCoroutine(WriteText(textField, textToWrite, exceptionCharacters, secondsToWait));
    }

    public void StopWritingText() {
        StopAllCoroutines();
    }

    private static IEnumerator WriteText(Text textField, string textToWrite, List<ExceptionCharacters> exceptionCharacters, float secondsToWait) {

        int charCounter = 0;

        List<int> breakPositions = textToWrite.AllIndexesOf(breakLine);

        while (true) {
            foreach (int breakPos in breakPositions) {
                if (charCounter != breakPos) { continue; }

                textField.text += Environment.NewLine;
                charCounter += breakLine.Length;
            }

            textField.text += textToWrite[charCounter];
            charCounter++;

            if (charCounter >= textToWrite.Length) { yield break; }

            foreach (ExceptionCharacters exceptionChar in exceptionCharacters) {
                if (textToWrite[charCounter - 1] == exceptionChar.Character) {
                    yield return new WaitForSecondsRealtime(exceptionChar.PauzeTime);
                }
            }

            yield return new WaitForSecondsRealtime(secondsToWait);;
        }
    }

    [Serializable]
    public class ExceptionCharacters {
        public char Character;
        public float PauzeTime;
    }
}
