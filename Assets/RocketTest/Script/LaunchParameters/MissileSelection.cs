using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSelection : MonoBehaviour
{
    // TODO: Add more missiles when ready
    public GameObject Missile1;
    public GameObject Missile2;
    public CameraManager CameraManager;
    public EditMissilePosition EditMissilePosition;

    private Vector3 currentPosition;
    private Quaternion currentRotation;


    void Start()
    {
        Missile1.SetActive(true);
        Missile2.SetActive(false);

        // Current Position and Rotation equal to Missile1
        currentPosition = Missile1.transform.position;
        currentRotation = Missile1.transform.rotation;
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            Missile1.SetActive(true);
            Missile2.SetActive(false);
            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            currentPosition = Missile2.transform.position;
            currentRotation = Missile2.transform.rotation;

            Missile1.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
        else if (val == 1)
        {
            Missile1.SetActive(false);

            Missile2.SetActive(true);
            CameraManager.ReassignMissile();
            EditMissilePosition.ReassignEditMissile();
            //CheckActive();

            currentPosition = Missile1.transform.position;
            currentRotation = Missile1.transform.rotation;

            Missile2.transform.SetPositionAndRotation(currentPosition, currentRotation);
        }
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