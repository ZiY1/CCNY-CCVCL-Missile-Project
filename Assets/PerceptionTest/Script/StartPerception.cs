using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;
using UnityEngine.UI;
using UnityEngine.Perception.Randomization.Scenarios;

public class StartPerception : MonoBehaviour
{
    public CameraManager cm;
    
    public PerceptionCamera pc;
    public PerceptionCamera pc2;
    public FixedLengthScenario fls;
    public CustomAnnotationAndMetricReporter cus;
    public CustomAnnotationAndMetricReporter cus2;

    private void Awake()
    {
        pc.enabled = false;
        pc2.enabled = false;
        cus.enabled = false;
        cus2.enabled = false;
        fls.enabled = false;
    }

    public void BeginCapture()
    {
        Debug.Log("button pressed");
        pc.enabled = true;
        pc2.enabled = true;
        cus.enabled = true;
        cus2.enabled = true;
        fls.enabled = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cm.LockOnToObject();

        if(pc.enabled)
        {
            pc.RequestCapture();
        }

        if(pc2.enabled)
        {
            pc2.RequestCapture();
        }

        if(cus.enabled)
        {
            cus.CaptureCustomData();
        }

        if(cus.enabled)
        {
            cus2.CaptureCustomData();
        }
    }
}
