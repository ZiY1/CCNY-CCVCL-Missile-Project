using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTargetPosition : MonoBehaviour
{
    public bool startWithEditorPlacement;

    public Camera missile_cam;
    public CharacterController characterController;


    // assign it to active Missile - ziyi
    public GameObject taregt;
    //public GameObject surface;
    //public GameObject mark;

    public bool moving = false;

    public float moveSpeed = 50f;
    public float rotSpeed = 50f;

    public Text current_position_text;

    GameObject original_Transform;

    public CameraPlacement cameraPlacement;

    public GameObject editingModeOpenObj;
    public GameObject enterCamEditingModeButtonObj;
    public GameObject camKeyboardControlsObj;

    // Start is called before the first frame update
    void Start()
    {
        if (startWithEditorPlacement || !(PlayerPrefs.HasKey("targetPosX")))
        {
            savePosition();
        }


        loadtargetPosition();


        missile_cam.gameObject.SetActive(false);
        characterController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            move();
        }
    }

    public void moveTarget()
    {
        if (cameraPlacement.editingMode)
        {
            cameraPlacement.exitEditMode();

            editingModeOpenObj.SetActive(false);
            enterCamEditingModeButtonObj.SetActive(true);
            camKeyboardControlsObj.SetActive(false);

        }

        missile_cam.gameObject.SetActive(true);
        missile_cam.depth = 2;

        original_Transform = new GameObject();

        missile_cam.gameObject.transform.position = taregt.transform.position + new Vector3(0, 1, -3);
        missile_cam.gameObject.transform.LookAt(taregt.transform);

        taregt.transform.parent = missile_cam.gameObject.transform;
        //surface.transform.parent = missile_cam.gameObject.transform;
        //mark.transform.parent = missile_cam.gameObject.transform;

        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        taregt.GetComponent<Rigidbody>().isKinematic = true;

        characterController.enabled = true;
        moving = true;

    }

    public void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(missile_cam.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(-missile_cam.transform.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-missile_cam.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(missile_cam.transform.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            characterController.Move(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            missile_cam.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            missile_cam.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }
        current_position_text.text = "Target Position: (" + taregt.transform.position.x.ToString("F2") + ", " + taregt.transform.position.y.ToString("F2") + ", " + taregt.transform.position.z.ToString("F2") + ")";
    }

    public void setPosition()
    {
        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        savePosition();
    }

    public void savePosition()
    {
        PlayerPrefs.SetFloat("targetPosX", taregt.transform.position.x);
        PlayerPrefs.SetFloat("targetPosY", taregt.transform.position.y);
        PlayerPrefs.SetFloat("targetPosZ", taregt.transform.position.z);

        PlayerPrefs.SetFloat("targetRotX", taregt.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("targetRotY", taregt.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("targetRotZ", taregt.transform.rotation.eulerAngles.z);
    }

    void loadtargetPosition()
    {
        var pos = new Vector3(PlayerPrefs.GetFloat("targetPosX"), PlayerPrefs.GetFloat("targetPosY"), PlayerPrefs.GetFloat("targetPosZ"));
        var rot = new Vector3(PlayerPrefs.GetFloat("targetRotX"), PlayerPrefs.GetFloat("targetRotY"), PlayerPrefs.GetFloat("targetRotZ"));

        taregt.transform.position = pos;
        taregt.transform.eulerAngles = rot;
    }

    public void Reset()
    {
        missile_cam.gameObject.transform.SetPositionAndRotation(original_Transform.transform.position, original_Transform.transform.rotation);
    }

    public void exitTargetPositionEditMode()
    {
        missile_cam.depth = -2;

        Reset();

        missile_cam.gameObject.SetActive(false);

        moving = false;
        characterController.enabled = false;

        taregt.transform.parent = null;
        //surface.transform.parent = null;
        //mark.transform.parent = null;


        Destroy(original_Transform);

        //taregt.GetComponent<Rigidbody>().isKinematic = false;
    }
}