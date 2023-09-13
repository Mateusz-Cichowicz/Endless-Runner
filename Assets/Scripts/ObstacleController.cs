using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    
    private float speed = 4f;//+
    private GameManager m_GameManager;
    private void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= -12)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        m_GameManager.AddPoints();
    }
}
