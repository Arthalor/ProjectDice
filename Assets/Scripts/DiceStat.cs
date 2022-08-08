using System;
using UnityEngine;

[Serializable]
public class DiceStat : MonoBehaviour
{
    public Dice dice;
    public int value { get; private set; }

    public void Start()
    {
        //dice = new Dice();
        RollStat();
    }

    public void RollStat()
    {
        value = dice.DiceRoll();
    }

    public void NewDice()
    {
        dice = new Dice();
        RollStat();
    }
}