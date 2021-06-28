using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [TextArea] public string text = "Settings for where the camera will be. The cameraPlacement will be however many places we want the camera to be, such as on the ground, bird's eye view, following the rocket, etc.";

    public Camera camera;
    public Transform[] cameraPlacement;
    [SerializeField] LaunchSetting ls;
    [SerializeField] RocketSetting rs;
    int num = 0;

    private void Awake()
    {
        ls = GameObject.Find("LaunchSettings").GetComponent<LaunchSetting>();
        if (!ls)
        {
            Error("LaunchSetting script not found");
        }
    }

    private void Start()
    {
        rs = ls.rocket.GetComponent<RocketSetting>();
        if (!rs)
            Error("RocketSetting Script not found");

        ChangeCameraPlacement(0);
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = cameraPlacement[num].transform.position;
        camera.transform.eulerAngles = cameraPlacement[num].transform.eulerAngles;
    }

    public void ChangeCameraPlacement(int num)
    {
        this.num = num;
        ChangeSpatialBlend(num);
    }

    private void ChangeSpatialBlend(int num)
    {
        if (num == 0)
        {
            rs.ChangeSpatialBlend(1f);
        }
        else if (num == 1)
        {
            rs.ChangeSpatialBlend(0.7f);
        }
        else if (num == 2)
        {
            rs.ChangeSpatialBlend(0f);
        }
        else
        {
            Error(num + " is not a valid button");
        }
    }

    private void Error(string errorText)
    {
        Debug.Log("(" + name + ") Error: " + errorText);
    }

    private void DebugInfo(string debugText)
    {
        Debug.Log("(" + name + ") Debug: " + debugText);
    }
}
