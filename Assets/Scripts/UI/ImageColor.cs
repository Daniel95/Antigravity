using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour {

    [SerializeField]
    private Color[] colors;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    public void SetColor(int i)
    {
        _image.color = colors[i];
    }

    public Color[] Colors
    {
        get { return colors; }
    }
}
