using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public bool debugInfo;

    public Vector3 changePos;
    public Vector3 changeRot;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += changePos;
        gameObject.transform.Rotate(changeRot);

        if (debugInfo)
            Debug.Log(gameObject.transform.position);
    }
}
