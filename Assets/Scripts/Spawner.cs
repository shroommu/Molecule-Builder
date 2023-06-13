using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;

    public void Spawn()
    {
        Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
