using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> powerUpPrefabs;
    public float powerUpSpawnigTime;
    void Start()
    {
        StartCoroutine(SpawnObject());
    }
    IEnumerator SpawnObject()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(-3f, 2f), transform.position.z);
            GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(powerUpSpawnigTime);
        }
    }
}
