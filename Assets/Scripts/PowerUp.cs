using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerUpsAvailable
{
    IncreaseSpeed,
    Invincibility
}
public class PowerUp : MonoBehaviour
{
    private float speed = 4f;

    public PowerUpsAvailable powerUpType;
    
    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
