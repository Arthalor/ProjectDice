using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [field: SerializeField] public List<Sprite> diceIcons { get; private set; }
    [field: SerializeField] public Transform playerTransform { get; private set; }
    [field: SerializeField] public PlayerStats playerStats { get; private set; }
}