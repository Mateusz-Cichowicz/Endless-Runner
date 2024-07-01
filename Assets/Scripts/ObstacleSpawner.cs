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
    [SerializeField]
    private float obstacleRotationYValue = 0;
    void Start() 
    {
        StartCoroutine(SpawnObject());
    }
    IEnumerator  SpawnObject() 
    {
        GameObject tempGameObject;
        while (true)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

            tempGameObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
            tempGameObject.transform.eulerAngles = new Vector3(0,obstacleRotationYValue,0);

            yield return new WaitForSeconds(obstacleSpawnigTime);
        }
    }
}
