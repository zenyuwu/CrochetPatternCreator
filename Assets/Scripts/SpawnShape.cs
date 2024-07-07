using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShape : MonoBehaviour
{
    [SerializeField] GameObject shape;

    public void Spawn()
    {
        Instantiate(shape, transform.position, Quaternion.identity);
    }
}
