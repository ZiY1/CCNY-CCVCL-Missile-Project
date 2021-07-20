using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTemp : MonoBehaviour
{
    public GameObject rocket;

    float timer = 0.5f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(rocket.transform);
    }
}
