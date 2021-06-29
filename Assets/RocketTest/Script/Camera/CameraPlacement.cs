using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPlacement : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] CharacterController characterController;

    public GameObject[] camPlacement;

    public int selectedCamIndex = -1;

    public bool editingMode = false;
    public float moveSpeed = 100f;
    public float rotSpeed = 25f;

    public bool startWithEditorPlacements = false; 

    private void Awake()
    {
        if (startWithEditorPlacements)
        {
            saveCamPlacements();
        }
        loadCamPlacement();
    }

    private void Start()
    {
        characterController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveCam();
    }

    public void moveCam()
    {
        if (editingMode)
        {
            if (selectedCamIndex > -1)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    characterController.Move(playerCamera.transform.forward * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    characterController.Move(-playerCamera.transform.right * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    characterController.Move(-playerCamera.transform.forward * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    characterController.Move(playerCamera.transform.right * moveSpeed * Time.deltaTime);
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
                    playerCamera.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.E))
                {
                    playerCamera.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    playerCamera.transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    playerCamera.transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
                }
                if(playerCamera.transform.position != camPlacement[selectedCamIndex].transform.position)
                {
                    camPlacement[selectedCamIndex].transform.position = playerCamera.transform.position;
                }
                if (playerCamera.transform.rotation != camPlacement[selectedCamIndex].transform.rotation)
                {
                    camPlacement[selectedCamIndex].transform.rotation = playerCamera.transform.rotation;
                }
            }
        }
    }

    public void selectCameraPlacement(int n)
    {
        if (selectedCamIndex != n)
        {
            selectedCamIndex = n;
            playerCamera.transform.SetPositionAndRotation(camPlacement[n].transform.position, camPlacement[n].transform.rotation);
            characterController.enabled = true;

        }
    }

    public void saveCamPlacements()
    {
        for (int i = 0; i < camPlacement.Length; i++)
        {
            PlayerPrefs.SetFloat("xPosPlacement" + i.ToString(), camPlacement[i].transform.position.x);
            PlayerPrefs.SetFloat("yPosPlacement" + i.ToString(), camPlacement[i].transform.position.y);
            PlayerPrefs.SetFloat("zPosPlacement" + i.ToString(), camPlacement[i].transform.position.z);
            PlayerPrefs.SetFloat("xRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.x);
            PlayerPrefs.SetFloat("yRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.y);
            PlayerPrefs.SetFloat("zRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.z);
        }
    }

    public void loadCamPlacement()
    {
        for(int i = 0; i < camPlacement.Length; i++)
        {
            var pos = new Vector3(PlayerPrefs.GetFloat("xPosPlacement" + i.ToString()), PlayerPrefs.GetFloat("yPosPlacement" + i.ToString()), PlayerPrefs.GetFloat("zPosPlacement" + i.ToString()));
            var rot = new Vector3(PlayerPrefs.GetFloat("xRotPlacement" + i.ToString()), PlayerPrefs.GetFloat("yRotPlacement" + i.ToString()), PlayerPrefs.GetFloat("zRotPlacement" + i.ToString()));
            camPlacement[i].transform.position = pos;
            camPlacement[i].transform.eulerAngles = rot;
        }
    }

    public void openEditMode()
    {
        editingMode = true;
        selectedCamIndex = -1;
        cameraManager.lock_on = false;
        previewFocusedCam();
    }

    public void exitEditMode()
    {
        editingMode = false;
        selectedCamIndex = -1;
        cameraManager.switchToFocusedCamera(0);
        characterController.enabled = false;
    }

    public void setTransform()
    {
        if (selectedCamIndex > -1)
        {
            camPlacement[selectedCamIndex].transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.transform.rotation);
        }
    }

    public void resetTransform()
    {
        loadCamPlacement();
        if (selectedCamIndex > -1) 
        {
            playerCamera.transform.SetPositionAndRotation(camPlacement[selectedCamIndex].transform.position, camPlacement[selectedCamIndex].transform.rotation);
        }
    }

    public void previewWideCam()
    {
        playerCamera.transform.eulerAngles = camPlacement[selectedCamIndex].transform.eulerAngles;
        playerCamera.GetComponent<Camera>().fieldOfView = cameraManager.GetFieldOfView(cameraManager.missile_obj.transform.position, cameraManager.wideCamMissileHeight);
        
    }

    public void previewFocusedCam()
    {
        playerCamera.transform.LookAt(cameraManager.missile_obj.transform);
        playerCamera.GetComponent<Camera>().fieldOfView = cameraManager.GetFieldOfView(cameraManager.missile_obj.transform.position, cameraManager.focusedCamMissileHeight);
    }
    
}
