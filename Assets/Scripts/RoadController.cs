using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private float speed = 4f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.x <= -19.5)
        {
            Destroy(gameObject);
        }
    }
}
