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
    Quaternion camQ;
    //Quaternion t;

    MetricDefinition missileMetricDefinition;
    AnnotationDefinition cameraFOVAnnotationDefinition;
    AnnotationDefinition cameraFocalLengthAnnotationDefinition;
    AnnotationDefinition targetPositionAnnotationDefinition;
    AnnotationDefinition targetRotationAnnotationDefinition;

    //testing testing 123

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
        //Camera's FOV value
        cameraFOVAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's FOV",
            "Camera's FOV value",
            id: Guid.Parse("F0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));
        //Camera's Focal Length
        cameraFocalLengthAnnotationDefinition = DatasetCapture.RegisterAnnotationDefinition(
            "Camera's Focal Length",
            "Camera's Focal Length value",
            id: Guid.Parse("A0B4A22C-0420-4D9F-BAFC-954B8F7B35A7"));



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
            v = Quaternion.Euler(missile.transform.eulerAngles);
            camQ = Quaternion.Euler(gameObject.transform.eulerAngles);
            //t = missile.transform.rotation;
            //Debug.Log("Quaternion.Euler: " + v);
            //Debug.Log("Quaternion: " + t); //t == v
            //Report the light's position by manually creating the json array string.
            //var lightPos = targetLight.transform.position;
            DatasetCapture.ReportMetric(missileMetricDefinition,
                $@"[{{ ""x"": {v.x}, ""y"": {v.y}, ""z"": {v.z}, ""w"": {v.w} }}]");
            //compute the location of the object in the camera's local space
            //Vector3 targetPos = transform.worldToLocalMatrix * target.transform.position;
            //just taking the gameobjects position, no camera position considered
            Vector3 targetPos = missile.transform.position;
            Vector3 targetRot = missile.transform.eulerAngles;
            double cameraFOV = cam.fieldOfView;
            double cameraFL = cam.focalLength;
            //Debug.Log("Camera's Focal Length: " + cameraFL);
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
            }

            Debug.Log("Camera's Quaternion: " + camQ);
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
