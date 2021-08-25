using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;
using UnityEngine.UI;
using UnityEngine.Perception.Randomization.Scenarios;

public class StartPerception : MonoBehaviour
{
    public PerceptionCamera pc;
    public FixedLengthScenario fls;
    public CustomAnnotationAndMetricReporter cus;

    private void Awake()
    {
        pc.enabled = false;
        cus.enabled = false;
        fls.enabled = false;
    }

    public void BeginCapture()
    {
        Debug.Log("button pressed");
        pc.enabled = true;
        cus.enabled = true;
        fls.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.enabled)
        {
            pc.RequestCapture();
        }
    }
}
