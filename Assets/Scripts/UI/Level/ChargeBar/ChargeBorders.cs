using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBorders : MonoBehaviour {

    [SerializeField]
    private GameObject border;

    private Canvas _canvas;

    private Image _barImage;

    private readonly List<GameObject> _borders = new List<GameObject>();

    private void Awake()
    {
        _barImage = GetComponent<Image>();
        _canvas = FindObjectOfType<Canvas>();
    }

    public void SetBorderAmount(int amount)
    {
        if (amount > _borders.Count)
        {
            _borders.Add(Instantiate(border,
                new Vector3(_barImage.rectTransform.position.x - _barImage.rectTransform.rect.width / 2 * transform.parent.localScale.x *_canvas.scaleFactor, transform.position.y,
                    transform.position.z), new Quaternion(), transform.parent.parent));
        }
        else {
            if (amount != _borders.Count)
            {
                for (int i = _borders.Count - 1; i >= amount; i--)
                {
                    Destroy(_borders[i]);
                    _borders.RemoveAt(i);
                }
            }
        }
    }
}
