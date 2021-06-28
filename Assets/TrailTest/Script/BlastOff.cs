using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOff : MonoBehaviour
{
    Rigidbody rd;
    float force = 100f;

    // Start is called before the first frame update
    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rd.AddForce(transform.up * force);
        }
    }
}
