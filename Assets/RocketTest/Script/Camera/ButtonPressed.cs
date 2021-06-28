using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPressed : MonoBehaviour
{
    int num;
    public CameraSettings cs;

    // Start is called before the first frame update
    void Start()
    {
        num = int.Parse(GetComponentInChildren<Text>().text.ToString());
    }

    public void OnPress()
    {
        cs.ChangeCameraPlacement(num - 1);
    }

    public void WIP()
    {
        Debug.Log("Not working yet");
    }
}
