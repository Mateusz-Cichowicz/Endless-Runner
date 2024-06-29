using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    private GameManager gm;

    private int powerUpDuration = 4;
    private float verticalUpEdge = 4.5f;
    private float verticalDownEdge = -2f;
    private int horizontalSpaceEdge = 9;
    private Collider collider;
    [SerializeField]
    private List<Transform> lanePositions = new List<Transform>();
    private int lanePosition = 0;
    private Vector3 target;
    private bool targetReached = true;

    private void Start()
    {
        collider = GetComponent<Collider>();
        target = transform.position;
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (lanePosition != 0)
            {
                lanePosition -= 1;
                target = new Vector3(transform.position.x, transform.position.y, lanePositions[lanePosition].position.z);
                targetReached = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (lanePosition != 3)
            {
                lanePosition += 1;
                target = new Vector3(transform.position.x, transform.position.y, lanePositions[lanePosition].position.z);
                targetReached = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            velocity = jumpForce;
        }

        if (transform.position.y == verticalDownEdge)
        {
            isJumping = false;
        }
        if (!targetReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            if (transform.position == target) 
            {
                targetReached = true;
            }
        }
        transform.Translate(new Vector3( 0, velocity, 0)* Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -horizontalSpaceEdge, horizontalSpaceEdge), Mathf.Clamp(transform.position.y, verticalDownEdge, verticalUpEdge), transform.position.z);
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
    }
}

