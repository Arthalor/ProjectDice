using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = default;

    [SerializeField] private GameObject[] healthDice = default;

    public void UpdateHealthUI()
    {
        for (int i = 0; i < 6; i++) 
        {
            healthDice[i].SetActive(i < stats.health);
        }
    }
}