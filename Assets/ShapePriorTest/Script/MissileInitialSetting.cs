using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissileInitialSetting : MonoBehaviour
{
    public GameObject missile1;
    public GameObject missile2;
    public GameObject missile3;
    public GameObject missile4;
    public GameObject missile5;
    public GameObject missile6;
    public GameObject missile7;
    public GameObject missile8;
    public GameObject missile9;
    // Start is called before the first frame update
    void Start()
    {
        SetUpInitialDOF();
    }

    public void SetUpInitialDOF()
    {
        // Set initial 6DOF of the missiles
        Vector3 initialVec = new Vector3(0, 0, 2);
        Quaternion initialQuat = Quaternion.Euler(-90, 0, 0);

        missile1.transform.localPosition = initialVec;
        missile1.transform.localRotation = initialQuat;

        missile2.transform.localPosition = new Vector3(0, 0, 3); // for better virtual effect
        missile2.transform.localRotation = initialQuat;

        missile3.transform.localPosition = initialVec;
        missile3.transform.localRotation = initialQuat;

        missile4.transform.localPosition = initialVec;
        missile4.transform.localRotation = initialQuat;

        missile5.transform.localPosition = initialVec;
        missile5.transform.localRotation = initialQuat;

        missile6.transform.localPosition = initialVec;
        missile6.transform.localRotation = initialQuat;

        missile7.transform.localPosition = initialVec;
        missile7.transform.localRotation = initialQuat;

        missile8.transform.localPosition = initialVec;
        missile8.transform.localRotation = initialQuat;

        missile9.transform.localPosition = new Vector3(0, 0, 3); // for better virtual effect
        missile9.transform.localRotation = initialQuat;
    }

    public void ResetAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
