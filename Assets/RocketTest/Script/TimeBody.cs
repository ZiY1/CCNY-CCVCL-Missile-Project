using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    bool isRewinding = false;
    //public float currentSpeed;

    public float recordTime = 5f; //the extent of time that a user can rewind. Increasing this allows users to rewind further but also increases the information size

    //records the upward and rightward velocity of rocket
    [SerializeField] float forceY;
    [SerializeField] float forceZ;

    List<PointInTime> pointInTime; //custom script that holds an array of rockets position, rotation, and velocity

    RocketSetting rs;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pointInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        rs = GetComponent<RocketSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }
        if(Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointInTime.Count > 0) //ensure that no error pops up if rewinding too far
        {
            PointInTime pointsInTime = pointInTime[0];
            transform.position = pointsInTime.position;
            transform.rotation = pointsInTime.rotation;
            //currentSpeed = pointsInTime.speed;
            forceY = pointsInTime.velocity.y;
            forceZ = pointsInTime.velocity.z;

            pointInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
            

    }

    void Record()
    {
        if(pointInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointInTime.RemoveAt(pointInTime.Count - 1); //ensures program is not recording too much
        }

        pointInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
        //rs.Launch(currentSpeed);
        rs.AdjustSpeed(forceY, forceZ);
    }
}
