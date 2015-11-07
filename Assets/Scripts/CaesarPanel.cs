using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class CaesarPanel : CommonPanel, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick();
        Manager.Instance.OnCaesarSelected(true);
    }

    public override void OnBackClicked()
    {
        base.OnBackClicked();
        Manager.Instance.OnCaesarSelected(false);
    }
}
