using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMissilePosition : MonoBehaviour
{
    // if true, will start with position and rotation of missile from the editor instead of saved P&R
    public bool startWithEditorPlacement;

    public Camera missile_cam;
    public CharacterController characterController;


    // TODO: Add more missiles when ready - ziyi
    public GameObject missile1;
    public GameObject missile2;
    public GameObject missile3;
    public GameObject missile4;
    public GameObject missile5;
    public GameObject missile6;
    public GameObject missile7;
    public GameObject missile8;
    public GameObject missile9;


    // assign it to active Missile - ziyi
    public GameObject missile;
    public GameObject platform;
    public GameObject ground;

    // true = missile position editing mode --> open
    public bool moving = false;

    // speed at which the missile is moved and rotated
    public float moveSpeed = 50f;
    public float rotSpeed = 50f;

    public Text current_position_text;

    // original position and rotation of missile when editing mode is opened
    GameObject original_Transform;

    public CameraPlacement cameraPlacement;

    public GameObject editingModeOpenObj;
    public GameObject enterCamEditingModeButtonObj;
    public GameObject camKeyboardControlsObj;

    // Start is called before the first frame update
    void Start()
    {
        // Defualt missile - ziyi
        missile = missile1;

        if (startWithEditorPlacement || !(PlayerPrefs.HasKey("missilePosX")))
        {
            savePosition();
        }

        loadMissilePosition();


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

    // Function to open missile position editing mode
    public void moveMissile()
    {
        // exits cam placement edit mode and target position edit mode
        if (cameraPlacement.editingMode)
        {
            cameraPlacement.exitEditMode();

            editingModeOpenObj.SetActive(false);
            enterCamEditingModeButtonObj.SetActive(true);
            camKeyboardControlsObj.SetActive(false);

        }

        // turns on camera to move missile
        missile_cam.gameObject.SetActive(true);
        missile_cam.depth = 2;

        original_Transform = new GameObject();

        // moves missile camera to be at an offset from the missile
        missile_cam.gameObject.transform.position = missile.transform.position + new Vector3(0, 1, -3);
        missile_cam.gameObject.transform.LookAt(missile.transform);

        // sets the missile camera as the parent of the missile, platform and ground so that they move together
        missile.transform.parent = missile_cam.gameObject.transform;
        platform.transform.parent = missile_cam.gameObject.transform;
        ground.transform.parent = missile_cam.gameObject.transform;

        // keeps track of initial position and rotation
        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        // prevents missile from moving due to physics
        missile.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        missile.GetComponent<Rigidbody>().isKinematic = true;

        characterController.enabled = true;
        moving = true;

    }

    // Function to move missile with keyboard input
    public void move()
    {
        if (Input.GetKey(KeyCode.W)) // move forward
        {
            characterController.Move(missile_cam.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) // move left
        {
            characterController.Move(-missile_cam.transform.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // move back
        {
            characterController.Move(-missile_cam.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) // move right
        {
            characterController.Move(missile_cam.transform.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift)) // move down
        {
            characterController.Move(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space)) // move up
        {
            characterController.Move(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q)) // rotate left
        {
            missile_cam.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E)) // move right
        {
            missile_cam.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }

        // update UI text that shows current missile's position
        current_position_text.text = "Missile Position: (" + missile.transform.position.x.ToString("F2") + ", " + missile.transform.position.y.ToString("F2") + ", " + missile.transform.position.z.ToString("F2") + ")";
    }

    // Function that updates the original position of the missile
    public void setPosition()
    {
        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        savePosition();
    }

    // Function to save the new position and rotation of the missile, platform, and ground to PlayerPrefs
    public void savePosition()
    {
        PlayerPrefs.SetFloat("missilePosX", missile.transform.position.x);
        PlayerPrefs.SetFloat("missilePosY", missile.transform.position.y);
        PlayerPrefs.SetFloat("missilePosZ", missile.transform.position.z);


        PlayerPrefs.SetFloat("platformPosX", platform.transform.position.x);
        PlayerPrefs.SetFloat("platformPosY", platform.transform.position.y);
        PlayerPrefs.SetFloat("platformPosZ", platform.transform.position.z);

        PlayerPrefs.SetFloat("platformRotX", platform.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("platformRotY", platform.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("platformRotZ", platform.transform.rotation.eulerAngles.z);


        PlayerPrefs.SetFloat("groundPosX", ground.transform.position.x);
        PlayerPrefs.SetFloat("groundPosY", ground.transform.position.y);
        PlayerPrefs.SetFloat("groundPosZ", ground.transform.position.z);

        PlayerPrefs.SetFloat("groundRotX", ground.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("groundRotY", ground.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("groundRotZ", ground.transform.rotation.eulerAngles.z);
    }

    // Function to load saved missile position
    void loadMissilePosition()
    {
        var missile_pos = new Vector3(PlayerPrefs.GetFloat("missilePosX"), PlayerPrefs.GetFloat("missilePosY"), PlayerPrefs.GetFloat("missilePosZ"));

        missile.transform.position = missile_pos;


        var platform_pos = new Vector3(PlayerPrefs.GetFloat("platformPosX"), PlayerPrefs.GetFloat("platformPosY"), PlayerPrefs.GetFloat("platformPosZ"));
        var platform_rot = new Vector3(PlayerPrefs.GetFloat("platformRotX"), PlayerPrefs.GetFloat("platformRotY"), PlayerPrefs.GetFloat("platformRotZ"));

        platform.transform.position = platform_pos;
        platform.transform.eulerAngles = platform_rot;


        var ground_pos = new Vector3(PlayerPrefs.GetFloat("groundPosX"), PlayerPrefs.GetFloat("groundPosY"), PlayerPrefs.GetFloat("groundPosZ"));
        var ground_rot = new Vector3(PlayerPrefs.GetFloat("groundRotX"), PlayerPrefs.GetFloat("groundRotY"), PlayerPrefs.GetFloat("groundRotZ"));

        ground.transform.position = ground_pos;
        ground.transform.eulerAngles = ground_rot;
    }

    // Function to reset position of missile
    public void Reset()
    {
        missile_cam.gameObject.transform.SetPositionAndRotation(original_Transform.transform.position, original_Transform.transform.rotation);
    }

    // Function to exit missile position editing mode
    public void exitMissilePositionEditMode()
    {
        // turns off missile camera
        missile_cam.depth = -2;

        Reset(); // deletes unsaved changes

        missile_cam.gameObject.SetActive(false);

        moving = false;
        characterController.enabled = false;

        missile.transform.parent = null;
        platform.transform.parent = null;
        ground.transform.parent = null;

        
        Destroy(original_Transform);

        // missile will be affected by physics engine
        missile.GetComponent<Rigidbody>().isKinematic = false;
        missile.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
    }


    // Reassign missile to active missile whenever dorpdown menu is clicked. - ziyi
    public void ReassignEditMissile()
    {
        if (missile1.activeSelf)
        {
            missile1.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile1;
        }
        else if (missile2.activeSelf)
        {
            missile2.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile2;
        }
        else if (missile3.activeSelf)
        {
            missile3.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile3;
        }
        else if (missile4.activeSelf)
        {
            missile4.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile4;
        }
        else if (missile5.activeSelf)
        {
            missile5.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile5;
        }
        else if (missile6.activeSelf)
        {
            missile6.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile6;
        }
        else if (missile7.activeSelf)
        {
            missile7.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile7;
        }
        else if (missile8.activeSelf)
        {
            missile8.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile8;
        }
        else if (missile9.activeSelf)
        {
            missile9.transform.SetPositionAndRotation(missile.transform.position, missile.transform.rotation);
            missile = missile9;
        }
    }

    // Return current missile position and rotation
    public Vector3 ReturnPosition()
    {
        return missile.transform.position;
    }

    public Quaternion ReturnRotation()
    {
        return missile.transform.rotation;
    }
}
