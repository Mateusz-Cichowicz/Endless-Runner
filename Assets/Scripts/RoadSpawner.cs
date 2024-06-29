using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> roadPrefabs;
    private GameObject roadTile;

    private void Start()
    {
        roadTile = SpawnObject();
    }
    void Update() 
    {
        if (roadTile.transform.position.x <= 12.5) 
        {
            roadTile = SpawnObject();
        }
    }
    private GameObject SpawnObject()
    {
        GameObject tempGameObject;
        GameObject prefab;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Random.Range(0, 100) < 80)
        {
            prefab = roadPrefabs[0];
        }
        else 
        {
            prefab = roadPrefabs[1];
        }

        tempGameObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        tempGameObject.transform.rotation = transform.rotation;
        return tempGameObject;
    }
}
