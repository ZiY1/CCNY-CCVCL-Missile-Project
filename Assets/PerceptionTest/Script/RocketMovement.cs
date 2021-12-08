using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float initVelocity;
    Rigidbody rb;
    bool spin = false;
    public float spinValue = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startSpin();
    }

    public void Launch()
    {
        rb.velocity = transform.forward * initVelocity;
    }

    public void startSpin()
    {
        spin = true;
    }

    public void stopSpin()
    {
        spin = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Missile hit " + collision.gameObject.name);
        stopSpin();
    }

    private void Update()
    {
        if(spin)
            gameObject.transform.Rotate(xAngle:0, yAngle:0, zAngle:spinValue);
    }
}
