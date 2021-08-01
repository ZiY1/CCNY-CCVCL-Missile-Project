using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    //[SerializeField] SpawnRocket spawnRocket;
    [SerializeField] CameraPlacement cameraPlacement;

    [SerializeField] GameObject camSelectionButtonsParent;
    [SerializeField] GameObject camSelectionButtonPrefab;

    public List<GameObject> cameraLocations;

    public List<Button> camButtons;

    public GameObject missile_obj;

    public bool lock_on = true;

    public float focusedCamMissileHeight;
    public float wideCamMissileHeight;

    float missile_height;

    public int cam_mode = 0; // 0 - focused | 1 - wide

    public Button wideCamButton;
    public Button focusedCamButton;


    private void Start()
    {
        createCamSelectionButtons();
        missile_height = focusedCamMissileHeight;
        switchCamera(0);
        //missile_obj = spawnRocket.GetRocket();
    }

    void Update()
    {
        if (!cameraPlacement.editingMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                switchCamera(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (cameraLocations.Count > 1)
                {
                    switchCamera(1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (cameraLocations.Count > 2)
                {
                    switchCamera(2);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (cameraLocations.Count > 3)
                {
                    switchCamera(3);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (cameraLocations.Count > 4)
                {
                    switchCamera(4);
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (cam_mode == 0)
                {
                    switchToWideCam();
                }
                else if(cam_mode == 1)
                {
                    switchToFocusedCam();
                }
            }
        }

        if (lock_on)
        {
            //foreach(GameObject camera in cameraPlacement.playerCamera) {
                cam.GetComponent<Camera>().transform.LookAt(missile_obj.transform);

                cam.GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, cam.GetComponent<Camera>());
            //}
        }

    }

    public float GetFieldOfView(Vector3 objectPosition, float objectHeight, Camera c)
    {
        Vector3 diff = objectPosition - c.transform.position;
        float distance = Vector3.Dot(diff, c.transform.forward);
        float angle = Mathf.Atan((objectHeight * .5f) / distance);
        return angle * 2f * Mathf.Rad2Deg;
    }

    
    public void switchCamera(int i)
    {
        if (cameraPlacement.selectedCamIndex != i && cameraPlacement.selectedCamIndex != -1)
        {
            camButtons[cameraPlacement.selectedCamIndex].image.color = Color.white;
        }
        cam.transform.SetPositionAndRotation(cameraLocations[i].transform.position, cameraLocations[i].transform.rotation);
        //cam.depth = -1;
        //cam = cameraPlacement.playerCamera[i].GetComponent<Camera>();
        //cam.depth = 0;
        cameraPlacement.selectedCamIndex = i;
        camButtons[cameraPlacement.selectedCamIndex].image.color = Color.yellow;
        if(cam_mode == 0)
        {
            switchToFocusedCam();
        }
        else if(cam_mode == 1)
        {
            switchToWideCam();
        }
    }

    public void switchToWideCam()
    {
        lock_on = false;

        cam.transform.SetPositionAndRotation(cameraLocations[cameraPlacement.selectedCamIndex].transform.position, cameraLocations[cameraPlacement.selectedCamIndex].transform.rotation);

        missile_height = wideCamMissileHeight;
        cam.fieldOfView = PlayerPrefs.GetFloat("fovcam" + cameraPlacement.selectedCamIndex.ToString());

        cam_mode = 1;

        wideCamButton.gameObject.SetActive(false);
        focusedCamButton.gameObject.SetActive(true);

    }

    public void switchToFocusedCam()
    {
        lock_on = true;

        missile_height = focusedCamMissileHeight;

        cam_mode = 0;

        focusedCamButton.gameObject.SetActive(false);
        wideCamButton.gameObject.SetActive(true);
    }

    public void createCamSelectionButtons()
    {
        for(int i = 0; i < camButtons.Count; i++)
        {
            Destroy(camButtons[i].gameObject);
        }

        camButtons.Clear();
        cameraLocations.Clear();

        for (int i = 0; i < cameraPlacement.camPlacement.Count; i++)
        {
            GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, camSelectionButtonsParent.transform);
            
            int index = i;

            new_button.name = "Placement" + (i + 1).ToString();

            //Canvas.ForceUpdateCanvases();

            for(int j = 0; j < camButtons.Count; j++)
            {
                camButtons[j].GetComponent<RectTransform>().anchoredPosition += new Vector2(-40, 0);
            }
            if (i == 0)
            {
                new_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 15, 0);
            }
            else
            {
                new_button.GetComponent<RectTransform>().anchoredPosition = camButtons[i - 1].GetComponent<RectTransform>().localPosition + new Vector3(40, 0, 0);
            }

            camButtons.Add(new_button.GetComponent<Button>());

            cameraLocations.Add(cameraPlacement.camPlacement[i]);

            camButtons[i].GetComponentInChildren<Text>().text = (index + 1).ToString();
            camButtons[i].onClick.AddListener(() => { int tmp = index; this.switchCamera(tmp); });

            //camButtons[i].GetComponent<ButtonPressed>().cs = camButtons[0].GetComponent<CameraSettings>();
        }
        switchCamera(0);
    }

    /*public Transform GetSpawnRocketTransform()
    {
        return spawnRocket.transform;
    }*/
}
