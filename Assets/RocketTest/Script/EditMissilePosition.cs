using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMissilePosition : MonoBehaviour
{
    public Camera missile_cam;
    public CharacterController characterController;

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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void loadMissilePosition()
    {

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
}
