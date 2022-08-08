using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    [SerializeField] private GameObject bullet = default;

    public void Foo()
    {
        Instantiate(bullet);
    }
}
