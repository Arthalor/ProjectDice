using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] public Canvas canvas = default;
    [SerializeField] private RectTransform rTrans = default;
    [SerializeField] private CanvasGroup group = default;

    public Dice diceInfo;
    public RectTransform origin;

    public void OnBeginDrag(PointerEventData eventData)
    {
        origin = (RectTransform)transform.parent;
        transform.SetParent(canvas.transform);
        group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(origin);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -15);
        }
        group.blocksRaycasts = true;
    }
}