using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraPlacement : MonoBehaviour
{
    // if true, will start with position and rotation of placements from the editor instead of saved placements
    public bool startWithEditorPlacements = false;

    [SerializeField] GameObject playerCamPrefab;
    [SerializeField] public GameObject playerCamera;

    [SerializeField] CameraManager cameraManager;
    [SerializeField] CharacterController characterController;

    // list containing camera placements
    public List<GameObject> camPlacement;

    public List<Button> camSelectionButtons;
    public Button wideViewButton;
    public Button focusedViewButton;

    public GameObject fovpanel;
    public Slider fovSlider;
    public InputField FoVInputField;
    public List<float> wideCamFoVs;
    
    // indicates which cam placement is selected (-1 = none selected)
    public int selectedCamIndex = -1;

    public bool editingMode = false;

    // speed at which cam moves and rotates
    public float moveSpeed = 50f;
    public float rotSpeed = 50f;

    public Text current_position_text;
    public InputField x_pos_inputField;
    public InputField y_pos_inputField;
    public InputField z_pos_inputField;
    public Button edit_pos_button;
    public Button set_pos_button;
    public Button cancel_set_pos_button;

    public Text current_rotation_text;
    public InputField x_rot_inputField;
    public InputField y_rot_inputField;
    public InputField z_rot_inputField;
    public Button edit_rot_button;
    public Button set_rot_button;
    public Button cancel_set_rot_button;

    public GameObject camSelectionButtonPrefab;
    public GameObject newButtonParent;

    public Button addNewLocationButton;
    public Button removeLocationButton;

    public EditMissilePosition editMissilePositionManager;
    

    public GameObject missileEditPanelObj;
    public GameObject missilePositionKeyboardControlsObj;
    public GameObject openEditMissilePositinModeButtonObj;

    public EditTargetPosition editTargetPositionManager;

    public GameObject targetEditPanelObj;
    public GameObject targetPositionKeyboardControlsObj;
    public GameObject openEditTargetPositinModeButtonObj;

    public Volume postProcessingVolume;

    // dph = depth of field (controls camera blur)
    public Slider dphDistSlider;
    public InputField dphDistInputField;
    public List<float> dphDistances;
    

    public Slider dphIntensitySlider;
    public InputField dphIntensityInputField;
    public List<float> dphIntensities;
    

    private void Awake()
    {
        if (startWithEditorPlacements)
        {
            saveCamPlacements();
        }

        // if there is no saved placement data -> save
        if (!PlayerPrefs.HasKey("numberOfCams"))
        {
            saveCamPlacements();
        }

        loadCamPlacement(); // load saved placements

        characterController.enabled = false;
        /*foreach (GameObject location in camPlacement)
        {
            GameObject cam = Instantiate(playerCamPrefab);
            playerCamera.Add(cam);
            cam.transform.SetPositionAndRotation(location.transform.position, location.transform.rotation);
            cam.GetComponent<CharacterController>().enabled = false;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        moveCam();
    }

    // Function to move the camera in editing mode
    public void moveCam()
    {
        if (editingMode) // can only move cam in cam editing mode
        {
            if (selectedCamIndex > -1)
            {
                if (Input.GetKey(KeyCode.W)) // move forward
                {
                    characterController.Move(playerCamera.transform.forward * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.A)) // move left
                {
                    characterController.Move(-playerCamera.transform.right * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.S)) // move back
                {
                    characterController.Move(-playerCamera.transform.forward * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D)) // move right
                {
                    characterController.Move(playerCamera.transform.right * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftShift))  // move down
                {
                    characterController.Move(Vector3.down * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Space)) // move up
                {
                    characterController.Move(Vector3.up * moveSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Q)) // rotate left
                {
                    playerCamera.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.E)) // rotate right
                {
                    playerCamera.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.LeftControl)) // tilt up
                {
                    playerCamera.transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftAlt))  // tilt down
                {
                    playerCamera.transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
                }

                // moves selected cam placement object to current location of camera
                if(playerCamera.transform.position != camPlacement[selectedCamIndex].transform.position)
                {
                    camPlacement[selectedCamIndex].transform.position = playerCamera.transform.position;
                }
                // sets selected cam placement obect rotation to current camera rotation
                if (playerCamera.transform.rotation != camPlacement[selectedCamIndex].transform.rotation)
                {
                    camPlacement[selectedCamIndex].transform.rotation = playerCamera.transform.rotation;
                }

                // update UI text that shows current camera's position and rotation
                current_position_text.text = "Camera Position: (" + playerCamera.transform.position.x.ToString("F2") + ", " + playerCamera.transform.position.y.ToString("F2") + ", " + playerCamera.transform.position.z.ToString("F2") + ")";
                current_rotation_text.text = "Camera Rotation: (" + playerCamera.transform.eulerAngles.x.ToString("F2") + ", " + playerCamera.transform.eulerAngles.y.ToString("F2") + ", " + playerCamera.transform.eulerAngles.z.ToString("F2") + ")";
                
            }
        }
    }

    // Function to select a camera placement to edit its position/rotation
    public void selectCameraPlacement(int n) // index in array (first placement = 0)
    {
        if (selectedCamIndex != n)
        {
            // sets color of previously selected button in UI to white
            if(selectedCamIndex != n && selectedCamIndex != -1)
            {
                camSelectionButtons[selectedCamIndex].image.color = Color.white;
                characterController.enabled = false;
                //playerCamera[selectedCamIndex].GetComponent<Camera>().depth = -1;
            }

            // update index for currently selected cam placement
            selectedCamIndex = n;
            playerCamera.GetComponent<Camera>().depth = 0;

            // sets camera position/rotation to that of the selected cam placement
            playerCamera.transform.SetPositionAndRotation(camPlacement[n].transform.position, camPlacement[n].transform.rotation);

            // changes color of selected cam placement button to yellow
            camSelectionButtons[n].image.color = Color.yellow;

            characterController = playerCamera.GetComponent<CharacterController>();
            characterController.enabled = true;

            // switches to wide view
            previewWideCam();

            cancelEditCamPos();
            cancelEditCamRot();
        }
    }

    // Function to save/update camera placement settings
    public void saveCamPlacements()
    {
        // deletes saved extra placements
        if (PlayerPrefs.HasKey("numberOfCams"))
        {
            if (camPlacement.Count < PlayerPrefs.GetInt("numberOfCams"))
            {
                for (int i = camPlacement.Count; i < PlayerPrefs.GetInt("numberOfCams"); i++)
                {
                    PlayerPrefs.DeleteKey("xPosPlacement" + i.ToString());
                    PlayerPrefs.DeleteKey("yPosPlacement" + i.ToString());
                    PlayerPrefs.DeleteKey("zPosPlacement" + i.ToString());

                    PlayerPrefs.DeleteKey("xRotPlacement" + i.ToString());
                    PlayerPrefs.DeleteKey("yRotPlacement" + i.ToString());
                    PlayerPrefs.DeleteKey("zRotPlacement" + i.ToString());

                    PlayerPrefs.DeleteKey("fovcam" + i.ToString());
                    PlayerPrefs.DeleteKey("dphDist" + i.ToString());
                    PlayerPrefs.DeleteKey("dphIntensity" + i.ToString());
                }
            }
        }

        // saves position, rotation or each cam placement and their blur settings and wide view FoV
        for (int i = 0; i < camPlacement.Count; i++)
        {
            PlayerPrefs.SetFloat("xPosPlacement" + i.ToString(), camPlacement[i].transform.position.x);
            PlayerPrefs.SetFloat("yPosPlacement" + i.ToString(), camPlacement[i].transform.position.y);
            PlayerPrefs.SetFloat("zPosPlacement" + i.ToString(), camPlacement[i].transform.position.z);

            PlayerPrefs.SetFloat("xRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.x);
            PlayerPrefs.SetFloat("yRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.y);
            PlayerPrefs.SetFloat("zRotPlacement" + i.ToString(), camPlacement[i].transform.rotation.eulerAngles.z);

            PlayerPrefs.SetFloat("fovcam" + i.ToString(), wideCamFoVs[i]);
            PlayerPrefs.SetFloat("dphDist" + i.ToString(), dphDistances[i]);
            PlayerPrefs.SetFloat("dphIntensity" + i.ToString(), dphIntensities[i]);
        }

        // saved number of cam placements
        PlayerPrefs.SetInt("numberOfCams", camPlacement.Count);

    }

    // loads saved cam placement settings
    public void loadCamPlacement()
    {
        removeExtraPlacements();
        addMissingPlacements();
        for(int i = 0; i < PlayerPrefs.GetInt("numberOfCams"); i++)
        {
            var pos = new Vector3(PlayerPrefs.GetFloat("xPosPlacement" + i.ToString()), PlayerPrefs.GetFloat("yPosPlacement" + i.ToString()), PlayerPrefs.GetFloat("zPosPlacement" + i.ToString()));
            var rot = new Vector3(PlayerPrefs.GetFloat("xRotPlacement" + i.ToString()), PlayerPrefs.GetFloat("yRotPlacement" + i.ToString()), PlayerPrefs.GetFloat("zRotPlacement" + i.ToString()));
            camPlacement[i].transform.position = pos;
            camPlacement[i].transform.eulerAngles = rot;
            wideCamFoVs[i] = PlayerPrefs.GetFloat("fovcam" + i.ToString());
            dphDistances[i] = PlayerPrefs.GetFloat("dphDist" + i.ToString());
            dphIntensities[i] = PlayerPrefs.GetFloat("dphIntensity" + i.ToString());
        }
    }

    // Function to add missing placements in case they were deleted in edit mode but exited without saving changes
    public void addMissingPlacements()
    {
        if(camPlacement.Count < PlayerPrefs.GetInt("numberOfCams"))
        {
            int n = camPlacement.Count;

            for(int i = n; i < PlayerPrefs.GetInt("numberOfCams"); i++)
            {
                camPlacement.Add(new GameObject("Location " + (i+1).ToString()));

                GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, newButtonParent.transform);
                new_button.GetComponent<RectTransform>().anchoredPosition = camSelectionButtons[i - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(40, 0);
                new_button.name = "Select placement " + (i + 1).ToString() + " button";

                camSelectionButtons.Add(new_button.gameObject.GetComponent<Button>());

                int j = i;

                camSelectionButtons[i].gameObject.GetComponentInChildren<Text>().text = camSelectionButtons.Count.ToString();
                camSelectionButtons[i].onClick.AddListener(() => { int tmp = j; this.selectCameraPlacement(tmp); });

                addNewLocationButton.GetComponent<RectTransform>().anchoredPosition = addNewLocationButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(40, 0);

                wideCamFoVs.Add(70f);
                dphDistances.Add(15f);
                dphIntensities.Add(10f);
            }
        }
    }

    // Function to open camera placement editing mode
    public void openEditMode()
    {
        foreach(Button b in camSelectionButtons)
        {
            b.image.color = Color.white;
        }

        // exits from missile/target position editing mode
        if (editMissilePositionManager.moving || editTargetPositionManager.moving)
        {
            if (editMissilePositionManager.moving)
            {
                editMissilePositionManager.exitMissilePositionEditMode();
            }

            if (editTargetPositionManager.moving)
            {
                editTargetPositionManager.exitTargetPositionEditMode();
            }

            missileEditPanelObj.SetActive(false);
            missilePositionKeyboardControlsObj.SetActive(false);
            openEditMissilePositinModeButtonObj.SetActive(true);

            targetEditPanelObj.SetActive(false);
            targetPositionKeyboardControlsObj.SetActive(false);
            openEditTargetPositinModeButtonObj.SetActive(true);
        }

        resetTransform();
        characterController = playerCamera.GetComponent<CharacterController>();
        editingMode = true;
        cameraManager.lock_on = false;
        selectCameraPlacement(selectedCamIndex);
        previewWideCam();
        camSelectionButtons[selectedCamIndex].image.color = Color.yellow;
        characterController.enabled = true;
    }

    // Function to exit cam placement editing mode
    public void exitEditMode()
    {
        selectedCamIndex = -1;

        /*foreach(GameObject cam in playerCamera)
        {
            cam.GetComponent<Camera>().depth = -1;
        }*/
        /*for (int i = 0; i < cameraManager.camButtons.Count; i++)
        {
            cameraManager.camButtons[i].image.color = Color.white;
        }*/

        resetTransform(); // delete unsaved changes

        characterController.enabled = false;
        editingMode = false;

        cameraManager.createCamSelectionButtons();

    }

    // Function to update the position/rotation of currently selected cam placement object to that of the camera
    public void setTransform()
    {
        if (selectedCamIndex > -1)
        {
            camPlacement[selectedCamIndex].transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.transform.rotation);
        }
    }

    // Function to reset camera placements to that of the saved settings
    public void resetTransform()
    {
        cancelEditCamPos();
        cancelEditCamRot();
        loadCamPlacement();
        if (selectedCamIndex > -1 && selectedCamIndex < PlayerPrefs.GetInt("numberOfCams")) 
        {
            playerCamera.transform.SetPositionAndRotation(camPlacement[selectedCamIndex].transform.position, camPlacement[selectedCamIndex].transform.rotation);
            previewWideCam();
        }
        else
        {
            selectedCamIndex = -1;
            selectCameraPlacement(0);
        }

        if (camPlacement.Count < 5)
        {
            addNewLocationButton.gameObject.SetActive(true);
        }
        if(camPlacement.Count == 5)
        {
            addNewLocationButton.gameObject.SetActive(false);
        }

        if(camPlacement.Count > 1)
        {
            removeLocationButton.gameObject.SetActive(true);
        }
        if(camPlacement.Count == 1)
        {
            removeLocationButton.gameObject.SetActive(false);
        }
    }

    // Function to change the camera's FoV to wide view mode FoV
    public void previewWideCam()
    {
        playerCamera.transform.eulerAngles = camPlacement[selectedCamIndex].transform.eulerAngles;

        wideViewButton.gameObject.SetActive(false);
        focusedViewButton.gameObject.SetActive(true);
        fovpanel.SetActive(true);

        fovSlider.value = wideCamFoVs[selectedCamIndex];
        playerCamera.GetComponent<Camera>().fieldOfView = fovSlider.value;
        FoVInputField.text = wideCamFoVs[selectedCamIndex].ToString();

        dphDistSlider.value = dphDistances[selectedCamIndex];
        dphIntensitySlider.value = dphIntensities[selectedCamIndex];
        DepthOfField dph;
        if (postProcessingVolume.profile.TryGet<DepthOfField>(out dph))
        {
            dph.focusDistance.value = dphDistSlider.value;
            dph.focalLength.value = dphIntensitySlider.value;
        }
        dphDistInputField.text = dphDistances[selectedCamIndex].ToString();
        dphIntensityInputField.text = dphIntensities[selectedCamIndex].ToString();
    }

    // Function to change the camera's FoV to how it would look in focused mode
    public void previewFocusedCam()
    {
        playerCamera.transform.LookAt(cameraManager.missile_obj.transform);
        playerCamera.GetComponent<Camera>().fieldOfView = cameraManager.GetFieldOfView(cameraManager.missile_obj.transform.position, cameraManager.focusedCamMissileHeight, playerCamera.GetComponent<Camera>());
        focusedViewButton.gameObject.SetActive(false);
        wideViewButton.gameObject.SetActive(true);
        fovpanel.SetActive(false);
    }

    // Function to edit that current cam placement's wide FoV based on the slider
    public void changeWideFoV()
    {
        playerCamera.GetComponent<Camera>().fieldOfView = fovSlider.value;
        if(selectedCamIndex != -1)
        {
            wideCamFoVs[selectedCamIndex] = fovSlider.value;
        }
        FoVInputField.text = fovSlider.value.ToString();
    }

    // function to edit the FoV of the wide view cam based on the input field input
    public void updateFoV()
    {
        int.TryParse(FoVInputField.text, out int fov);

        float newfov = Mathf.Clamp(fov, fovSlider.minValue, fovSlider.maxValue);

        //playerCamera[selectedCamIndex].GetComponent<Camera>().fieldOfView = newfov;

        playerCamera.GetComponent<Camera>().fieldOfView = newfov;

        fovSlider.value = newfov;
        FoVInputField.text = newfov.ToString();
    }

    // Function to edit the blur distance for the current cam placement based on the slider
    public void changeBlurDistance()
    {
        DepthOfField dph;

        if(postProcessingVolume.profile.TryGet<DepthOfField>(out dph))
        {
            dph.focusDistance.value = dphDistSlider.value;
        }

        if (selectedCamIndex != -1)
        {
            dphDistances[selectedCamIndex] = dph.focusDistance.value;
        }
        dphDistInputField.text = dphDistSlider.value.ToString();
    }

    // Function to edit the blur distance for the current cam placement based on the input field
    public void updateBlurDistance()
    {
        float.TryParse(dphDistInputField.text, out float dphdist);

        float newphdDist = Mathf.Clamp(dphdist, dphDistSlider.minValue, dphDistSlider.maxValue);

        dphDistSlider.value = newphdDist;
        dphDistInputField.text = newphdDist.ToString();
    }

    // Function to edit the blur intensity for the current cam placement based on the slider
    public void changeBlurIntensity()
    {
        DepthOfField dph;

        if (postProcessingVolume.profile.TryGet<DepthOfField>(out dph))
        {
            dph.focalLength.value = dphIntensitySlider.value;
        }

        if (selectedCamIndex != -1)
        {
            dphIntensities[selectedCamIndex] = dph.focalLength.value;
        }
        dphIntensityInputField.text = dphIntensitySlider.value.ToString();
    }

    // Function to edit the blur intensity for the current cam placement based on the input field
    public void updateBlurIntensity()
    {
        float.TryParse(dphIntensityInputField.text, out float dphInt);

        float newphdInt = Mathf.Clamp(dphInt, dphIntensitySlider.minValue, dphIntensitySlider.maxValue);

        dphIntensitySlider.value = newphdInt;
        dphIntensityInputField.text = newphdInt.ToString();
    }

    // Function to add new cam placement
    public void addNewCamPlacement()
    {
        int newButtonIndex = camSelectionButtons.Count;

        // create new UI button - start
        GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, newButtonParent.transform);
        new_button.GetComponent<RectTransform>().anchoredPosition = camSelectionButtons[newButtonIndex - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(40, 0);
        new_button.name = "Select placement " + (newButtonIndex + 1).ToString() + " button";

        camSelectionButtons.Add(new_button.gameObject.GetComponent<Button>());

        camSelectionButtons[newButtonIndex].gameObject.GetComponentInChildren<Text>().text = camSelectionButtons.Count.ToString();
        camSelectionButtons[newButtonIndex].onClick.AddListener(() => { int tmp = newButtonIndex; this.selectCameraPlacement(tmp); });

        addNewLocationButton.GetComponent<RectTransform>().anchoredPosition = addNewLocationButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(40, 0);
        // create new UI button - end

        // create new cam placement game object
        camPlacement.Add(new GameObject("Location " + (newButtonIndex + 1).ToString()));
        camPlacement[newButtonIndex].transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.transform.rotation);

        // add new wide cam FoV to list
        wideCamFoVs.Add(70f);

        // add new blur distance to list
        dphDistances.Add(15f);

        // add new blur intensity to list
        dphIntensities.Add(10f);

        //playerCamera.Add(Instantiate(playerCamPrefab));
        //playerCamera[newButtonIndex].transform.SetPositionAndRotation(playerCamera[selectedCamIndex].transform.position, playerCamera[selectedCamIndex].transform.rotation);

        selectCameraPlacement(newButtonIndex);

        if(newButtonIndex == 4) // max. 5 cam placements (can be changed)
        {
            addNewLocationButton.gameObject.SetActive(false);
        }

        if (!removeLocationButton.gameObject.activeInHierarchy)
        {
            removeLocationButton.gameObject.SetActive(true);
        }


    }

    // Function to remove the currently selected camera placement
    public void removeCamPlacement()
    {
        int camToRemove = selectedCamIndex;

        if (camToRemove != -1)
        {
            selectedCamIndex = -1;
            
            // shifts cam placement buttons after the deleted placement to the left
            for(int i = camToRemove + 1; i < camSelectionButtons.Count; i++)
            {
                camSelectionButtons[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(40, 0);
                camSelectionButtons[i].GetComponentInChildren<Text>().text = i.ToString();
                camSelectionButtons[i].onClick.RemoveAllListeners();

                int n = i - 1;

                // fixes the subsequent buttons and their on click events
                camSelectionButtons[i].onClick.AddListener(() => { int tmp = n; this.selectCameraPlacement(tmp); });

                camSelectionButtons[i].name = "Select placement " + i.ToString() + " button";

                camPlacement[i].name = "Location " + i.ToString();
            }

            // destroys the currently selected cam placement and its other elements
            Destroy(camPlacement[camToRemove], 1f);
            camPlacement.RemoveAt(camToRemove);

            addNewLocationButton.GetComponent<RectTransform>().anchoredPosition -= new Vector2(40, 0);

            Destroy(camSelectionButtons[camToRemove].gameObject);
            camSelectionButtons.RemoveAt(camToRemove);


            wideCamFoVs.RemoveAt(camToRemove);
            dphDistances.RemoveAt(camToRemove);
            dphIntensities.RemoveAt(camToRemove);

            // switches to the cam placement 1
            selectCameraPlacement(0);

            if (camPlacement.Count < 5)
            {
                addNewLocationButton.gameObject.SetActive(true);
            }
            if (camPlacement.Count == 1)
            {
                removeLocationButton.gameObject.SetActive(false);
            }
        }

    }

    // Function to remove any extra cam placements and its other elements in case user exits edit mode without saving changes
    public void removeExtraPlacements()
    {
        if (PlayerPrefs.GetInt("numberOfCams") < camPlacement.Count)
        {
            int cam_count = camPlacement.Count;
            for (int i = cam_count - 1; i >= PlayerPrefs.GetInt("numberOfCams"); i--)
            {
                /*GameObject temp = playerCamera[i];
                playerCamera.RemoveAt(i);
                Destroy(temp);*/

                GameObject temp = camSelectionButtons[i].gameObject;
                camSelectionButtons.RemoveAt(i);
                Destroy(temp);

                temp = camPlacement[i];
                camPlacement.RemoveAt(i);
                Destroy(temp);

                wideCamFoVs.RemoveAt(i);
                dphDistances.RemoveAt(i);
                dphIntensities.RemoveAt(i);

                addNewLocationButton.GetComponent<RectTransform>().anchoredPosition = addNewLocationButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(-40, 0);
            }
        }
    }

    public void startEditingCamPos()
    {
        x_pos_inputField.text = playerCamera.transform.position.x.ToString("F2");
        y_pos_inputField.text = playerCamera.transform.position.y.ToString("F2");
        z_pos_inputField.text = playerCamera.transform.position.z.ToString("F2");
    }

    public void cancelEditCamPos()
    {
        edit_pos_button.gameObject.SetActive(true);
        x_pos_inputField.transform.parent.gameObject.SetActive(false);
    }

    public void setCamPos()
    {
        float.TryParse(x_pos_inputField.text, out float x_pos);
        float.TryParse(y_pos_inputField.text, out float y_pos);
        float.TryParse(z_pos_inputField.text, out float z_pos);

        if(x_pos > 1000)
        {
            x_pos = 1000;
        }
        if (x_pos < -1000)
        {
            x_pos = -1000;
        }
        if (z_pos > 1000)
        {
            z_pos = 1000;
        }
        if (z_pos < -1000)
        {
            z_pos = -1000;
        }
       
        if (y_pos < -15)
        {
            y_pos = -15;
        }

        playerCamera.transform.position = new Vector3(x_pos, y_pos, z_pos);
    }

    public void startEditingCamRot()
    {
        x_rot_inputField.text = playerCamera.transform.eulerAngles.x.ToString("F2");
        y_rot_inputField.text = playerCamera.transform.eulerAngles.y.ToString("F2");
        z_rot_inputField.text = playerCamera.transform.eulerAngles.z.ToString("F2");
    }

    public void cancelEditCamRot()
    {
        edit_rot_button.gameObject.SetActive(true);
        x_rot_inputField.transform.parent.gameObject.SetActive(false);
    }

    public void setCamRot()
    {
        float.TryParse(x_rot_inputField.text, out float x_rot);
        float.TryParse(y_rot_inputField.text, out float y_rot);
        float.TryParse(z_rot_inputField.text, out float z_rot);

        playerCamera.transform.eulerAngles = new Vector3(x_rot, y_rot, z_rot);
    }
}
