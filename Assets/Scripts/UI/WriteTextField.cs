using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteTextField : MonoBehaviour {

    [SerializeField]
    private float secondsToWait = 0.03f;

    private Text _textField;

    private List<char> _savedToChars = new List<char>();

    private TextWriter _textWriter;

    private void Start()
    {
        _textWriter = GetComponent<TextWriter>();
        _textField = GetComponent<Text>();

        foreach (char character in _textField.text)
        {
            _savedToChars.Add(character);
        }

        _textField.text = "";

        _textWriter.StartTypingText(_textField, _savedToChars, secondsToWait);
    }
}
