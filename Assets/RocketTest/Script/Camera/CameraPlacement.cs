using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPlacement : MonoBehaviour
{
    [SerializeField] GameObject playerCamPrefab;
    [SerializeField] public List<GameObject> playerCamera;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] CharacterController characterController;

    public List<GameObject> camPlacement;

    public List<Button> camSelectionButtons;
    public Button wideViewButton;
    public Button focusedViewButton;

    public GameObject fovpanel;
    public Slider fovSlider;
    public List<float> wideCamFoVs;
    public InputField FoVInputField;

    public int selectedCamIndex = -1;

    public bool editingMode = false;
    public float moveSpeed = 100f;
    public float rotSpeed = 25f;

    public bool startWithEditorPlacements = false;
    public GameObject camSelectionButtonPrefab;
    public GameObject newButtonParent;
    public Button addNewLocationButton;

    private void Awake()
    {
        if (startWithEditorPlacements)
        {
            saveCamPlacements();
        }
        loadCamPlacement();

        characterController.enabled = false;
        foreach (GameObject location in camPlacement)
        {
            GameObject cam = Instantiate(playerCamPrefab);
            playerCamera.Add(cam);
            cam.transform.SetPositionAndRotation(location.transform.position, location.transform.rotation);
            cam.GetComponent<CharacterController>().enabled = false;
        }
    }

    private void Start()
    {
        
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
                    characterController.Move(playerCamera[selectedCamIndex].transform.forward * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    characterController.Move(-playerCamera[selectedCamIndex].transform.right * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    characterController.Move(-playerCamera[selectedCamIndex].transform.forward * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    characterController.Move(playerCamera[selectedCamIndex].transform.right * moveSpeed * Time.deltaTime);
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
                    playerCamera[selectedCamIndex].transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.E))
                {
                    playerCamera[selectedCamIndex].transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    playerCamera[selectedCamIndex].transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    playerCamera[selectedCamIndex].transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
                }
                if(playerCamera[selectedCamIndex].transform.position != camPlacement[selectedCamIndex].transform.position)
                {
                    camPlacement[selectedCamIndex].transform.position = playerCamera[selectedCamIndex].transform.position;
                }
                if (playerCamera[selectedCamIndex].transform.rotation != camPlacement[selectedCamIndex].transform.rotation)
                {
                    camPlacement[selectedCamIndex].transform.rotation = playerCamera[selectedCamIndex].transform.rotation;
                }
            }
        }
    }

    public void selectCameraPlacement(int n)
    {
        if (selectedCamIndex != n)
        {
            if(selectedCamIndex != n && selectedCamIndex != -1)
            {
                camSelectionButtons[selectedCamIndex].image.color = Color.white;
                characterController.enabled = false;
                playerCamera[selectedCamIndex].GetComponent<Camera>().depth = -1;
            }

            selectedCamIndex = n;
            playerCamera[selectedCamIndex].GetComponent<Camera>().depth = 0;

            playerCamera[selectedCamIndex].transform.SetPositionAndRotation(camPlacement[n].transform.position, camPlacement[n].transform.rotation);
            camSelectionButtons[n].image.color = Color.yellow;

            characterController = playerCamera[selectedCamIndex].GetComponent<CharacterController>();
            characterController.enabled = true;

            previewWideCam();
        }
    }

    public void saveCamPlacements()
    {
        if(camPlacement.Count < PlayerPrefs.GetInt("numberOfCams"))
        {
            for(int i = camPlacement.Count; i < PlayerPrefs.GetInt("numberOfCams"); i++)
            {
                PlayerPrefs.DeleteKey("xPosPlacement" + i.ToString());
                PlayerPrefs.DeleteKey("yPosPlacement" + i.ToString());
                PlayerPrefs.DeleteKey("zPosPlacement" + i.ToString());

                PlayerPrefs.DeleteKey("xRotPlacement" + i.ToString());
                PlayerPrefs.DeleteKey("yRotPlacement" + i.ToString());
                PlayerPrefs.DeleteKey("zRotPlacement" + i.ToString());

                PlayerPrefs.DeleteKey("fovcam" + i.ToString());
            }
        }

        for (int i = 0; i < camPlacement.Count; i++)
        {
            PlayerPrefs.SetFloat("xPosPlacement" + i.ToString(), camPlacement[i].transform.position.x);
            PlayerPrefs.SetFloat("yPosPlacement" + i.ToString(), camPlacement[i].transform.position.y);
            PlayerPrefs.SetFloat("zPosPlacement" + i.ToString(), camPlacement[i].transform.position.z);

            PlayerPrefs.SetFloat("xRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.x);
            PlayerPrefs.SetFloat("yRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.y);
            PlayerPrefs.SetFloat("zRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.z);

            PlayerPrefs.SetFloat("fovcam" + i.ToString(), wideCamFoVs[i]);

            PlayerPrefs.SetInt("numberOfCams", camPlacement.Count);
        }

    }

    public void loadCamPlacement()
    {
        for(int i = 0; i < PlayerPrefs.GetInt("numberOfCams"); i++)
        {
                var pos = new Vector3(PlayerPrefs.GetFloat("xPosPlacement" + i.ToString()), PlayerPrefs.GetFloat("yPosPlacement" + i.ToString()), PlayerPrefs.GetFloat("zPosPlacement" + i.ToString()));
                var rot = new Vector3(PlayerPrefs.GetFloat("xRotPlacement" + i.ToString()), PlayerPrefs.GetFloat("yRotPlacement" + i.ToString()), PlayerPrefs.GetFloat("zRotPlacement" + i.ToString()));
                camPlacement[i].transform.position = pos;
                camPlacement[i].transform.eulerAngles = rot;
                wideCamFoVs[i] = PlayerPrefs.GetFloat("fovcam" + i.ToString());
        }
    }

    public void openEditMode()
    {
        foreach(Button b in camSelectionButtons)
        {
            b.image.color = Color.white;
        }
        characterController = playerCamera[selectedCamIndex].GetComponent<CharacterController>();
        editingMode = true;
        cameraManager.lock_on = false;
        selectCameraPlacement(selectedCamIndex);
        previewWideCam();
        camSelectionButtons[selectedCamIndex].image.color = Color.yellow;
        characterController.enabled = true;
    }

    public void exitEditMode()
    {
        selectedCamIndex = -1;

        if (PlayerPrefs.GetInt("numberOfCams") < playerCamera.Count)
        {
            int cam_count = playerCamera.Count;
            for (int i = cam_count-1; i >= PlayerPrefs.GetInt("numberOfCams"); i--) 
            {
                GameObject temp = playerCamera[i];
                playerCamera.RemoveAt(i);
                Destroy(temp);

                temp = camSelectionButtons[i].gameObject;
                camSelectionButtons.RemoveAt(i);
                Destroy(temp);

                temp = camPlacement[i];
                camPlacement.RemoveAt(i);
                Destroy(temp);

                wideCamFoVs.RemoveAt(i);

                addNewLocationButton.gameObject.transform.position = addNewLocationButton.gameObject.transform.position + new Vector3(-40, 0, 0);
            }
        }
        foreach(GameObject cam in playerCamera)
        {
            cam.GetComponent<Camera>().depth = -1;
        }
        for (int i = 0; i < cameraManager.camButtons.Count; i++)
        {
            cameraManager.camButtons[i].image.color = Color.white;
        }
        resetTransform();

        cameraManager.switchToFocusedCamera(0);
        characterController.enabled = false;
        editingMode = false;

        cameraManager.createCamSelectionButtons();

    }

    public void setTransform()
    {
        if (selectedCamIndex > -1)
        {
            camPlacement[selectedCamIndex].transform.SetPositionAndRotation(playerCamera[selectedCamIndex].transform.position, playerCamera[selectedCamIndex].transform.rotation);
        }
    }

    public void resetTransform()
    {
        loadCamPlacement();
        if (selectedCamIndex > -1) 
        {
            playerCamera[selectedCamIndex].transform.SetPositionAndRotation(camPlacement[selectedCamIndex].transform.position, camPlacement[selectedCamIndex].transform.rotation);
            previewWideCam();
        }
    }

    public void previewWideCam()
    {
        playerCamera[selectedCamIndex].transform.eulerAngles = camPlacement[selectedCamIndex].transform.eulerAngles;

        wideViewButton.gameObject.SetActive(false);
        focusedViewButton.gameObject.SetActive(true);
        fovpanel.SetActive(true);

        fovSlider.value = wideCamFoVs[selectedCamIndex];
        playerCamera[selectedCamIndex].GetComponent<Camera>().fieldOfView = fovSlider.value;
        FoVInputField.text = wideCamFoVs[selectedCamIndex].ToString();
    }

    public void previewFocusedCam()
    {
        playerCamera[selectedCamIndex].transform.LookAt(cameraManager.missile_obj.transform);
        playerCamera[selectedCamIndex].GetComponent<Camera>().fieldOfView = cameraManager.GetFieldOfView(cameraManager.missile_obj.transform.position, cameraManager.focusedCamMissileHeight, playerCamera[selectedCamIndex].GetComponent<Camera>());
        focusedViewButton.gameObject.SetActive(false);
        wideViewButton.gameObject.SetActive(true);
        fovpanel.SetActive(false);
    }

    public void changeWideFoV()
    {
        playerCamera[selectedCamIndex].GetComponent<Camera>().fieldOfView = fovSlider.value;
        if(selectedCamIndex != -1)
        {
            wideCamFoVs[selectedCamIndex] = fovSlider.value;
        }
        FoVInputField.text = fovSlider.value.ToString();
    }

    public void updateFoV()
    {
        int.TryParse(FoVInputField.text, out int fov);

        float newfov = Mathf.Clamp(fov, fovSlider.minValue, fovSlider.maxValue);

        playerCamera[selectedCamIndex].GetComponent<Camera>().fieldOfView = newfov;

        fovSlider.value = newfov;
        FoVInputField.text = newfov.ToString();
    }

    public void addNewCamPlacement()
    {
        int newButtonIndex = camSelectionButtons.Count;

        GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, newButtonParent.transform);
        new_button.transform.position = camSelectionButtons[newButtonIndex - 1].transform.position + new Vector3(40, 0, 0);

        camSelectionButtons.Add(new_button.gameObject.GetComponent<Button>());

        camSelectionButtons[newButtonIndex].gameObject.GetComponentInChildren<Text>().text = camSelectionButtons.Count.ToString();
        camSelectionButtons[newButtonIndex].onClick.AddListener(() => { int tmp = newButtonIndex; this.selectCameraPlacement(tmp); });

        addNewLocationButton.gameObject.transform.position = addNewLocationButton.gameObject.transform.position + new Vector3(40, 0, 0);

        camPlacement.Add(new GameObject("Location " + (newButtonIndex + 1).ToString()));
        camPlacement[newButtonIndex].transform.SetPositionAndRotation(playerCamera[selectedCamIndex].transform.position, playerCamera[selectedCamIndex].transform.rotation);

        wideCamFoVs.Add(70f);

        playerCamera.Add(Instantiate(playerCamPrefab));
        playerCamera[newButtonIndex].transform.SetPositionAndRotation(playerCamera[selectedCamIndex].transform.position, playerCamera[selectedCamIndex].transform.rotation);

        selectCameraPlacement(newButtonIndex);

        if(newButtonIndex == 2)
        {
            addNewLocationButton.gameObject.SetActive(false);
        }

    }

}
