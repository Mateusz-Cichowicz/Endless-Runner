using System.Collections;
using System.Collections.Generic;
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
    private GameManager gm;

    void Update()
    {
        velocity += gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * -movementSpeed * Time.deltaTime);
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
    }
}
