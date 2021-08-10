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

    bool perceptionOn = false;

    public void BeginCapture()
    {
        Debug.Log("button pressed");
        perceptionOn = true;
        fls.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(perceptionOn)
        {
            pc.RequestCapture();
        }
    }
}
