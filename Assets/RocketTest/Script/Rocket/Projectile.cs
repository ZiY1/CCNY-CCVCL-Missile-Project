using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // launch variables
    [SerializeField] private Transform TargetObject;
    [Range(1.0f, 6.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;

    // state
    private bool bTargetReady;
    private bool bTouchingGround;

    // cache
    private Rigidbody rigid;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bTargetReady = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    // launches the object towards the TargetObject with a given LaunchAngle
    void Launch() 
    {

        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 targetXZPos = new Vector3(TargetObject.position.x, transform.position.y, TargetObject.position.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = TargetObject.position.y - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rigid.velocity = globalVelocity;
        Debug.Log(transform.rotation.eulerAngles);
        bTargetReady = false; 
    }

    // resets the projectile to its initial position
    void ResetToInitialState()
    {
        rigid.velocity = Vector3.zero;
        this.transform.SetPositionAndRotation(initialPosition, initialRotation);
        bTargetReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (bTargetReady)
            {
                Launch();
            }
            else
            {
                ResetToInitialState();
                SetNewTarget();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToInitialState();
        }

        if (!bTouchingGround && !bTargetReady)
        {
            // update the rotation of the projectile during trajectory motion
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }


    // Sets a random target around the object based on the TargetRadius
    void SetNewTarget()
    {
        Transform targetTF = TargetObject.GetComponent<Transform>(); // shorthand

        // To acquire our new target from a point around the projectile object:
        // - we start with a vector in the XZ-Plane (ground), let's pick right (1, 0, 0).
        //   (or pick left, forward, back, or any perpendicular vector to the rotation axis, which is up)
        // - We'll use a quaternion to rotate our vector. To create a rotation quaternion, we'll be using
        //   the AngleAxis() function, which takes a rotation angle and a rotation amount in degrees as parameters.
        Vector3 rotationAxis = Vector3.up;  // as our object is on the XZ-Plane, we'll use up vector as the rotation axis.
        float randomAngle = Random.Range(0.0f, 360.0f);
        Vector3 randomVectorOnGroundPlane = Quaternion.AngleAxis(randomAngle, rotationAxis) * Vector3.right;

        // - scale the randomVector with the target radius
        // - we also add an offset which makes the starting position at the same height level as the target
        Vector3 randomPoint = randomVectorOnGroundPlane * TargetRadius + new Vector3(0, targetTF.position.y, 0);

        //  - finally, we'll set the target object's position and update our state.
        TargetObject.SetPositionAndRotation(randomPoint, targetTF.rotation);
        bTargetReady = true;
    }

    void OnCollisionEnter()
    {
        bTouchingGround = true;
    }

    void OnCollisionExit()
    {
        bTouchingGround = false;
    }
}
