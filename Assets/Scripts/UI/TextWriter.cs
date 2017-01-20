using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    /// <summary>
    /// Slowly write text in a existing textfield.
    /// </summary>
    /// <param name="textField"></param>
    /// <param name="charList"></param>
    public void StartTypingText(Text textField, List<char> charList, float secondsToWait)
    {
        StartCoroutine(WriteText(textField, charList, secondsToWait));
    }

    private IEnumerator WriteText(Text texfield, List<char> charList, float secondsToWait)
    {
        int charCounter = 0;

        while (true)
        {
            texfield.text += charList[charCounter];
            charCounter++;

            if (charCounter >= charList.Count) 
                yield break;

            for (int i = 0; i < secondsToWait; i++)
            {
                yield return new WaitForSecondsRealtime(secondsToWait);
            }
        }
    }
}
