using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Projectile2 : MonoBehaviour
{

    // Launch variables

    /* Using the SerializeField attribute causes Unity to serialize any private variable. 
     * This doesn't apply to static variables and properties in C#. You use the SerializeField 
     * attribute when you need your variable to be private but also want it to show up in the Editor
     */
    [SerializeField] private Transform TargetObject;
    private float LaunchAngle;


    // State
    private bool bTargetReady;
    private bool bTouchingGround;


    // cache

    /* Control of an object's position through physics simulation. */
    private Rigidbody rigid;

    /* A Vector3 has a 3D direction, like a xyz point in a 3D space */
    private Vector3 initialPosition;

    /* Quaternions are used to represent rotations. */
    private Quaternion initialRotation;

    //spin stuff
    bool spin = false;
    float spinValue = 10.0f;
    GameObject missileBody;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bTargetReady = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        missileBody = gameObject.transform.GetChild(1).gameObject;
        Debug.Log(missileBody.name);
        // Testing
        //Debug.Log("initPos: " + initialPosition);
        //Debug.Log("initRot: " + initialRotation);
    }



    // returns the distance between the red dot and the TargetObject's y-position
    // this is a very little offset considered the ranges in this demo so it shouldn't make a big difference.
    // however, if this code is tested on smaller values, the lack of this offset might introduce errors.
    // to be technically accurate, consider using this offset together with the target platform's y-position.
    float GetPlatformOffset()
    {
        float platformOffset = 0.0f;
        // 
        //          (SIDE VIEW OF THE PLATFORM)
        //
        //                   +------------------------- Mark (Sprite)
        //                   v
        //                  ___                                          -+-
        //    +-------------   ------------+         <- Platform (Cube)   |  platformOffset
        // ---|--------------X-------------|-----    <- TargetObject     -+-
        //    +----------------------------+
        //

        // we're iterating through Mark (Sprite) and Platform (Cube) Transforms. 
        foreach (Transform childTransform in TargetObject.GetComponentsInChildren<Transform>())
        {
            // take into account the y-offset of the Mark gameobject, which essentially
            // is (y-offset + y-scale/2) of the Platform as we've set earlier through the editor.
            if (childTransform.name == "Mark")
            {
                platformOffset = childTransform.localPosition.y;
                break;
            }
        }
        return platformOffset;
    }


    // Launches the object towards the TargetObject with a given LaunchAngle
    public void Launch()
    {
        // Get the launch angle from the user 

        try
        {
            LaunchAngle = GameObject.Find("LaunchAngleSlider").GetComponent<Slider>().value;

            // Think of it as top-down view of vectors: 
            // we don't care about the y-component(height) of the initial and target position.
            Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 targetXZPos = new Vector3(TargetObject.position.x, 0.0f, TargetObject.position.z);

            // rotate the object to face the target
            transform.LookAt(targetXZPos);

            // Formula
            float distance = Vector3.Distance(projectileXZPos, targetXZPos);
            float gravity = Physics.gravity.y;
            float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad); //in radius
            float height = (TargetObject.position.y + GetPlatformOffset()) - transform.position.y;

            //Debug.Log(string.Format("tanAlpha: {0}", tanAlpha));

            // Calculate the initial speed required to land the projecile on the target object
            float Vz = Mathf.Sqrt(gravity * distance * distance / (2.0f * (height - distance * tanAlpha)));
            float Vy = tanAlpha * Vz;

            // Create the velocity vector in local space and get it in global space
            Vector3 localVelocity = new Vector3(0f, Vy, Vz);
            Vector3 globalVelocity = transform.TransformDirection(localVelocity);

            // Launch the object by setting its initial velocity and flipping its state
            rigid.velocity = globalVelocity;

            bTargetReady = false;

            spin = true;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("\nPlease enter float or integer");
        }

    }




    // Resets the projectile to its initial position
    public void ResetToInitialState()
    {
        /*rigid.useGravity = false;
        rigid.useGravity = true;
        bTargetReady = true;
        rigid.velocity = Vector3.zero;
        this.transform.SetPositionAndRotation(initialPosition, initialRotation);*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bTargetReady)
            {
                Launch();
            }
            else
            {
                ResetToInitialState();
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToInitialState();
        }*/



        if (!bTouchingGround && !bTargetReady)
        {
            // Update the rotation of the projectile during trajectory motion
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
        if (spin)
        {
            Debug.Log("Spinning");
            missileBody.transform.Rotate(xAngle: 0, yAngle: 0, zAngle: spinValue);
        }

    }



    void OnCollisionEnter(Collision collision)
    {
        bTouchingGround = true;
        rigid.velocity = Vector3.zero;
        spin = false;
    }

    void OnCollisionExit()
    {
        bTouchingGround = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Target") || (!bTargetReady && collider.CompareTag("Ground")))
        {
            Debug.Log("Hit!");
            // TODO: figure out a way to recreating the missile after destroying it
            //Destroy(gameObject);
        }
    }
}
