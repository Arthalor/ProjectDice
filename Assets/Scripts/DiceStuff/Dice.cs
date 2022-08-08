using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Helper;

[Serializable]
public class Dice
{
    public int[] pips = new int[6] { 1, 2, 3, 4, 5, 6, };
    public bool[] weights = new bool[6] { false, false, false, false, false, false, };

    public DiceType GetDiceType() 
    {
        if (IsWeighted())
        {
            if (IsAltered())
                return DiceType.Loaded;
            else return DiceType.Weighted;
        }
        else 
        {
            if (IsAltered()) return DiceType.EyeAlterd;
            else return DiceType.Standard;
        }
    }

    public Dice()
    {

    }

    public Dice(DiceData fromData) 
    {
        pips = fromData.eyes;
        weights = fromData.weights;
    }

    public Dice(bool randomize) 
    {
        if (randomize) Randomize();
    }

    public Dice(Dice fromDice) 
    {
        pips = fromDice.pips;
        weights = fromDice.weights;
    }

    public void Randomize() 
    {
        RandomizeEyes();
        RandomizeWeights();
    }

    public void RandomizeEyes() 
    {
        for (int i = 0; i < 6; i++)
        {
            pips[i] = Random.Range(1, 7);
        }
    }

    public void RandomizeWeights() 
    {
        for (int i = 0; i < 3; i++)
        {
            weights[RandomSide()] = RandomH.Coinflip();
        }
    }

    private bool IsAltered()
    {
        for (int i = 0; i < pips.Length; i++)
        {
            if (pips[i] != i + 1) return true;
        }
        return false;
    }

    private bool IsWeighted()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i]) return true;
        }
        return false;
    }

    public int DiceRoll()
    {
        switch (GetDiceType())
        {
            case DiceType.Standard:
                return Random.Range(1, 7);
            case DiceType.EyeAlterd:
                return ReturnEyesOnSide(RandomSide());
            case DiceType.Weighted:
                return WeightedSide() + 1;
            case DiceType.Loaded:
                return ReturnEyesOnSide(WeightedSide());
        }
        throw new System.Exception();
    }

    private int RandomSide()
    {
        return Random.Range(0, 6);
    }

    private int WeightedSide()
    {
        //Weighted is ALWAYS just double the chance
        int randomIndex = RandomSide();
        if (RandomH.Coinflip()) 
        {
            if (weights[randomIndex]) return randomIndex;
            else return WeightedSide();
        }
        return randomIndex;
    }

    private int ReturnEyesOnSide(int side)
    {
        return pips[side];
    }

    public void SwapDice(ref Dice diceTwo)
    {
        Dice copy = diceTwo;
        diceTwo.pips = pips;
        diceTwo.weights = weights;

        pips = copy.pips;
        weights = copy.weights;
    }

    public enum DiceType
    {
        Standard,
        EyeAlterd,
        Weighted,
        Loaded,
    }

    public Sprite TypeSprite() 
    {
        return null;
    }
}