using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Scenarios;
using UnityEngine.UI;
using Unity.Simulation;

public class PerceptionInterface : MonoBehaviour
{
    public FixedLengthScenario fls;

    public Text seedText;
    public Text totalIterationsText;

    public InputField seedInputfield;
    public InputField totalIterationsInputfield;

    uint newSeed;
    int newTotalIterations;

    public RawImage raw;
    public Camera cam;

    public List<RenderTexture> textureResolutions;

    /*private void Awake()
    {
        raw.texture = textureResolutions[0];
        cam.targetTexture = textureResolutions[0];
    }*/
    private void Start()
    {
        // sets folder where the dataset will be stored
        Configuration.localPersistentDataPath = Application.persistentDataPath;
        //Debug.Log(Application.persistentDataPath + "/Datasets");

        seedText.text = "\tSeed: " + fls.constants.randomSeed.ToString();
        totalIterationsText.text = "\tTotal Iterations: " + fls.constants.totalIterations.ToString();
    }

    // Function to open the scenario editing interface
    public void openEditScenarioSettings()
    {
        newSeed = fls.constants.randomSeed;
        newTotalIterations = fls.constants.totalIterations;

        seedInputfield.text = newSeed.ToString();
        totalIterationsInputfield.text = newTotalIterations.ToString();
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
        totalIterationsText.text = "\tTotal Iterations: " + fls.constants.totalIterations.ToString();
    }

    // Function to open file explorer where the folder containing the datasets is located
    public void ShowDatasetsFolder()
    {
        UnityEditor.EditorUtility.RevealInFinder(Application.persistentDataPath + "/.");
    }

}
