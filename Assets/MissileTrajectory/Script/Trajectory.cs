using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Trajectory : MonoBehaviour
{

    private ArrayList arrayListPosition;
    private ArrayList arrayListRotation;
    private ArrayList arrayListFoV;

    private static int counter = 0;
    private float timeInterval = 0.25f;
    private float nextTime = 0.0f;

    public Camera cam;

  // Start is called before the first frame update
    void Start()
    {

        // Initialize the two ArrayLists
        arrayListPosition = new ArrayList();
        arrayListRotation = new ArrayList();
        arrayListFoV = new ArrayList();

        ReadCSVFile();

    }

    // Read the corresponding position and rotation values, store them into an ArrayList
    void ReadCSVFile()
    {

        // All CSV files have been briefly modified where the column of number labeling was deleted

        // AiHua's first ground truth data
        //StreamReader strReader = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/target_annotation1.csv");
        //StreamReader strReader = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/3Dboundingbox1.csv");

        // AiHua's second ground truth data
        //StreamReader strReader = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/target_annotation2.csv");
        //StreamReader strReader = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/3Dboundingbox2.csv");

        // Anthony's ground truth data
        //StreamReader strReader = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/target_annotation3.csv");
        StreamReader strReader = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/3Dboundingbox.csv");

        StreamReader strReaderFoV = new StreamReader("Assets/MissileTrajectory/Ground Truth Data/ego_annotation_new.csv");


        bool endOfFile = false;

        while(!endOfFile)
        {

          // Read the CSV file line by line
          string data_String = strReader.ReadLine();

            string data_String_ego = strReaderFoV.ReadLine();


            // Skip the first row which is the column names
            if (counter < 1)
          {
              counter++;
              continue;
          }

          if (data_String == null)
          {

            endOfFile =  true;
            break;

          }

          // Split the entire line into individual column values
          var data_values = data_String.Split(',');

            var data_values_ego = data_String_ego.Split(',');


            /* Store the position values (x, y, z) as a Vector3 and add it to an ArrayList
               The indexes represent the column number of the position values (x, y, z) from the CSV files */
            arrayListPosition.Add(new Vector3(float.Parse(data_values[2].ToString()), float.Parse(data_values[3].ToString()), float.Parse(data_values[4].ToString())));

          /* Rotation using Euler Angles, multiple methods
             May not be the best idea to use Euler angles due to the possibility of Gimbal Lock, it is suggested to use Quaternion instead */

          // Method #1: For target annotation CSV files, store the Euler angles (x, y, z) as Quaternion and add it to an ArrayList
          //arrayListRotation.Add(Quaternion.Euler(new Vector3(float.Parse(data_values[4].ToString()), float.Parse(data_values[5].ToString()), float.Parse(data_values[6].ToString()))));

          // Method #1: For 3D Bounding Box CSV files, store the Euler angles (x, y, z) as Quaternion and add it to an ArrayList
          //arrayListRotation.Add(Quaternion.Euler(new Vector3(float.Parse(data_values[8].ToString()), float.Parse(data_values[9].ToString()), float.Parse(data_values[10].ToString()))));

          // Method #2: For target annotation CSV files, store the Euler angles (x, y, z) as Vector3 and add it to an ArrayList
          //arrayListRotation.Add(new Vector3(float.Parse(data_values[4].ToString()), float.Parse(data_values[5].ToString()), float.Parse(data_values[6].ToString())));

          // Method #2: For 3D Bounding Box CSV files, store the Euler angles (x, y, z) as Vector3 and add it to an ArrayList
          //arrayListRotation.Add(new Vector3(float.Parse(data_values[8].ToString()), float.Parse(data_values[9].ToString()), float.Parse(data_values[10].ToString())));

          /* Rotation using Quaternion */

          // For target annotation CSV files, store the Quaternion values (x, y, w, z) as Quaternion and add it to an ArrayList
          //arrayListRotation.Add(new Quaternion(float.Parse(data_values[7].ToString()), float.Parse(data_values[8].ToString()), float.Parse(data_values[9].ToString()), float.Parse(data_values[10].ToString())));

          // For 3D Bounding Box CSV files, store the Quaternion values (x, y, w, z) as Quaternion and add it to an ArrayList
          arrayListRotation.Add(new Quaternion(float.Parse(data_values[8].ToString()), float.Parse(data_values[9].ToString()), float.Parse(data_values[10].ToString()), float.Parse(data_values[11].ToString())));

            // get cam FoV from file
            arrayListFoV.Add(float.Parse(data_values_ego[11].ToString()));
            //Debug.Log(data_values_ego[11].ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {

            // Increment counter per frame, get the position of current frame, update gameObject's position
            if (counter < arrayListPosition.Count)
            {

                Vector3 updatePosition = (Vector3)arrayListPosition[counter];
                transform.position = updatePosition;

                /*
                // Method 1: Rotating using Euler Angles as Quaternion

                Quaternion updateQuaternion = (Quaternion)arrayListRotation[counter];
                transform.rotation = updateQuaternion;

                */

                /*
                // Method 2: Rotation using Euler Angles as Vector3
                Vector3 temp = (Vector3)arrayListRotation[counter];

                string[] rotationData = arrayListRotation[counter].ToString().Split(',');

                // Remove the '(' in the first value
                rotationData[0] = sdata[0].Replace("(", "");

                // Remove the ')' in the last value
                rotationData[2] = sdata[2].Replace(")", "");

                // Rotation using Euler Angles (x, y, z)
                transform.Rotate(float.Parse(sdata[0]), float.Parse(sdata[1]), float.Parse(sdata[2]), Space.World);
                */

                
                // Method 2: Rotation using Quaternion

                Quaternion tempQ = (Quaternion)arrayListRotation[counter];
                transform.rotation = tempQ;

                // change cam FoV
                float tempF = (float)arrayListFoV[counter];
                cam.fieldOfView = tempF;

                Debug.Log(counter);

                counter++;

            }

            nextTime += timeInterval;

        }

    }

}
