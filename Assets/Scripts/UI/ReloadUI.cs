using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    [SerializeField] private Image magUI = default;
    [SerializeField] private Image reloadUI = default;

    [SerializeField] private PlayerAttackDie attackDie = default;
    [SerializeField] private Sprite[] sprites = default;

    public void UpdateTimer() 
    {
        magUI.fillAmount = attackDie.GetChamberProgress();
        reloadUI.fillAmount = 1 - attackDie.GetReloadProgress();
    }

    public void UpdateMagIcon()
    {
        if (attackDie.GetCurrentMag() > 0)
            magUI.sprite = sprites[attackDie.GetCurrentMag() - 1];
        else magUI.sprite = null;
    }

    public void UpdateReloadIcon() 
    {
        reloadUI.sprite = sprites[attackDie.GetMaxReloadTimer() - 1];
    }
}