using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CreateCVS : MonoBehaviour
{
    public TextAsset text;

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/PerceptionTest/Data/test.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("TEst");
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
