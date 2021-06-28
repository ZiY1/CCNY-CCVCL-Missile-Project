using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketInfo : MonoBehaviour
{

    public float speed;
    public float angularSpeed;
    Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        speed = r.velocity.magnitude;
        angularSpeed = r.angularVelocity.magnitude;
    }
}
