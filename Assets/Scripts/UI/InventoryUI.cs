using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerStats pStats = default;
    [SerializeField] private Image image = default;
    protected Dice diceCopy;

    [SerializeField] private UIManager manager = default;

    public virtual void LoadDice() 
    {
        diceCopy = pStats.diceInventory;
    }

    public virtual void UploadDice()
    {
        pStats.diceInventory = diceCopy;
    }

    public virtual void ReplaceDice(Dice newDice)
    {
        diceCopy = newDice;
    }

    public Dice GetDiceCopy() 
    {
        return diceCopy;
    }

    public void LoadImage(Sprite[] sprites)
    {
        image = transform.GetChild(1).GetComponent<Image>();
        image.sprite = sprites[(int)diceCopy.GetDiceType()];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.HoverUI(diceCopy);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manager.HoverUIEnd();
    }
}