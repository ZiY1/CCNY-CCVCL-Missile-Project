using System;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;
//using System.Collections.Generic;

[RequireComponent(typeof(PerceptionCamera))]
public class CustomAnnotationAndMetricReporter : MonoBehaviour
{
    [Serializable]
    public struct MissileData
    {
        public string name;
        public Vector3 worldPosition;
        public Vector3 worldRotation;
        public float[] worldQuaterion;
    }
    [Serializable]
    public struct CameraData
    {
        public string name;
        public Vector3 worldRotation;
        public double FOV;
        public double focalLength;
        public Vector2 sensorSize;
        public Vector2 lensShift;
        public string gateFit;
    }

    public GameObject missile;
    Camera cam;
    Quaternion v;

    AnnotationDefinition missileDataAnnotationDefinition;
    AnnotationDefinition cameraDataAnnotationDefinition;

    public void Start()
    {
        gameObject.GetComponent<Camera>().usePhysicalProperties = true; //needed to properly record focal length
        cam = GetComponent<Camera>();
        
        cameraDataAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera Data",
            "Camera Data: World Rotation, Field of View, Focal Length, Sensor Size, Lens Shift, Gate Fit",
            id: Guid.Parse("E1B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Missile data all in one
        missileDataAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Missile Data",
            "Missile Data: World Rotation, World Position",
            id: Guid.Parse("D1B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));

    }

    public void LateUpdate()
    {
        if(missile == true)
        {
            CameraData cameraData;
            cameraData.name = cam.gameObject.name;
            cameraData.worldRotation = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            cameraData.FOV = cam.fieldOfView; //camera's field of view - "The field of view of the camera in degrees."
            cameraData.focalLength = cam.focalLength; //camera's focallength - "The camera focal length, expressed in millimeters. To use this property, enable UsePhysicalProperties"
            cameraData.sensorSize = cam.sensorSize; //camera's sensor size - "The size of the camera sensor, expressed in millimeters."
            cameraData.lensShift = cam.lensShift; //camera's lens shift - "The lens offset of the camera. The lens shift is relative to the sensor size. For example, a lens shift of 0.5 offsets the sensor by half its horizontal size."
            cameraData.gateFit = cam.gateFit.ToString(); //Camera's Gate Fit - "There are two gates for a camera, the sensor gate and the resolution gate. The physical camera sensor gate is defined by the sensorSize property, the resolution gate is defined by the render target area"

            //missiles quaternion
            v = Quaternion.Euler(missile.transform.eulerAngles);

            MissileData missileData;
            missileData.name = missile.name;
            missileData.worldPosition = missile.transform.position;
            missileData.worldRotation = missile.transform.eulerAngles;
            missileData.worldQuaterion = new float[4];
            missileData.worldQuaterion[0] = v.x;
            missileData.worldQuaterion[1] = v.y;
            missileData.worldQuaterion[2] = v.z;
            missileData.worldQuaterion[3] = v.w;

            //Report using the PerceptionCamera's SensorHandle if scheduled this frame
            var sensorHandle = GetComponent<PerceptionCamera>().SensorHandle;
            if (sensorHandle.ShouldCaptureThisFrame)
            {
                sensorHandle.ReportAnnotationValues(
                    cameraDataAnnotationDefinition,
                    new[] { cameraData });
                sensorHandle.ReportAnnotationValues(
                    missileDataAnnotationDefinition,
                    new[] { missileData });
            }

            //Debug.Log("Camera's Intrinsic: " + cam.projectionMatrix); ~values around 35-70 in testing.
        }
    }
}

// Example metric that is added each frame in the dataset:
// {
//   "capture_id": null,
//   "annotation_id": null,
//   "sequence_id": "9768671e-acea-4c9e-a670-0f2dba5afe12",
//   "step": 1,
//   "metric_definition": "1f6bff46-f884-4cc5-a878-db987278fe35",
//   "values": [{ "x": 96.1856, "y": 192.676, "z": -193.8386 }]
// },

// Example annotation that is added to each capture in the dataset:
// {
//   "id": "33f5a3aa-3e5e-48f1-8339-6cbd64ed4562",
//   "annotation_definition": "c0b4a22c-0420-4d9f-bafc-954b8f7b35a7",
//   "values": [
//     [
//       -1.03097284,
//       0.07265166,
//       -6.318692
//     ]
//   ]
// }
