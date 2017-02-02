using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleBar : MonoBehaviour {

    [SerializeField]
    private readonly Vector2 maxScalePropotion = new Vector2(1,1);

    private Vector2 _maxScale;

    private void Awake()
    {
        _maxScale = VectorMath.MultiplyV2(transform.localScale, maxScalePropotion);
    }

    /// <summary>
    /// Rescales the object.
    /// </summary>
    /// <param name="scale"></param>
    public void Rescale(Vector2 scale)
    {
        transform.localScale = scale;
    }

    public void ScaleWidth(float width)
    {
        Vector2 scale = new Vector2(width, transform.localScale.y);

        Rescale(scale);
    }

    public void ScaleHeight(float height)
    {
        Vector2 scale = new Vector2(transform.localScale.x, height);
        Rescale(scale);
    }

    /// <summary>
    /// Scales the images with propotion(percentage / 100).
    /// </summary>
    /// <param name="scalePropotion"></param>
    public void ScalePropotion(Vector2 scalePropotion)
    {
        Vector2 scale = VectorMath.MultiplyV2(_maxScale, scalePropotion);

        Rescale(scale);
    }

    public void ScaleWidthPropotion(float widthPropotion)
    {
        float scaleX = _maxScale.x * widthPropotion;
        ScaleWidth(scaleX);
    }

    public void ScaleHeightPropotion(float heightPropotion)
    {
        float scaleY = _maxScale.y * heightPropotion;

        ScaleHeight(scaleY);
    }
}