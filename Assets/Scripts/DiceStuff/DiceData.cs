using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDice", menuName = ("ScriptableObject/Dice"), order = 3)]
public class DiceData : ScriptableObject
{
    public int[] eyes = new int[6] { 1, 2, 3, 4, 5, 6 };
    public bool[] weights = new bool[6] { false, false, false, false, false, false };

    private void OnValidate()
    {
        CapValues();
        if (eyes.Length > 6) eyes = ShortenArray(eyes);
        if (weights.Length > 6) weights = ShortenArray(weights);
    }

    private int[] ShortenArray(int[] array) 
    {
        int[] retValue = new int[6];
        for (int i = 0; i < 6; i++) 
        {
            retValue[i] = array[i];
        }
        return retValue;
    }

    private bool[] ShortenArray(bool[] array) 
    {
        bool[] retValue = new bool[6];
        for (int i = 0; i < 6; i++) 
        {
            retValue[i] = array[i];
        }
        return retValue;
    }

    private void CapValues() 
    {
        for(int i = 0; i < 6; i++)
        {
            eyes[i] = Mathf.Clamp(eyes[i], 1, 6);
        }
    }
}