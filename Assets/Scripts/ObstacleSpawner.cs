using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstaclePrefabs;
    public float obstacleSpawnigTime;
    void Start() 
    {
        StartCoroutine(SpawnObject());
    }
    IEnumerator  SpawnObject() 
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(-3f, 2f), transform.position.z);
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

            Instantiate(prefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(obstacleSpawnigTime);
        }
    }
}
