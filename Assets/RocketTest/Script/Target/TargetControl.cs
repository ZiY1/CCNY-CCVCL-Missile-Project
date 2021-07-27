using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TargetControl : MonoBehaviour
{
    private float xAxis;
    private float yAxis;
    private float zAxis;

    public bool resetTargetPosotion;
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        /*if (resetTargetPosotion)
        {
            this.transform.position = new Vector3(xAxis, yAxis, zAxis);
        }*/
    }

    public void GetTargetUserInput()
    {
        try
        {
            InputField input_xAxis = GameObject.Find("xAxis").GetComponent<InputField>();
            InputField input_yAxis = GameObject.Find("yAxis").GetComponent<InputField>();
            InputField input_zAxis = GameObject.Find("zAxis").GetComponent<InputField>();

            xAxis = float.Parse(input_xAxis.text);
            yAxis = float.Parse(input_yAxis.text);
            zAxis = float.Parse(input_zAxis.text);

            ResetTargetLocation();

        }
        catch (Exception ex)
        {
            Debug.Log("Please enter float or integer");
        }
    }

    void ResetTargetLocation()
    {
        this.transform.position = new Vector3(xAxis, yAxis, zAxis);
    }
}