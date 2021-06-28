using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSetting : MonoBehaviour
{
    [TextArea] public string text = "We may need to consider keeping Kinematic on and using our own equation to control the flight of the rocket. Leaving it up to Unity's physics seems to return different flight path even when parameters are the same";

    Rigidbody rb;
    [SerializeField] TrailRenderer tr;
    [SerializeField] AudioSource rocketLaunch;
    [SerializeField] AudioSource rocketRunning;

    bool launch = false;

    public float mass = 1.0f;
    public float drag = 0.0f;

    private float delay = 0.5f;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;
        rb.isKinematic = true; //keep the rocket from falling over

        tr = GetComponentInChildren<TrailRenderer>();
        tr.enabled = false;
    }

    private void Start()
    {
        rocketLaunch = SearchAudio("RocketLaunch");
        rocketRunning = SearchAudio("RocketEngineLoop");

        rocketLaunch.Pause();
        rocketRunning.Pause();
    }

    public void Launch(float power)
    {
        if (launch == true)
            return;
        else
        {
            launch = true;
            tr.enabled = true;
            rb.isKinematic = false;
            //rocketLaunch.Play();
            //rocketRunning.Play();
            DebugInfo("Launching with " + power + " force");
            rb.velocity = transform.up * power;
        }
    }

    //a way to keep the rocket going when velocity is lost due to time being rewinded.
    public void AdjustSpeed(float y, float z)
    {
        Vector3 var = new Vector3(0f, y, z);
        DebugInfo(var.ToString());

        rb.velocity = var;
        tr.Clear(); //because rewinding the rocket does not reset the tr. Easier to just clear it
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //DebugInfo("Hit " + collision.gameObject.name);
        if (collision.gameObject.tag == "Ground")
            rocketRunning.Pause();
    }

    private void DebugInfo(string debugText)
    {
        Debug.Log("(" + name + ") Debug: " + debugText);
    }

    private void Error(string errorText)
    {
        Debug.Log("(" + name + ") Error: " + errorText);
    }

    private AudioSource SearchAudio(string audioName)
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        AudioSource aS = new AudioSource();

        foreach (AudioSource a in audioSource)
        {
            if (a.clip.name == audioName)
                aS = a;
        }

        if (!aS)
            Error(audioName + " not found");

        return aS;
    }

    public void ChangeSpatialBlend(float input)
    {
        rocketRunning.spatialBlend = input;
    }
}
