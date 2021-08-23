using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTargetPosition : MonoBehaviour
{
    // if true, will start with position and rotation of target from the editor instead of saved P&R
    public bool startWithEditorPlacement;

    public Camera missile_cam;
    public CharacterController characterController;


    // assign it to active Missile - ziyi
    public GameObject taregt;
    //public GameObject surface;
    //public GameObject mark;

    // true = target position editing mode --> open
    public bool moving = false;

    // speed at which the target is moved and rotated
    public float moveSpeed = 50f;
    public float rotSpeed = 50f;

    public Text current_position_text;

    // original position and rotation of target when editing mode is opened
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

    // Function to open target position editing mode
    public void moveTarget()
    {
        // exits missile position and camera placement editing mode
        if (cameraPlacement.editingMode)
        {
            cameraPlacement.exitEditMode();

            editingModeOpenObj.SetActive(false);
            enterCamEditingModeButtonObj.SetActive(true);
            camKeyboardControlsObj.SetActive(false);

        }

        // turns on camera to move target
        missile_cam.gameObject.SetActive(true);
        missile_cam.depth = 2;

        original_Transform = new GameObject();

        missile_cam.gameObject.transform.position = taregt.transform.position + new Vector3(0, 1, -3);
        missile_cam.gameObject.transform.LookAt(taregt.transform);

        // makes target child of camera to they move together
        taregt.transform.parent = missile_cam.gameObject.transform;

        // initializes original target position and rotation
        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        taregt.GetComponent<Rigidbody>().isKinematic = true;

        characterController.enabled = true;
        moving = true;

    }

    // Function to move target
    public void move()
    {
        if (Input.GetKey(KeyCode.W)) // forward
        {
            characterController.Move(missile_cam.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) // left
        {
            characterController.Move(-missile_cam.transform.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // back
        {
            characterController.Move(-missile_cam.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) // right
        {
            characterController.Move(missile_cam.transform.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift)) // down
        {
            characterController.Move(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space)) // up
        {
            characterController.Move(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q)) // rotate left
        {
            missile_cam.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E)) // rotate right
        {
            missile_cam.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }
        // update target position UI text
        current_position_text.text = "Target Position: (" + taregt.transform.position.x.ToString("F2") + ", " + taregt.transform.position.y.ToString("F2") + ", " + taregt.transform.position.z.ToString("F2") + ")";
    }

    // Function that updates the original position of the target
    public void setPosition()
    {
        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        savePosition();
    }

    // Function to save the new position and rotation of the target to PlayerPrefs
    public void savePosition()
    {
        PlayerPrefs.SetFloat("targetPosX", taregt.transform.position.x);
        PlayerPrefs.SetFloat("targetPosY", taregt.transform.position.y);
        PlayerPrefs.SetFloat("targetPosZ", taregt.transform.position.z);

        PlayerPrefs.SetFloat("targetRotX", taregt.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("targetRotY", taregt.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("targetRotZ", taregt.transform.rotation.eulerAngles.z);
    }

    // Function to load saved target transform
    void loadtargetPosition()
    {
        var pos = new Vector3(PlayerPrefs.GetFloat("targetPosX"), PlayerPrefs.GetFloat("targetPosY"), PlayerPrefs.GetFloat("targetPosZ"));
        var rot = new Vector3(PlayerPrefs.GetFloat("targetRotX"), PlayerPrefs.GetFloat("targetRotY"), PlayerPrefs.GetFloat("targetRotZ"));

        taregt.transform.position = pos;
        taregt.transform.eulerAngles = rot;
    }

    // Function to reset position of target
    public void Reset()
    {
        missile_cam.gameObject.transform.SetPositionAndRotation(original_Transform.transform.position, original_Transform.transform.rotation);
    }

    // Function to exit target position editing mode
    public void exitTargetPositionEditMode()
    {
        // turn off target cam
        missile_cam.depth = -2;

        Reset(); // deletes any unsaved changes

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