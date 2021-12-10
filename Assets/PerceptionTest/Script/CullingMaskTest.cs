using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingMaskTest : MonoBehaviour
{
    public int cm; //set this to either 6 for smoke, or -1 for none.

    public bool smoke_on;

    private void Start()
    {
        smoke_on = true;
    }

    public void toggle_smoke_trail(bool on)
    {
        //Culling Mask takes bit values. 
        if (!on)
        {
            gameObject.GetComponent<Camera>().cullingMask = ~(1 << cm);
        }
        else
        {
            gameObject.GetComponent<Camera>().cullingMask = ~(1 << -1);
        }
        smoke_on = on;
    }
}
