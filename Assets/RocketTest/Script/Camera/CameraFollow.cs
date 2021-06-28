using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] LaunchSetting ls;
    [SerializeField] GameObject rocket;

    public SpawnRocket sr;

    bool rocketFound;
    float timer = 0.5f;

    private void Awake()
    {
        ls = GameObject.Find("LaunchSettings").GetComponent<LaunchSetting>();
        if (!ls)
            Error("LaunchSetting Script not found");
    }

    void Start()
    {
        rocketFound = false;
        rocket = sr.GetRocket();
    }

    private IEnumerator Delay(float waitTime)
    {
        while (!ls.rocket)
        {
            yield return new WaitForSeconds(waitTime);
        }
        if (ls.rocket)
        {
            rocket = ls.rocket;
            rocketFound = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rocketFound)
            transform.LookAt(rocket.transform);
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
