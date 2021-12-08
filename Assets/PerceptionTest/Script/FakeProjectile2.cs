using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeProjectile2 : MonoBehaviour
{

    // Launch variables

    /* Using the SerializeField attribute causes Unity to serialize any private variable. 
     * This doesn't apply to static variables and properties in C#. You use the SerializeField 
     * attribute when you need your variable to be private but also want it to show up in the Editor
     */
    [SerializeField] private Transform TargetObject;
    private float LaunchAngle;


    // State
    private bool bLaunchAngleValid;
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
    public static float spinSpeed = 180f; // degrees per sec
    GameObject missileBody;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bTargetReady = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        missileBody = gameObject.transform.GetChild(0).gameObject;
        Debug.Log(missileBody.name);
        // Testing
        //Debug.Log("initPos: " + initialPosition);
        //Debug.Log("initRot: " + initialRotation);
    }

    public void Launch()
    {
        // Get the launch angle from the user
        // 10/08-Ziyi
        string launchAngleStr = "10";

            bLaunchAngleValid = true;
            LaunchAngle = float.Parse(launchAngleStr);

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
            float height = (TargetObject.position.y + 0.0f) - transform.position.y;

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
}
