using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public Vector3 changePos;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += changePos;
        Debug.Log(gameObject.transform.position);
    }
}
