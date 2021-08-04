using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    double timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        Debug.Log("DeltaTime: " + Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer += Time.deltaTime);
    }
}
