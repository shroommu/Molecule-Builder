using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;

    public int prefabNumber = 1;

    public void Spawn()
    {
        GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        obj.name = prefab.name + " Copy " + prefabNumber.ToString();
        prefabNumber++;
    }
}
