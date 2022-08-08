using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour
{
    [SerializeField] private Image[] eyes = default;
    [SerializeField] private Image[] weights = default;

    [SerializeField] private Sprite[] eyeSprites = default;
    [SerializeField] private Sprite[] weightSprite = default;

    public void UpdateSprites(Dice dice) 
    {
        for (int i = 0; i < 6; i++) 
        {
            eyes[i].sprite = eyeSprites[dice.pips[i] - 1];
            weights[i].sprite = weightSprite[0];
        }
    }
}