using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetZaxis : MonoBehaviour
{
    public Transform targetObject;
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.text = targetObject.position.z.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //inputField.text = targetObject.position.z.ToString();
    }
}

