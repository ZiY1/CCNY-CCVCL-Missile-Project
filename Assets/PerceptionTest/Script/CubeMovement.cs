using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += pos;
        Debug.Log(gameObject.transform.position);
    }
}
