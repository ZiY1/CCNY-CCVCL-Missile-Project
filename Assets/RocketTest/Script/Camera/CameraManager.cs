using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraManager : MonoBehaviour
{
    // main camera
    public List<Camera> cam;

    //[SerializeField] SpawnRocket spawnRocket;

    [SerializeField] CameraPlacement cameraPlacement;

    [SerializeField] GameObject camSelectionButtonsParent;
    [SerializeField] GameObject camSelectionButtonPrefab;

    public List<GameObject> cameraLocations;

    public List<Button> camButtons;

    

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
    public GameObject missile_obj;


    public List<bool> lock_on;

    // controls screen size of missile to adjust camera FoV
    public float focusedCamMissileHeight;
    public float wideCamMissileHeight;

    float missile_height;

    public Button wideCamButton;
    public Button focusedCamButton;


    private void Start()
    {
        // lock_on flags for each cam
        lock_on.Add(true);
        lock_on.Add(true);
        lock_on.Add(true);
        lock_on.Add(true);
        lock_on.Add(true);

        // Defualt missile - ziyi
        missile_obj = missile1;

        createCamSelectionButtons();

        foreach(Camera c in cam)
        {
            if(c)
                c.depth = -1;
        }

        missile_height = focusedCamMissileHeight;
        switchCamera(0);
        //missile_obj = spawnRocket.GetRocket();

        

    }

    //void LateUpdate()
    //{
    //    // rotates camera to follow missile movement & adjusts cam FoV to keep missile at a constant screen size
    //    if (lock_on)
    //    {
    //        cam.GetComponent<Camera>().transform.LookAt(missile_obj.transform);

    //        cam.GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam.GetComponent<Camera>());
    //    }

    //}

    public void LockOnToObject()
    {
        if (!cameraPlacement.editingMode)
        {
            if (lock_on[0] && cam[0].gameObject.activeSelf)
            {
                cam[0].GetComponent<Camera>().transform.LookAt(missile_obj.transform);
                cam[0].GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam[0].GetComponent<Camera>());
            }
            if (lock_on[1] && cam[1].gameObject.activeSelf)
            {
                cam[1].GetComponent<Camera>().transform.LookAt(missile_obj.transform);
                cam[1].GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam[1].GetComponent<Camera>());
            }
            if (lock_on[2] && cam[2].gameObject.activeSelf)
            {
                cam[2].GetComponent<Camera>().transform.LookAt(missile_obj.transform);
                cam[2].GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam[2].GetComponent<Camera>());
            }
            if (lock_on[3] && cam[3].gameObject.activeSelf)
            {
                cam[3].GetComponent<Camera>().transform.LookAt(missile_obj.transform);
                cam[3].GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam[3].GetComponent<Camera>());
            }
            if (lock_on[4] && cam[4].gameObject.activeSelf)
            {
                cam[4].GetComponent<Camera>().transform.LookAt(missile_obj.transform);
                cam[4].GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam[4].GetComponent<Camera>());
            }
        }
    }

    // Function to adjust cam FoV to keep missile at a constant screen size
    public float GetFieldOfView(Vector3 objectPosition, float objectHeight, Camera c)
    {
        Vector3 diff = objectPosition - c.transform.position;
        float distance = Vector3.Dot(diff, c.transform.forward);
        float angle = Mathf.Atan((objectHeight * .5f) / distance);
        return angle * 2f * Mathf.Rad2Deg;
    }

    
    // Function to change camera placement
    public void switchCamera(int i)
    {
        // change color of selection buttons to white
        if (cameraPlacement.selectedCamIndex != i && cameraPlacement.selectedCamIndex != -1)
        {
            camButtons[cameraPlacement.selectedCamIndex].image.color = Color.white;
            cam[cameraPlacement.selectedCamIndex].depth = -1;

        }

        // move camera to selected placement
        //cam.transform.SetPositionAndRotation(cameraLocations[i].transform.position, cameraLocations[i].transform.rotation);

        // Change the depth/priority of the cams to render (highest depth renders).
        cam[i].depth = 0;

        cameraPlacement.selectedCamIndex = i;

        // changes color of selected cam placement to yellow
        camButtons[cameraPlacement.selectedCamIndex].image.color = Color.yellow;

        if(lock_on[cameraPlacement.selectedCamIndex])
        {
            switchToFocusedCam();
        }
        else
        {
            switchToWideCam();
        }

        // adjusts blur to whatever is set for the selected camera placement
        DepthOfField dph;
        if (cameraPlacement.postProcessingVolume.profile.TryGet<DepthOfField>(out dph))
        {
            dph.focusDistance.value = PlayerPrefs.GetFloat("dphDist" + cameraPlacement.selectedCamIndex.ToString());
            dph.focalLength.value = PlayerPrefs.GetFloat("dphIntensity" + cameraPlacement.selectedCamIndex.ToString());
        }
    }

    // Function to switch camera mode to wide view
    // Camera orientation and FoV does not change with missile movement
    public void switchToWideCam()
    {
        // camera won't move
        lock_on[cameraPlacement.selectedCamIndex] = false;

        // set cam position and rotation to that of the selected cam placement
        cam[cameraPlacement.selectedCamIndex].transform.SetPositionAndRotation(cameraLocations[cameraPlacement.selectedCamIndex].transform.position, cameraLocations[cameraPlacement.selectedCamIndex].transform.rotation);

        //missile_height = wideCamMissileHeight;

        // sets cam FoV to corresponding wide camera FoV for the selected placement
        cam[cameraPlacement.selectedCamIndex].fieldOfView = PlayerPrefs.GetFloat("fovcam" + cameraPlacement.selectedCamIndex.ToString());

        wideCamButton.gameObject.SetActive(false);
        focusedCamButton.gameObject.SetActive(true);

    }

    // Function to switch camera mode to focused view
    // Camera orientation, and FoV WILL change to follow missile movement
    public void switchToFocusedCam()
    {
        lock_on[cameraPlacement.selectedCamIndex] = true;

        missile_height = focusedCamMissileHeight;

        focusedCamButton.gameObject.SetActive(false);
        wideCamButton.gameObject.SetActive(true);
    }

    // Creates UI buttons for each camera placement
    public void createCamSelectionButtons()
    {
        // destroy already existing buttons
        for(int i = 0; i < camButtons.Count; i++)
        {
            Destroy(camButtons[i].gameObject);
        }

        // clear lists
        camButtons.Clear();
        cameraLocations.Clear();

        // Creates a button in the UI for each camera placement
        for (int i = 0; i < cameraPlacement.camPlacement.Count; i++)
        {
            // instantiates button
            GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, camSelectionButtonsParent.transform);
            
            int index = i;

            new_button.name = "Placement" + (i + 1).ToString();

            // moves already existing buttons to the left
            for(int j = 0; j < camButtons.Count; j++)
            {
                camButtons[j].GetComponent<RectTransform>().anchoredPosition += new Vector2(-40, 0);
            }

            // set specific position in canvas if it is the first button
            if (i == 0)
            {
                new_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 15, 0);
            }
            else // set position of new button to be to the right of already existing buttons
            {
                new_button.GetComponent<RectTransform>().anchoredPosition = camButtons[i - 1].GetComponent<RectTransform>().localPosition + new Vector3(40, 0, 0);
            }

            // add button to the list
            camButtons.Add(new_button.GetComponent<Button>());

            // add cam placement to the list
            cameraLocations.Add(cameraPlacement.camPlacement[i]);

            // set the text of the button
            camButtons[i].GetComponentInChildren<Text>().text = (index + 1).ToString();

            // set the event that will happen when the button is clicked
            camButtons[i].onClick.AddListener(() => { int tmp = index; this.switchCamera(tmp); });

            //camButtons[i].GetComponent<ButtonPressed>().cs = camButtons[0].GetComponent<CameraSettings>();
        }
        switchCamera(0);
    }

    // Reassign missile_obj to active missile whenever dorpdown menu is clicked. - ziyi
    public void ReassignMissile()
    {
        if (missile1.activeSelf)
        {
            missile_obj = missile1;
        }
        else if (missile2.activeSelf)
        {
            missile_obj = missile2;
        }
        else if (missile3.activeSelf)
        {
            missile_obj = missile3;
        }
        else if (missile4.activeSelf)
        {
            missile_obj = missile4;
        }
        else if (missile5.activeSelf)
        {
            missile_obj = missile5;
        }
        else if (missile6.activeSelf)
        {
            missile_obj = missile6;
        }
        else if (missile7.activeSelf)
        {
            missile_obj = missile7;
        }
        else if (missile8.activeSelf)
        {
            missile_obj = missile8;
        }
        else if (missile9.activeSelf)
        {
            missile_obj = missile9;
        }
    }

    /*public Transform GetSpawnRocketTransform()
    {
        return spawnRocket.transform;
    }*/
}
