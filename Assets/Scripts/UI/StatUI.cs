using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : InventoryUI 
{
    [SerializeField] private DiceStat dStat = default;

    public override void LoadDice()
    {
        diceCopy = dStat.dice;
    }

    public override void UploadDice()
    {
        dStat.dice = diceCopy;
    }
}