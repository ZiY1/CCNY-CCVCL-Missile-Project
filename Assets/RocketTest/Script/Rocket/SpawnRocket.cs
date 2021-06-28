using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnRocket : MonoBehaviour
{
    [TextArea] public string textArea = "Simply spawns a rocket with initial settings. Seperated in case we want to spawn more than one rocket for reasons(i.e. comparing parameters)";

    public GameObject rocketPrefab;
    //public GameObject[] rocketPlacement;
    //public GameObject[] platform;

    public GameObject spawn;
    public GameObject rocket;

    float spawnHeight = 50f;

    float timer = 0.5f;

    private void Awake()
    {
        //sphere = rocketPlacement[1].transform.GetChild(0).gameObject;

        //spawn = sphere.transform.Find("SpawnPoint").gameObject;
        Vector3 spawnPos = spawn.transform.position;
        spawnPos.y = spawnHeight;
        spawn.transform.position = spawnPos;

        rocket = Instantiate(rocketPrefab, spawn.transform.position, spawn.transform.rotation);
    }

    public GameObject GetRocket()
    {
        if(!rocket)
        {
            StartCoroutine(Delay(timer));
        }
        return rocket;
    }

    private IEnumerator Delay(float waitTime)
    {
        while (!rocket)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void DebugInfo(string debugText)
    {
        Debug.Log("(" + name + ") Debug: " + debugText);
    }

    private void Error(string errorText)
    {
        Debug.Log("(" + name + ") Error: " + errorText);
    }

}
