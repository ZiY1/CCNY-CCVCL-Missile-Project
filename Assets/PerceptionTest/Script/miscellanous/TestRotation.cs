using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    public Transform tran;
    public Quaternion q;
    public Quaternion qu;
    // Start is called before the first frame update
    void Start()
    { 
        tran = gameObject.transform;
        //q.eulerAngles = new Vector3(100,100,100);
        qu = new Quaternion(0.7f, -0.1f, -0.1f, 0.7f); //<replace theses digits
    }

    // Update is called once per frame
    void Update()
    {
        tran.eulerAngles = qu.eulerAngles;
        Debug.Log("Transform.EulerAngle: " + gameObject.transform.eulerAngles);
        Debug.Log("Quaternion.EulerAngle: " + qu.eulerAngles);
        Debug.Log("Quaternion: " + Quaternion.Euler(gameObject.transform.eulerAngles));
    }
}
