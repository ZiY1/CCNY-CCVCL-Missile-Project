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

    private void Start()
    {
        missile_height = focusedCamMissileHeight;
        switchToFocusedCamera(0);
        //missile_obj = spawnRocket.GetRocket();
        createCamSelectionButtons();
    }

    void Update()
    {
        if (!cameraPlacement.editingMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                switchToFocusedCamera(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                switchToFocusedCamera(1);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                switchToWideCam();
            }
        }

        if (lock_on)
        {
            foreach(GameObject camera in cameraPlacement.playerCamera) {
                camera.GetComponent<Camera>().transform.LookAt(missile_obj.transform);

                camera.GetComponent<Camera>().fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height, camera.GetComponent<Camera>());
            }
        }

    }

    public float GetFieldOfView(Vector3 objectPosition, float objectHeight, Camera c)
    {
        Vector3 diff = objectPosition - c.transform.position;
        float distance = Vector3.Dot(diff, c.transform.forward);
        float angle = Mathf.Atan((objectHeight * .5f) / distance);
        return angle * 2f * Mathf.Rad2Deg;
    }

    public void switchToFocusedCamera(int i)
    {
        if (cameraPlacement.selectedCamIndex != i && cameraPlacement.selectedCamIndex != -1)
        {
            camButtons[cameraPlacement.selectedCamIndex].image.color = Color.white;
        }
        lock_on = true;
        //cam.transform.SetPositionAndRotation(cameraLocations[i].transform.position, cameraLocations[i].transform.rotation);
        cam.depth = -1;
        cam = cameraPlacement.playerCamera[i].GetComponent<Camera>();
        cam.depth = 0;
        missile_height = focusedCamMissileHeight;
        cameraPlacement.selectedCamIndex = i;
        camButtons[cameraPlacement.selectedCamIndex].image.color = Color.yellow;

    }

    public void switchToWideCam()
    {
        lock_on = false;

        cam.transform.SetPositionAndRotation(cameraLocations[cameraPlacement.selectedCamIndex].transform.position, cameraLocations[cameraPlacement.selectedCamIndex].transform.rotation);

        missile_height = wideCamMissileHeight;
        cam.fieldOfView = PlayerPrefs.GetFloat("fovcam" + cameraPlacement.selectedCamIndex.ToString());

    }

    public void createCamSelectionButtons()
    {
        for(int i = camButtons.Count; i < PlayerPrefs.GetInt("numberOfCams"); i++)
        {
            GameObject new_button = GameObject.Instantiate(camSelectionButtonPrefab, camSelectionButtonsParent.transform);

            int index = i;

            new_button.name = "Placement" + (i + 1).ToString();

            for(int j = 0; j < camButtons.Count; j++)
            {
                camButtons[j].transform.position += new Vector3(-40, 0, 0);
            }
            new_button.transform.position = camButtons[i - 1].transform.position + new Vector3(40, 0, 0);

            camButtons.Add(new_button.GetComponent<Button>());

            cameraLocations.Add(cameraPlacement.camPlacement[i]);

            camButtons[i].GetComponentInChildren<Text>().text = camButtons.Count.ToString();
            camButtons[i].onClick.AddListener(() => { int tmp = index; this.switchToFocusedCamera(tmp); });

            camButtons[i].GetComponent<ButtonPressed>().cs = camButtons[0].GetComponent<CameraSettings>();
        }
    }

    public void destroyExtraButtons()
    {
        if(camButtons.Count > cameraPlacement.playerCamera.Count)
        {
            for(int i = cameraPlacement.playerCamera.Count; i < camButtons.Count; i++)
            {

            }
        }
    }

    /*public Transform GetSpawnRocketTransform()
    {
        return spawnRocket.transform;
    }*/
}
