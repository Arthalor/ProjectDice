using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy = default;
    [SerializeField] private List<Transform> spawns = default;

    void Start() 
    {
        InvokeRepeating("SpawnEnemy", 5f, 5f);
    }

    void SpawnEnemy() 
    {
        Instantiate(enemy, spawns[Random.Range(0,spawns.Count)].position, Quaternion.identity);
    }
}