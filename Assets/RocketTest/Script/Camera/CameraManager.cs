using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] SpawnRocket spawnRocket;

    public GameObject[] cameraLocations;

    public GameObject missile_obj;

    bool lock_on = true;

    public float focusedCamMissileHeight;
    public float wideCamMissileHeight;

    float missile_height;

    private void Awake()
    {
        missile_height = focusedCamMissileHeight;
        cam.transform.SetPositionAndRotation(cameraLocations[0].transform.position, cameraLocations[0].transform.rotation);
    }

    private void Start()
    {
        missile_obj = spawnRocket.GetRocket();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchToFocusedCamera(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchToFocusedCamera(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchToWideCam(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchToWideCam(1);
        }

        if (lock_on)
        {
            cam.transform.LookAt(missile_obj.transform);

            cam.fieldOfView = GetFieldOfView(missile_obj.transform.position, missile_height);
        }

    }

    float GetFieldOfView(Vector3 objectPosition, float objectHeight)
    {
        Vector3 diff = objectPosition - Camera.main.transform.position;
        float distance = Vector3.Dot(diff, Camera.main.transform.forward);
        float angle = Mathf.Atan((objectHeight * .5f) / distance);
        return angle * 2f * Mathf.Rad2Deg;
    }

    public void switchToFocusedCamera(int i)
    {
        lock_on = true;
        cam.transform.SetPositionAndRotation(cameraLocations[i].transform.position, cameraLocations[i].transform.rotation);
        missile_height = focusedCamMissileHeight;
    }

    public void switchToWideCam(int i)
    {
        lock_on = false;

        cam.transform.LookAt(spawnRocket.spawn.transform);
        cam.transform.SetPositionAndRotation(cameraLocations[i].transform.position, cameraLocations[i].transform.rotation);

        missile_height = wideCamMissileHeight;
        cam.fieldOfView = GetFieldOfView(spawnRocket.spawn.transform.position, missile_height);

    }
}
