using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileDOFSetUp : MonoBehaviour
{
    private float posX;
    private float posY;
    private float posZ;
    private float rotX;
    private float rotY;
    private float rotZ;

    public GameObject missile1;
    public GameObject missile2;
    public GameObject missile3;
    public GameObject missile4;
    public GameObject missile5;
    public GameObject missile6;
    public GameObject missile7;
    public GameObject missile8;
    public GameObject missile9;
    private GameObject missileActive;

    public InputField input_posX;
    public InputField input_posY;
    public InputField input_posZ;
    public InputField input_rotX;
    public InputField input_rotY;
    public InputField input_rotZ;

    private Vector3 currentPos;
    private Quaternion currentRot;
    // Start is called before the first frame update
    void Start()
    {
        missileActive = missile1;
    }

    public void GetUserInput()
    {
        try
        {
            posX = float.Parse(input_posX.text);
            posY = float.Parse(input_posY.text);
            posZ = float.Parse(input_posZ.text);
            rotX = float.Parse(input_rotX.text);
            rotY = float.Parse(input_rotY.text);
            rotZ = float.Parse(input_rotZ.text);

            SetMissileDOF();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    // Update all missiles to make it consistent
    void SetMissileDOF()
    {
        currentPos = new Vector3(posX, posY, posZ);
        currentRot = Quaternion.Euler(rotX, rotY, rotZ);

        missile1.transform.localPosition = currentPos;
        missile1.transform.localRotation = currentRot;

        missile2.transform.localPosition = currentPos;
        missile2.transform.localRotation = currentRot;

        missile3.transform.localPosition = currentPos;
        missile3.transform.localRotation = currentRot;

        missile4.transform.localPosition = currentPos;
        missile4.transform.localRotation = currentRot;

        missile5.transform.localPosition = currentPos;
        missile5.transform.localRotation = currentRot;

        missile6.transform.localPosition = currentPos;
        missile6.transform.localRotation = currentRot;

        missile7.transform.localPosition = currentPos;
        missile7.transform.localRotation = currentRot;

        missile8.transform.localPosition = currentPos;
        missile8.transform.localRotation = currentRot;

        missile9.transform.localPosition = currentPos;
        missile9.transform.localRotation = currentRot;
    }

}
