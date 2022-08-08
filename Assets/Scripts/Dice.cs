using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Dice
{
    public int[] eyes = new int[6] { 1, 2, 3, 4, 5, 6, };
    public int[] weights = new int[6] { 1, 1, 1, 1, 1, 1, };

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
        eyes = fromData.eyes;
        weights = fromData.weights;
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
            eyes[i] = Random.Range(1, 7);
        }
    }

    public void RandomizeWeights() 
    {
        for (int i = 0; i < 3; i++)
        {
            weights[RandomSide()] = Random.Range(1, 7);
        }
    }

    private bool IsAltered()
    {
        for (int i = 0; i < eyes.Length; i++)
        {
            if (eyes[i] != i + 1) return true;
        }
        return false;
    }

    private bool IsWeighted()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] != 1) return true;
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

    private int SumWeights()
    {
        int sum = 0;
        foreach (int w in weights)
        {
            sum += w;
        }
        return sum;
    }

    private int RandomSide()
    {
        return Random.Range(0, 6);
    }

    private int WeightedSide()
    {
        int weightSum = SumWeights();
        int sideRoll = Random.Range(0, weightSum);
        int weightedIndex = 0;
        for (int i = 0; i < weightSum; i++)
        {
            int indexRange = weights[weightedIndex] - 1;
            for (int j = indexRange; j >= 0; j--)
            {
                if (sideRoll == (i + j))
                {
                    Debug.Log(weightedIndex);
                    return weightedIndex;
                }
            }
            i += indexRange;
            weightedIndex++;
        }
        Debug.Log("WEIGHTS NOT WORKING");
        return weightedIndex;
    }

    private int ReturnEyesOnSide(int side)
    {
        return eyes[side];
    }

    public void SwapDice(ref Dice diceTwo)
    {
        Dice copy = diceTwo;
        diceTwo.eyes = eyes;
        diceTwo.weights = weights;

        eyes = copy.eyes;
        weights = copy.weights;
    }

    public enum DiceType
    {
        Standard,
        EyeAlterd,
        Weighted,
        Loaded,
    }
}