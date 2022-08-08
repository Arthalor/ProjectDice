using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public DiceStat damage { get; private set; }
    [field: SerializeField] public DiceStat range { get; private set; }
    [field: SerializeField] public DiceStat magSize { get; private set; }
    [field: SerializeField] public DiceStat magReloadTime { get; private set; }
    [field: SerializeField] public DiceStat moveSpeed { get; private set; }
    [field: SerializeField] public DiceStat fireRate { get; private set; }
    [field: SerializeField] public DiceStat bulletSpeed { get; private set; }

    public Dice diceInventory { get; set; }

    [SerializeField] private DiceData initialInventory;
    [SerializeField] private DiceData initialDamage;
    [SerializeField] private DiceData initialReload;

    private void Awake()
    {
        health = 6;
        diceInventory = new Dice(initialInventory);
        damage.dice = new Dice(initialDamage);
        magReloadTime.dice = new Dice(initialReload);
    }

    private void Start()
    {
        diceInventory = new Dice(initialInventory);
    }

    private void Update()
    {

    }

    public void TakeDamage() 
    {
        health -= 1;
        if (health <= 0) Die();
    }

    private void Die() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}