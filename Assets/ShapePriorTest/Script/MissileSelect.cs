using UnityEngine;

public class MissileSelect : MonoBehaviour
{

    // TODO: Add more missiles when ready
    public GameObject missile1;
    public GameObject missile2;
    public GameObject missile3;
    public GameObject missile4;
    public GameObject missile5;
    public GameObject missile6;
    public GameObject missile7;
    public GameObject missile8;
    public GameObject missile9;

    public PosX posX;
    public PosY posY;
    public PosZ posZ;
    public RotX rotX;
    public RotY rotY;
    public RotZ rotZ;

    private Vector3 currentPosition;
    private Quaternion currentRotation;


    void Start()
    {
        missile1.SetActive(true);
        missile2.SetActive(false);
        missile2.SetActive(false);
        missile3.SetActive(false);
        missile4.SetActive(false);
        missile5.SetActive(false);
        missile6.SetActive(false);
        missile7.SetActive(false);
        missile8.SetActive(false);
        missile9.SetActive(false);
    }

    public void HandleInputData(int val)
    {

        if (val == 0)
        {
            missile1.SetActive(true);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 1)
        {
            missile2.SetActive(true);
            missile1.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 2)
        {
            missile3.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 3)
        {
            missile4.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 4)
        {
            missile5.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 5)
        {
            missile6.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 6)
        {
            missile7.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile8.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 7)
        {
            missile8.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile9.SetActive(false);
        }
        else if (val == 8)
        {
            missile9.SetActive(true);
            missile1.SetActive(false);
            missile2.SetActive(false);
            missile3.SetActive(false);
            missile4.SetActive(false);
            missile5.SetActive(false);
            missile6.SetActive(false);
            missile7.SetActive(false);
            missile8.SetActive(false);
        }

        posX.ReassignEditMissile();
        posY.ReassignEditMissile();
        posZ.ReassignEditMissile();
        rotX.ReassignEditMissile();
        rotY.ReassignEditMissile();
        rotZ.ReassignEditMissile();
    }
    // Debug only
    void CheckActive()
    {
        if (missile1.activeSelf)
        {
            Debug.Log("M1 active");

        }
        else if (missile2.activeSelf)
        {
            Debug.Log("M2 active");

        }
    }
}