using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    public float speed = 4f;
    private GameManager m_GameManager;
    [SerializeField]
    private Space currentSpace;
    [SerializeField]
    private Vector3 direction;
    private void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, currentSpace);
        
        if (transform.position.x <= -18 || transform.position.x >= 18)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        m_GameManager.AddPoints();
    }
}
