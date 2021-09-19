using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeasurements : MonoBehaviour
{
    public Vector3 size;
    private MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        size = renderer.bounds.size;
    }
}
