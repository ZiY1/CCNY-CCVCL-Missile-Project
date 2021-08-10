using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchOnClick : MonoBehaviour
{
    // TODO: Add more missiles when ready
    public GameObject missile1;
    public GameObject missile2;

    private Projectile2 p1;
    private Projectile2 p2;


    public void OnClick()
    {
        if (missile1.activeSelf)
        {
            //Debug.Log("M1 active");
            p1 = (Projectile2)missile1.GetComponent(typeof(Projectile2));
            p1.Launch();
        }
        else if (missile2.activeSelf)
        {
            //Debug.Log("M2 active");
            p2 = (Projectile2)missile2.GetComponent(typeof(Projectile2));
            p2.Launch();
        }
    }
}

