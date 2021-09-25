using System;
using UnityEngine;
using UnityEngine.Perception.GroundTruth;

[RequireComponent(typeof(PerceptionCamera))]
public class CustomAnnotationAndMetricReporter : MonoBehaviour
{
    //[Serializable]
    //public struct BoxData
    //{
    //    /// <summary>
    //    /// Integer identifier of the label
    //    /// </summary>
    //    //public int label_id;
    //    /// <summary>
    //    /// String identifier of the label
    //    /// </summary>
    //    //public string label_name;
    //}

    //public GameObject targetLight;
    //public GameObject camera; //not yet implemented. Possibly need both camera's nad target's position. For now, just target

    //MetricDefinition lightMetricDefinition;
    //AnnotationDefinition boundingBoxAnnotationDefinition;
    //SensorHandle cameraSensorHandle;
    //BoundingBox3DLabeler boundingBox3DLabeler;

    public GameObject missile;
    Camera cam;
    Quaternion v;
    Vector3 cameraRot;

    MetricDefinition missileMetricDefinition;
    AnnotationDefinition targetPositionAnnotationDefinition;
    AnnotationDefinition targetRotationAnnotationDefinition;
    AnnotationDefinition cameraRotationDefiniation;
    AnnotationDefinition cameraFOVAnnotationDefinition;
    AnnotationDefinition cameraFocalLengthAnnotationDefinition;
    AnnotationDefinition cameraSensorSizeAnnotationDefinition;
    AnnotationDefinition cameraLensShiftAnnotationDefinition;
    AnnotationDefinition cameraGateFitAnnotationDefinition;

    public void Start()
    {
        //Metrics and annotations are registered up-front
        //lightMetricDefinition = DatasetCapture.RegisterMetricDefinition(
        //    "Light position",
        //    "The world-space position of the light",
        //    Guid.Parse("1F6BFF46-F884-4CC5-A878-DB987278FE35"));
        //boundingBoxAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
        //    "Target bounding box",
        //    "The position of the target in the camera's local space",
        //    id: Guid.Parse("C0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        gameObject.GetComponent<Camera>().usePhysicalProperties = true; //needed to properly record focal length
        cam = GetComponent<Camera>();
        //Missile's World Quaternion
        missileMetricDefinition = DatasetCapture.RegisterMetricDefinition(
            "Missile's World Quaternion",
            "The world-quaternion of the missile",
            Guid.Parse("2F6BFF46-F884-4CC5-A878-DB987278FE35"));
        //Missile's World Position
        targetPositionAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Target bounding box position",
            "The position of the target in Unity's World Space",
            id: Guid.Parse("D0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Missile's World Rotation in eulerAngle
        targetRotationAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Target bounding box rotation",
            "The rotation of the target in Unity's World Space",
            id: Guid.Parse("E0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's Rotation value
        cameraRotationDefiniation = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's Rotation",
            "Camera's Rotation value",
            id: Guid.Parse("B0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's FOV value
        cameraFOVAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's FOV",
            "The field of view of the camera in degrees",
            id: Guid.Parse("F0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's Focal Length
        cameraFocalLengthAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's Focal Length",
            "The camera focal length, expressed in millimeters. To use this property, enable UsePhysicalProperties",
            id: Guid.Parse("A0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's Sensor Size
        cameraSensorSizeAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's Sensor Size",
            "The size of the camera sensor, expressed in millimeters",
            id: Guid.Parse("C0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's Lens Shift
        cameraLensShiftAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's Lens Shift",
            "The lens offset of the camera. The lens shift is relative to the sensor size. For example, a lens shift of 0.5 offsets the sensor by half its horizontal size",
            id: Guid.Parse("A1B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's Gate Fit
        cameraGateFitAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's Gate Fit",
            "There are two gates for a camera, the sensor gate and the resolution gate. The physical camera sensor gate is defined by the sensorSize property, the resolution gate is defined by the render target area",
            id: Guid.Parse("B1B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));

        //int testID = 10009;
        //string testName = "Tester";
        //BoxData testData = ConvertToBoxData(testID, testName);
        //camera = gameObject;
    }

    //private BoxData ConvertToBoxData(int id, string name)
    //{
    //    return new BoxData
    //    {
    //        label_id = id,
    //        label_name = name
    //    };
    //}
    public void LateUpdate()
    {
        if(missile == true)
        {
            //missiles quaternion
            v = Quaternion.Euler(missile.transform.eulerAngles);

            //Writing missile's quaternion to file
            DatasetCapture.ReportMetric(missileMetricDefinition,
                $@"[{{ ""x"": {v.x}, ""y"": {v.y}, ""z"": {v.z}, ""w"": {v.w} }}]");

            //missiles position
            Vector3 targetPos = missile.transform.position;

            //missiles rotation
            Vector3 targetRot = missile.transform.eulerAngles;

            //camera's field of view - "The field of view of the camera in degrees."
            double cameraFOV = cam.fieldOfView;

            //camera's focallength - "The camera focal length, expressed in millimeters. To use this property, enable UsePhysicalProperties"
            double cameraFL = cam.focalLength;

            //camera's sensor size - "The size of the camera sensor, expressed in millimeters."
            Vector2 camSensorSize = cam.sensorSize;

            //camera's lens shift - "The lens offset of the camera. The lens shift is relative to the sensor size. For example, a lens shift of 0.5 offsets the sensor by half its horizontal size."
            Vector2 camLensShift = cam.lensShift;

            //Camera's Gate Fit - "There are two gates for a camera, the sensor gate and the resolution gate. The physical camera sensor gate is defined by the sensorSize property, the resolution gate is defined by the render target area"
            string camGateFit = cam.gateFit.ToString();

            //camera's rotation in vector3
            cameraRot = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);

            //Report using the PerceptionCamera's SensorHandle if scheduled this frame
            var sensorHandle = GetComponent<PerceptionCamera>().SensorHandle;
            if (sensorHandle.ShouldCaptureThisFrame)
            {
                sensorHandle.ReportAnnotationValues(
                    targetPositionAnnotationDefinition,
                    new[] { targetPos });
                sensorHandle.ReportAnnotationValues(
                    targetRotationAnnotationDefinition,
                    new[] { targetRot });
                sensorHandle.ReportAnnotationValues(
                    cameraFOVAnnotationDefinition,
                    new[] { cameraFOV });
                sensorHandle.ReportAnnotationValues(
                    cameraFocalLengthAnnotationDefinition,
                    new[] { cameraFL });
                sensorHandle.ReportAnnotationValues(
                    cameraRotationDefiniation,
                    new[] { cameraRot });
                sensorHandle.ReportAnnotationValues(
                    cameraSensorSizeAnnotationDefinition,
                    new[] { camSensorSize });
                sensorHandle.ReportAnnotationValues(
                    cameraLensShiftAnnotationDefinition,
                    new[] { camLensShift });
                sensorHandle.ReportAnnotationValues(
                    cameraGateFitAnnotationDefinition,
                    new[] { camGateFit });
            }

            //Debug.Log("Camera's Lens Shift: " + camLensShift);
            //Debug.Log("Camera's Gate Fit: " + camGateFit);

        }

        //Debug.Log("Target01 Rotation: " + target.transform.eulerAngles + "\nTarget02 Rotation: " + target2.transform.eulerAngles);

        //Vector3 targetPos = target.transform.position;
        //var boundingBox = GetComponent<BoundingBox3DLabeler>();
        //Debug.Log(boundingBox.idLabelConfig);
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
