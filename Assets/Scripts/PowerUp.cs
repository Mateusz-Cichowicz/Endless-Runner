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
    private int powerUpDuration = 4;
    private float speed = 4f;//+

    public PowerUpsAvailable powerUpType;
    
    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }
    IEnumerator Pickup(Collider player) 
    {
        switch (powerUpType)
        {
            case PowerUpsAvailable.IncreaseSpeed:

                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.movementSpeed = 10; // change it into variables
                yield return new WaitForSeconds(powerUpDuration);
                playerController.movementSpeed = 5;
                break;
            case PowerUpsAvailable.Invincibility:
                player.isTrigger = false;
                yield return new WaitForSeconds(powerUpDuration);
                player.isTrigger = true;
                break;
            default:
                Debug.Log("Power up type is invalid.");
                break;
        }

        Destroy(gameObject);
    }
}
