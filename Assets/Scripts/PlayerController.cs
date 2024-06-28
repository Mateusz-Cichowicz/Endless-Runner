using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5;//+
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float gravity = -15.81f;
    private float velocity = 0;
    private bool isJumping = false;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Color playerColor;
    [SerializeField]
    private GameManager gm;

    private int powerUpDuration = 4;

    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        material.SetColor("_Color", playerColor);
    }

    void Update()
    {
        velocity += gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * -movementSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            velocity = jumpForce;
        }

        if (transform.position.y == -3)
        {
            isJumping = false;
        }
        transform.Translate(new Vector3( 0, velocity, 0)* Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9), Mathf.Clamp(transform.position.y, -3, 4.5f), transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObstacleController>())
        {
            gm.StopGame();
        }
        else 
        {
            PowerUpsAvailable powerup = other.GetComponent<PowerUp>().powerUpType;
            Color color = other.gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            material.SetColor("_Color", color);
            Destroy(other.gameObject);
            StartCoroutine(Pickup(powerup));
        }
    }

    IEnumerator Pickup(PowerUpsAvailable powerUp)
    {
        switch (powerUp)
        {
            case PowerUpsAvailable.IncreaseSpeed:

                movementSpeed *= 2;
                yield return new WaitForSeconds(powerUpDuration);
                movementSpeed /= 2;
                break;
            case PowerUpsAvailable.Invincibility:
                collider.isTrigger = false;
                yield return new WaitForSeconds(powerUpDuration);
                collider.isTrigger = true;
                break;
            default:
                Debug.Log("Power up type is invalid.");
                break;
        }
        material.SetColor("_Color", playerColor);
    }
}

