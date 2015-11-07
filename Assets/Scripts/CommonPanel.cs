using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class CommonPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float AnimationSpeed;
    public GameObject Title, Content;
    public Color Highlight;

    RectTransform rect;
    Vector3 scaleTo;
    Quaternion rotationTo, rotationZero;
    Vector2 anchorMinZero, anchorMaxZero;
    Vector2 anchorMinTo, anchorMaxTo;
    Color normal, colorTo;
    Image img;
    protected bool selected;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
        normal = img.color;
        colorTo = normal;
        rect = GetComponent<RectTransform>();
        scaleTo = new Vector3(1, 1, 1);
        rotationTo = rect.localRotation;
        rotationZero = rotationTo;
        anchorMinTo = rect.anchorMin;
        anchorMaxTo = rect.anchorMax;
        anchorMinZero = anchorMinTo;
        anchorMaxZero = anchorMaxTo;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime * AnimationSpeed;
        rect.localScale = Vector3.Lerp(rect.localScale, scaleTo, t);
        rect.localRotation = Quaternion.Lerp(rect.localRotation, rotationTo, t);
        rect.anchorMin = Vector2.Lerp(rect.anchorMin, anchorMinTo, t);
        rect.anchorMax = Vector2.Lerp(rect.anchorMax, anchorMaxTo, t);

        img.color = Color.Lerp(img.color, colorTo, t);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected)
        {
            scaleTo = new Vector3(1.2f, 1.2f, 1.2f);
            colorTo = Highlight;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            scaleTo = Vector3.one;
            colorTo = normal;
        }
    }

    public void OnPointerClick()
    {
        if (!selected)
        {
            colorTo = normal;
            selected = true;
            scaleTo = new Vector3(1.2f, 1.2f, 1.2f);
            rotationTo = Quaternion.Euler(0, 0, 0);
            anchorMinTo = Vector2.zero;
            anchorMaxTo = Vector2.one;
            Content.SetActive(true);
        }
    }

    public virtual void OnBackClicked()
    {
        selected = false;
        scaleTo = Vector3.one;
        rotationTo = rotationZero;
        anchorMinTo = anchorMinZero;
        anchorMaxTo = anchorMaxZero;
        Content.SetActive(false);
    }

    public void Hide()
    {
        scaleTo = Vector3.zero;
    }

    public void Show()
    {
        scaleTo = Vector3.one;
    }
}
