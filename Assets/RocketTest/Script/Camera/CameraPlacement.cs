using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraPlacement : MonoBehaviour
{
    public bool startWithEditorPlacements = false;
    [SerializeField] GameObject playerCamPrefab;
    //[SerializeField] public List<GameObject> playerCamera;
    [SerializeField] public GameObject playerCamera;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] CharacterController characterController;

    public List<GameObject> camPlacement;

    public List<Button> camSelectionButtons;
    public Button wideViewButton;
    public Button focusedViewButton;

    public GameObject fovpanel;
    public Slider fovSlider;
    public InputField FoVInputField;
    public List<float> wideCamFoVs;
    

    public int selectedCamIndex = -1;

    public bool editingMode = false;
    public float moveSpeed = 50f;
    public float rotSpeed = 50f;

    
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

        loadCamPlacement();

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
            if(selectedCamIndex != n && selectedCamIndex != -1)
            {

                camSelectionButtons[selectedCamIndex].image.color = Color.white;
                characterController.enabled = false;
                //playerCamera[selectedCamIndex].GetComponent<Camera>().depth = -1;
            }

            selectedCamIndex = n;
            playerCamera.GetComponent<Camera>().depth = 0;

            playerCamera.transform.SetPositionAndRotation(camPlacement[n].transform.position, camPlacement[n].transform.rotation);
            camSelectionButtons[n].image.color = Color.yellow;

            characterController = playerCamera.GetComponent<CharacterController>();
            characterController.enabled = true;

            previewWideCam();
        }
    }

    public void saveCamPlacements()
    {
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

        PlayerPrefs.SetInt("numberOfCams", camPlacement.Count);

    }

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


    public void openEditMode()
    {
        foreach(Button b in camSelectionButtons)
        {
            b.image.color = Color.white;
        }

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
        resetTransform();

        characterController.enabled = false;
        editingMode = false;

        cameraManager.createCamSelectionButtons();

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

    public void previewFocusedCam()
    {
        playerCamera.transform.LookAt(cameraManager.missile_obj.transform);
        playerCamera.GetComponent<Camera>().fieldOfView = cameraManager.GetFieldOfView(cameraManager.missile_obj.transform.position, cameraManager.focusedCamMissileHeight, playerCamera.GetComponent<Camera>());
        focusedViewButton.gameObject.SetActive(false);
        wideViewButton.gameObject.SetActive(true);
        fovpanel.SetActive(false);
    }

    public void changeWideFoV()
    {
        playerCamera.GetComponent<Camera>().fieldOfView = fovSlider.value;
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

        //playerCamera[selectedCamIndex].GetComponent<Camera>().fieldOfView = newfov;

        playerCamera.GetComponent<Camera>().fieldOfView = newfov;

        fovSlider.value = newfov;
        FoVInputField.text = newfov.ToString();
    }

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

    public void updateBlurDistance()
    {
        float.TryParse(dphDistInputField.text, out float dphdist);

        float newphdDist = Mathf.Clamp(dphdist, dphDistSlider.minValue, dphDistSlider.maxValue);

        dphDistSlider.value = newphdDist;
        dphDistInputField.text = newphdDist.ToString();
    }

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

    public void updateBlurIntensity()
    {
        float.TryParse(dphIntensityInputField.text, out float dphInt);

        float newphdInt = Mathf.Clamp(dphInt, dphIntensitySlider.minValue, dphIntensitySlider.maxValue);

        dphIntensitySlider.value = newphdInt;
        dphIntensityInputField.text = newphdInt.ToString();
    }

    public void addNewCamPlacement()
    {
        int newButtonIndex = camSelectionButtons.Count;

        GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, newButtonParent.transform);
        new_button.GetComponent<RectTransform>().anchoredPosition = camSelectionButtons[newButtonIndex - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(40, 0);
        new_button.name = "Select placement " + (newButtonIndex + 1).ToString() + " button";

        camSelectionButtons.Add(new_button.gameObject.GetComponent<Button>());

        camSelectionButtons[newButtonIndex].gameObject.GetComponentInChildren<Text>().text = camSelectionButtons.Count.ToString();
        camSelectionButtons[newButtonIndex].onClick.AddListener(() => { int tmp = newButtonIndex; this.selectCameraPlacement(tmp); });

        addNewLocationButton.GetComponent<RectTransform>().anchoredPosition = addNewLocationButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(40, 0);

        camPlacement.Add(new GameObject("Location " + (newButtonIndex + 1).ToString()));
        camPlacement[newButtonIndex].transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.transform.rotation);

        wideCamFoVs.Add(70f);
        dphDistances.Add(15f);
        dphIntensities.Add(10f);

        //playerCamera.Add(Instantiate(playerCamPrefab));
        //playerCamera[newButtonIndex].transform.SetPositionAndRotation(playerCamera[selectedCamIndex].transform.position, playerCamera[selectedCamIndex].transform.rotation);

        selectCameraPlacement(newButtonIndex);

        if(newButtonIndex == 4)
        {
            addNewLocationButton.gameObject.SetActive(false);
        }

        if (!removeLocationButton.gameObject.activeInHierarchy)
        {
            removeLocationButton.gameObject.SetActive(true);
        }


    }

    public void removeCamPlacement()
    {
        int camToRemove = selectedCamIndex;

        if (camToRemove != -1)
        {
            selectedCamIndex = -1;
            
            for(int i = camToRemove + 1; i < camSelectionButtons.Count; i++)
            {
                camSelectionButtons[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(40, 0);
                camSelectionButtons[i].GetComponentInChildren<Text>().text = i.ToString();
                camSelectionButtons[i].onClick.RemoveAllListeners();

                int n = i - 1;

                camSelectionButtons[i].onClick.AddListener(() => { int tmp = n; this.selectCameraPlacement(tmp); });

                camSelectionButtons[i].name = "Select placement " + i.ToString() + " button";

                camPlacement[i].name = "Location " + i.ToString();
            }

            Destroy(camPlacement[camToRemove], 1f);
            camPlacement.RemoveAt(camToRemove);

            addNewLocationButton.GetComponent<RectTransform>().anchoredPosition -= new Vector2(40, 0);

            Destroy(camSelectionButtons[camToRemove].gameObject);
            camSelectionButtons.RemoveAt(camToRemove);


            wideCamFoVs.RemoveAt(camToRemove);
            dphDistances.RemoveAt(camToRemove);
            dphIntensities.RemoveAt(camToRemove);

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

}
