using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleOutput : MonoBehaviour
{
    public GameObject target01;
    public GameObject target02;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(string.Format("Parent Target: {0}, \nChild Target: {0}", target01.transform.eulerAngles, target02.transform.eulerAngles));
        //Debug.Log(target01.name+  ": " + target01.transform.eulerAngles + "\n" + target02.name + ": " + target02.transform.eulerAngles);
    }
}
