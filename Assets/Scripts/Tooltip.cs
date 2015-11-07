using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Tooltip : MonoBehaviour
{
    public float FadeSpeed;

    Text _text;
    RectTransform _rect;
    Vector2 _positionZero;
    CanvasGroup _canvasGroup;
    bool _visible;

    public bool Visible
    {
        get
        {
            return _visible;
        }

        set
        {
            _visible = false;
        }
    }

    public Vector2 Position
    {
        get
        {
            return _rect.anchorMin;
        }

        set
        {
            //_rect.anchorMin = value;
            //_rect.anchorMax = value;
            _rect.anchoredPosition = _positionZero;
            _rect.position = value;
        }
    }

    void Start()
    {
        _text = GetComponentInChildren<Text>();
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _positionZero = _rect.anchoredPosition;

        Visible = false;
    }

    void Update()
    {
        if (Visible)
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 1f, Time.deltaTime * FadeSpeed);
        }
        else
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 0f, Time.deltaTime * FadeSpeed);
        }
    }

    public void SetTooltip(string descr)
    {
        _text.text = descr;
    }
}
