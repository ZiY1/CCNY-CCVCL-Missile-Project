using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMounted : MonoBehaviour
{
    public LaunchSetting ls;
    Vector3 rocketPos;
    float adjustPos = 150f; //how far you want the camera to be from the rocket
    //float yAngle = 180f;
    float timer = 0.5f;

    //private IEnumerator coroutine;
    bool rocketFound;

    // Start is called before the first frame update
    void Start()
    {
        //ls = GameObject.FindGameObjectWithTag("LaunchSetting").GetComponent<LaunchSetting>();
        //coroutine = Delay(timer);

        rocketFound = false;
        StartCoroutine(Delay(timer)); //delay is needed since this function starts but does not pick up rocket placement.
    }

    private IEnumerator Delay(float waitTime)
    {
        while(!ls.rocket)
        {
            yield return new WaitForSeconds(waitTime);
            //print("WaitAndPrint " + Time.time);
        }
        if(ls.rocket)
        {
            rocketFound = true;
            rocketPos = ls.rocket.transform.position;
            var q = Quaternion.AngleAxis(-90, Vector3.up);
            transform.rotation = q;
            //transform.eulerAngles = new Vector3(0f, -90f, 0f); //works, but wanted to use Quaterion.AngleAxis for no reason
            //rocketPos = new Vector3(rocketPos.x + adjustPos, rocketPos.y, rocketPos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rocketFound)
        {
            Vector3 pos = ls.rocket.transform.position;
            transform.position = new Vector3(pos.x + adjustPos, pos.y, pos.z);
        }
    }
}


//z = 20 y = 180'