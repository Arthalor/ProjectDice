using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) 
        {
            if (rectTransform.childCount < 2) return; 

            RectTransform newChildThisSlot = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform originalChildThisSlot = (RectTransform)rectTransform.GetChild(1);
            RectTransform thisSlot = rectTransform;
            RectTransform otherSlot = newChildThisSlot.GetComponent<DragDrop>().origin;

            InventoryUI thisSlotUI = thisSlot.GetComponent<InventoryUI>();
            InventoryUI otherSlotUI = otherSlot.GetComponent<InventoryUI>();

            Dice thisSlotDice = thisSlotUI.GetDiceCopy();
            Dice otherSlotDice = otherSlotUI.GetDiceCopy();

            thisSlotUI.ReplaceDice(otherSlotDice);
            otherSlotUI.ReplaceDice(thisSlotDice);

            originalChildThisSlot.SetParent(newChildThisSlot.GetComponent<DragDrop>().origin);
            originalChildThisSlot.anchoredPosition = new Vector2(0, -15);


            Debug.Log(newChildThisSlot.name + " : " + rectTransform);
            newChildThisSlot.SetParent(rectTransform);
            newChildThisSlot.anchoredPosition = new Vector2(0, -15);

            newChildThisSlot.GetComponent<DragDrop>().canvas.GetComponent<UIManager>().RefreshUI(); 
        }
    }
}