using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;
using UnityEngine.UI;
using UnityEngine.Perception.Randomization.Scenarios;

public class StartPerception : MonoBehaviour
{
    public CameraManager cm;

    public List<Camera> camList;
    
    public PerceptionCamera pc;
    public PerceptionCamera pc2;
    public PerceptionCamera pc3;
    public PerceptionCamera pc4;
    public PerceptionCamera pc5;

    public FixedLengthScenario fls;

    public CustomAnnotationAndMetricReporter cus;
    public CustomAnnotationAndMetricReporter cus2;
    public CustomAnnotationAndMetricReporter cus3;
    public CustomAnnotationAndMetricReporter cus4;
    public CustomAnnotationAndMetricReporter cus5;

    private void Awake()
    {
        fls.enabled = false;

        pc.enabled = false;
        pc2.enabled = false;
        pc3.enabled = false;
        pc4.enabled = false;
        pc5.enabled = false;
        cus.enabled = false;
        cus2.enabled = false;
        cus3.enabled = false;
        cus4.enabled = false;
        cus5.enabled = false;
        fls.enabled = false;
    }

    public void BeginCapture()
    {
        Debug.Log("button pressed");
        if (cm.cam[0].gameObject.activeSelf)
        {
            pc.enabled = true;
            cus.enabled = true;
        }
        if (cm.cam[1].gameObject.activeSelf)
        {
            pc2.enabled = true;
            cus2.enabled = true;
        }
        if (cm.cam[2].gameObject.activeSelf)
        {
            pc3.enabled = true;
            cus3.enabled = true;
        }
        if (cm.cam[3].gameObject.activeSelf)
        {
            pc4.enabled = true;
            cus4.enabled = true;
        }
        //if (cm.cam[4].gameObject.activeSelf)
        //{
        //    pc5.enabled = true;
        //    cus5.enabled = true;
        //}
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
        if (pc3.enabled)
        {
            pc3.RequestCapture();
        }
        if (pc4.enabled)
        {
            pc4.RequestCapture();
        }
        if (pc5.enabled)
        {
            pc5.RequestCapture();
        }


        if (cus.enabled)
        {
            cus.CaptureCustomData();
        }
        if(cus2.enabled)
        {
            cus2.CaptureCustomData();
        }
        if (cus3.enabled)
        {
            cus3.CaptureCustomData();
        }
        if (cus4.enabled)
        {
            cus4.CaptureCustomData();
        }
        if (cus5.enabled)
        {
            cus5.CaptureCustomData();
        }
    }
}
