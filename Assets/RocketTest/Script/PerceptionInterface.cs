using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Scenarios;
using UnityEngine.UI;
using Unity.Simulation;
using UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Samplers;

public class PerceptionInterface : MonoBehaviour
{
    public FixedLengthScenario fls;

    public Text seedText;

    public Text totalIterationsText;

    public Text rotRandXMin_Text;
    public Text rotRandYMin_Text;
    public Text rotRandZMin_Text;

    public Text rotRandXMax_Text;
    public Text rotRandYMax_Text;
    public Text rotRandZMax_Text;

    public Text dataSetDirectoyPath_Text;
    public Text copyPathButton_Text;

    public InputField seedInputfield;

    public InputField totalIterationsInputfield;

    public InputField rotRandXmin_Inputfield;
    public InputField rotRandXmax_Inputfield;

    public InputField rotRandYmin_Inputfield;
    public InputField rotRandYmax_Inputfield;

    public InputField rotRandZmin_Inputfield;
    public InputField rotRandZmax_Inputfield;

    uint newSeed;
    int newTotalIterations;

    float min_x = -10;
    float max_x = 10;
    float min_y = -10;
    float max_y = 10;
    float min_z = -10;
    float max_z = 10;

    float orig_min_x, orig_max_x, orig_min_y, orig_max_y, orig_min_z, orig_max_z;

    public Text rotRandomizerOffMsg;
    public InputField spinSpeedInputField;

    /*public RawImage raw;
    public Camera cam;

    public List<RenderTexture> textureResolutions;*/

    /*private void Awake()
    {
        raw.texture = textureResolutions[0];
        cam.targetTexture = textureResolutions[0];
    }*/
    private void Start()
    {
        // sets folder where the dataset will be stored
        Configuration.localPersistentDataPath = Application.persistentDataPath;

        dataSetDirectoyPath_Text.text = Configuration.localPersistentDataPath;
        //Debug.Log(Application.persistentDataPath + "/Datasets");

        seedText.text = "\tSeed: " + fls.constants.randomSeed.ToString();
        totalIterationsText.text = "\tNumber of Frames: " + fls.constants.totalIterations.ToString();

        rotRandXMin_Text.text = "\tX: min " + min_x.ToString(); rotRandXMax_Text.text = "\tmax " + max_x.ToString();
        rotRandYMin_Text.text = "\tY: min " + min_y.ToString(); rotRandYMax_Text.text = "\tmax " + max_y.ToString();
        rotRandZMin_Text.text = "\tZ: min " + min_z.ToString(); rotRandZMax_Text.text = "\tmax " + max_z.ToString();

        orig_min_x = min_x;
        orig_max_x = max_x;
        orig_min_y = min_y;
        orig_max_y = max_y;
        orig_min_z = min_z;
        orig_max_z = max_z;
    }

    // Function to open the scenario editing interface
    public void openEditScenarioSettings()
    {
        newSeed = fls.constants.randomSeed;
        newTotalIterations = fls.constants.totalIterations;

        seedInputfield.text = newSeed.ToString();
        totalIterationsInputfield.text = newTotalIterations.ToString();

        min_x = orig_min_x;
        min_y = orig_min_y;
        min_z = orig_min_z;

        max_x = orig_max_x;
        max_y = orig_max_y;
        max_z = orig_max_z;

        rotRandXmin_Inputfield.text = min_x.ToString();
        rotRandYmin_Inputfield.text = min_y.ToString();
        rotRandZmin_Inputfield.text = min_z.ToString();

        rotRandXmax_Inputfield.text = max_x.ToString();
        rotRandYmax_Inputfield.text = max_y.ToString();
        rotRandZmax_Inputfield.text = max_z.ToString();
    }

    // Function to read input for the scenario seed from the input field
    public void editSeed()
    {
        uint.TryParse(seedInputfield.text, out uint seed);
        newSeed = seed;
    }

    // Function to read input for the total iterations from the input field
    public void editTotalIterations()
    {
        int.TryParse(totalIterationsInputfield.text, out int totalIterations);
        newTotalIterations = totalIterations;
    }

    // Function to save changes made to scenario seed and total iterations
    public void save()
    {
        fls.constants.randomSeed = newSeed;
        fls.constants.totalIterations = newTotalIterations;

        seedText.text = "\tSeed: " + fls.constants.randomSeed.ToString();
        totalIterationsText.text = "\tNumber of Frames: " + fls.constants.totalIterations.ToString();

        fls.GetRandomizer<CustomRotationRandomizer>().rotation = new Vector3Parameter
        {
            x = new UniformSampler(min_x, max_x),
            y = new UniformSampler(min_y, max_y),
            z = new UniformSampler(min_z, max_z)
        };

        rotRandXMin_Text.text = "\tX: min " + min_x.ToString(); rotRandXMax_Text.text = "\tmax " + max_x.ToString();
        rotRandYMin_Text.text = "\tY: min " + min_y.ToString(); rotRandYMax_Text.text = "\tmax " + max_y.ToString();
        rotRandZMin_Text.text = "\tZ: min " + min_z.ToString(); rotRandZMax_Text.text = "\tmax " + max_z.ToString();

        orig_min_x = min_x;
        orig_max_x = max_x;
        orig_min_y = min_y;
        orig_max_y = max_y;
        orig_min_z = min_z;
        orig_max_z = max_z;
    }

    // Function to copy path where the folder containing the datasets is located
    public void ShowDatasetsFolder()
    {
        GUIUtility.systemCopyBuffer = Configuration.localPersistentDataPath;
        dataSetDirectoyPath_Text.text = Configuration.localPersistentDataPath;

        StopCoroutine("copyPathButtonTextChange");
        StartCoroutine("copyPathButtonTextChange");
    }

    IEnumerator copyPathButtonTextChange()
    {
        copyPathButton_Text.text = "Copied!";
        yield return new WaitForSeconds(5);
        copyPathButton_Text.text = "Copy path";

    }

    // Function to change the RotatinRandomizer x,y, and z values
    public void changeRotRandVals()
    {
        float.TryParse(rotRandXmin_Inputfield.text, out float new_minX);
        float.TryParse(rotRandYmin_Inputfield.text, out float new_minY);
        float.TryParse(rotRandZmin_Inputfield.text, out float new_minZ);

        float.TryParse(rotRandXmax_Inputfield.text, out float new_maxX);
        float.TryParse(rotRandYmax_Inputfield.text, out float new_maxY);
        float.TryParse(rotRandZmax_Inputfield.text, out float new_maxZ);

        min_x = new_minX;
        min_y = new_minY;
        min_z = new_minZ;

        max_x = new_maxX;
        max_y = new_maxY;
        max_z = new_maxZ;

    }

    public void toggleRotRandomizer(Toggle toggle)
    {
        fls.GetRandomizer<CustomRotationRandomizer>().enabled = toggle.isOn;
        rotRandomizerOffMsg.gameObject.SetActive(!toggle.isOn);

        spinSpeedInputField.transform.parent.gameObject.SetActive(!toggle.isOn);
        spinSpeedInputField.text = Projectile2.spinSpeed.ToString();
    }

}
