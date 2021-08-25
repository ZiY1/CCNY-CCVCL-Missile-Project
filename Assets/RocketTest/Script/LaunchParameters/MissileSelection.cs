using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers;
using UnityEngine.Perception.Randomization.Scenarios;

public class MissileSelection : MonoBehaviour
{
    public FixedLengthScenario fixedLengthScenario;

    // TODO: Add more missiles when ready
    public GameObject Missile1;
    public GameObject Missile2;
    public GameObject Missile3;
    public GameObject Missile4;
    public GameObject Missile5;
    public GameObject Missile6;
    public GameObject Missile7;
    public GameObject Missile8;
    public GameObject Missile9;

    public CameraManager CameraManager;
    public EditMissilePosition EditMissilePosition;
    public CustomAnnotationAndMetricReporter customAnnotationAndMetricReporter;

    private Vector3 currentPosition;
    private Quaternion currentRotation;


    void Start()
    {
        Missile1.SetActive(true);

        Missile2.SetActive(false);
        Missile2.SetActive(false);
        Missile3.SetActive(false);
        Missile4.SetActive(false);
        Missile5.SetActive(false);
        Missile6.SetActive(false);
        Missile7.SetActive(false);
        Missile8.SetActive(false);
        Missile9.SetActive(false);

        /*// Current Position and Rotation equal to Missile1
        currentPosition = Missile1.transform.position;
        currentRotation = Missile1.transform.rotation;*/
    }

    public void HandleInputData(int val)
    {
        currentPosition = EditMissilePosition.ReturnPosition();
        currentRotation = EditMissilePosition.ReturnRotation();

        if (val == 0)
        {
            Missile1.SetActive(true);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();


            Missile1.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 1)
        {
            Missile2.SetActive(true);
            Missile1.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();



            Missile2.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 2)
        {
            Missile3.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile3.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 3)
        {
            Missile4.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile4.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 4)
        {
            Missile5.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile5.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 5)
        {
            Missile6.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile6.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 6)
        {
            Missile7.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile8.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile7.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 7)
        {
            Missile8.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile9.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile8.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 8)
        {
            Missile9.SetActive(true);
            Missile1.SetActive(false);
            Missile2.SetActive(false);
            Missile3.SetActive(false);
            Missile4.SetActive(false);
            Missile5.SetActive(false);
            Missile6.SetActive(false);
            Missile7.SetActive(false);
            Missile8.SetActive(false);

            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            Missile9.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }

        fixedLengthScenario.GetRandomizer<CustomRotationRandomizer>().missileParent = CameraManager.missile_obj;
        customAnnotationAndMetricReporter.missile = CameraManager.missile_obj;
    }
    // Debug only
    void CheckActive()
    {
        if (Missile1.activeSelf)
        {
            Debug.Log("M1 active");

        }
        else if (Missile2.activeSelf)
        {
            Debug.Log("M2 active");

        }
    }
}