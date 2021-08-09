using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMissilePosition : MonoBehaviour
{
    public Camera missile_cam;
    public CharacterController characterController;


    // TODO: Add more missiles when ready - ziyi
    public GameObject missile1;
    public GameObject missile2;


    // assign it to active Missile - ziyi
    public GameObject missile;
    public GameObject platform;
    public GameObject ground;

    public bool moving = false;

    public float moveSpeed = 50f;
    public float rotSpeed = 50f;

    public Text current_position_text;

    GameObject original_Transform;

    public CameraPlacement cameraPlacement;

    public GameObject editingModeOpenObj;
    public GameObject enterCamEditingModeButtonObj;
    public GameObject camKeyboardControlsObj;

    public bool startWithEditorPlacement;

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

    public void moveMissile()
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

        missile_cam.gameObject.transform.position = missile.transform.position + new Vector3(0, 1, -3);
        missile_cam.gameObject.transform.LookAt(missile.transform);

        missile.transform.parent = missile_cam.gameObject.transform;
        platform.transform.parent = missile_cam.gameObject.transform;
        ground.transform.parent = missile_cam.gameObject.transform;

        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        missile.GetComponent<Rigidbody>().isKinematic = true;

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
        current_position_text.text = "Missile Position: (" + missile.transform.position.x.ToString("F2") + ", " + missile.transform.position.y.ToString("F2") + ", " + missile.transform.position.z.ToString("F2") + ")";
    }

    public void setPosition()
    {
        original_Transform.transform.SetPositionAndRotation(missile_cam.gameObject.transform.position, missile_cam.gameObject.transform.rotation);

        savePosition();
    }

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

    public void Reset()
    {
        missile_cam.gameObject.transform.SetPositionAndRotation(original_Transform.transform.position, original_Transform.transform.rotation);
    }

    public void exitMissilePositionEditMode()
    {
        missile_cam.depth = -2;

        Reset();

        missile_cam.gameObject.SetActive(false);

        moving = false;
        characterController.enabled = false;

        missile.transform.parent = null;
        platform.transform.parent = null;
        ground.transform.parent = null;

        
        Destroy(original_Transform);

        missile.GetComponent<Rigidbody>().isKinematic = false;
    }


    // Reassign missile to active missile whenever dorpdown menu is clicked. - ziyi
    public void ReassignEditMissile()
    {
        if (missile1.activeSelf)
        {
            missile = missile1;
        }
        else if (missile2.activeSelf)
        {
            missile = missile2;
        }
    }
}
