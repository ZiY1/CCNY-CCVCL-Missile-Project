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

        Configuration.localPersistentDataPath = Application.persistentDataPath;
        //Debug.Log(Application.persistentDataPath + "/Datasets");

        seedText.text = "\tSeed: " + fls.constants.randomSeed.ToString();
        totalIterationsText.text = "\tTotal Iterations: " + fls.constants.totalIterations.ToString();
    }


    public void openEditScenarioSettings()
    {
        newSeed = fls.constants.randomSeed;
        newTotalIterations = fls.constants.totalIterations;

        seedInputfield.text = newSeed.ToString();
        totalIterationsInputfield.text = newTotalIterations.ToString();
    }
    public void editSeed()
    {
        uint.TryParse(seedInputfield.text, out uint seed);
        newSeed = seed;
    }

    public void editTotalIterations()
    {
        int.TryParse(totalIterationsInputfield.text, out int totalIterations);
        newTotalIterations = totalIterations;
    }

    public void save()
    {
        fls.constants.randomSeed = newSeed;
        fls.constants.totalIterations = newTotalIterations;

        seedText.text = "\tSeed: " + fls.constants.randomSeed.ToString();
        totalIterationsText.text = "\tTotal Iterations: " + fls.constants.totalIterations.ToString();
    }

    public void ShowDatasetsFolder()
    {
        UnityEditor.EditorUtility.RevealInFinder(Application.persistentDataPath + "/.");
    }

}
