using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaunchSetting : MonoBehaviour
{
    public RocketSetting rs;
    public SpawnRocket sr;
    public GameObject rocket;
    public GameObject spawn;
    public GameObject sphere;

    public InputField powerInput;
    public InputField angleInput;
    public Button launchButton;

    Transform sphereTransform;
    Vector3 sphereVec;

    [SerializeField] float power;
    [SerializeField] bool launch = false;

    private void Start()
    {
        rocket = sr.rocket;
        spawn = sr.spawn;
        if (rocket)
        {
            rs = rocket.GetComponent<RocketSetting>();
            if (!rs)
                Error("No Rocket Settings Found");
        }
        else
            Error("No Rocket found");

        sphereTransform = sphere.transform;
        sphereVec = sphereTransform.eulerAngles;

    }

    public void LaunchButton()
    {
        launch = true;
        rs.Launch(power);
    }

    public void ChangeAngle(float newAngle)
    {
        sphereVec = new Vector3(sphereVec.x, sphereVec.y, newAngle);
        sphereTransform.eulerAngles = sphereVec;
    }

    public void ChangePower(float newPower)
    {
        power = newPower;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (!launch)
        {
            rocket.transform.position = spawn.transform.position;
            rocket.transform.localEulerAngles = spawn.transform.eulerAngles;
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
