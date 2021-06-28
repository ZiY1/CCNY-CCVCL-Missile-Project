using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleInput : MonoBehaviour
{
    InputField Input;
    char[] num;
    public LaunchSetting ls;

    private void Awake()
    {
        Input = GetComponent<InputField>();
        num = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }

    // Start is called before the first frame update
    void Start()
    {
        //makes sure that inputs are only int. Will need to allow ',' for floats in the future
        Input.onValidateInput += delegate (string text, int charIndex, char addedChar) { return MyValidate(addedChar); };
        Input.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private char MyValidate(char myChar)
    {
        if (!num.Contains(myChar))
        {
            myChar = '\0';
            ErrorMessage("Only int input");
        }

        return myChar;
    }

    private void ValueChangeCheck()
    {
        //DebugInfo(Input.text.ToString());
        if (Input.text.ToString() != "")
            ls.ChangeAngle(float.Parse(Input.text.ToString()));
    }

    //private void Update()
    //{
    //    if()
    //    sp.sphere.transform.Rotate(0f, 0f, float.Parse(Input.ToString()));
    //}

    private void ErrorMessage(string errorText)
    {
        Debug.Log("(" + gameObject.name + ") Error: " + errorText);
    }

    private void DebugInfo(string debugText)
    {
        Debug.Log("(" + gameObject.name + ") Debug: " + debugText);
    }
}
