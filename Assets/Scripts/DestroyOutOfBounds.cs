using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float boundRange = 20.0f;

    // Update is called once per frame
    void Update()
    {
        // When projectile leaves the screen, destroy projectile
        if (transform.position.x > boundRange || transform.position.x < -boundRange)
        {
            Destroy(gameObject);
        } else if (transform.position.z > boundRange || transform.position.z < -boundRange)
        {
            Destroy(gameObject);
        }
    }
}
