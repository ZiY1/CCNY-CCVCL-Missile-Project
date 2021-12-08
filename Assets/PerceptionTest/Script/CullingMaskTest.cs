using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingMaskTest : MonoBehaviour
{
    public int cm; //set this to either 6 for smoke, or -1 for none.

    void Start()
    {
        //Culling Mask takes bit values. 
        gameObject.GetComponent<Camera>().cullingMask = ~(1 << cm);

    }
}
