using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;
using UnityEngine.UI;
using UnityEngine.Perception.Randomization.Scenarios;

public class FakeStartPerception: MonoBehaviour
{
    public List<Camera> camList;

    public GameObject missile_obj;
    public FixedLengthScenario fls;

    bool beginCapture;

    private void Awake()
    {
        fls.enabled = false;
        beginCapture = false;

        foreach(Camera cam in camList)
        {
            cam.GetComponent<PerceptionCamera>().enabled = false;
            cam.GetComponent<CustomAnnotationAndMetricReporter>().enabled = false;
        }
    }

    public void BeginCapture()
    {
        Debug.Log("button pressed");

        fls.enabled = true;

        foreach (Camera cam in camList)
        {
            cam.GetComponent<PerceptionCamera>().enabled = true;
            cam.GetComponent<CustomAnnotationAndMetricReporter>().enabled = true;
        }

        missile_obj.GetComponent<RocketMovement>().Launch();

        beginCapture = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(Camera cam in camList)
            cam.GetComponent<Camera>().transform.LookAt(missile_obj.transform);

        if (beginCapture)
        {
            foreach (Camera cam in camList)
            {
                cam.GetComponent<PerceptionCamera>().RequestCapture();
                cam.GetComponent<CustomAnnotationAndMetricReporter>().CaptureCustomData();
            }
        }
    }
}
