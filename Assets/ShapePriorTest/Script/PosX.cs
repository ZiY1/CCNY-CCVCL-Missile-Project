using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosX : MonoBehaviour
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

    public InputField inputField;

    void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.text = missile1.transform.localPosition.x.ToString();
    }

    public void ReassignEditMissile()
    {
        if (missile1.activeSelf)
        {
            inputField.text = missile1.transform.localPosition.x.ToString();
        }
        else if (missile2.activeSelf)
        {
            inputField.text = missile2.transform.localPosition.x.ToString();
        }
        else if (missile3.activeSelf)
        {
            inputField.text = missile3.transform.localPosition.x.ToString();
        }
        else if (missile4.activeSelf)
        {
            inputField.text = missile4.transform.localPosition.x.ToString();
        }
        else if (missile5.activeSelf)
        {
            inputField.text = missile5.transform.localPosition.x.ToString();
        }
        else if (missile6.activeSelf)
        {
            inputField.text = missile6.transform.localPosition.x.ToString();
        }
        else if (missile7.activeSelf)
        {
            inputField.text = missile7.transform.localPosition.x.ToString();
        }
        else if (missile8.activeSelf)
        {
            inputField.text = missile8.transform.localPosition.x.ToString();
        }
        else if (missile9.activeSelf)
        {
            inputField.text = missile9.transform.localPosition.x.ToString();
        }
    }
}
