using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class VigenerePanel : CommonPanel, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick();
        Manager.Instance.OnVigenereSelected(true);
    }

    public override void OnBackClicked()
    {
        base.OnBackClicked();
        Manager.Instance.OnVigenereSelected(false);
    }
}
