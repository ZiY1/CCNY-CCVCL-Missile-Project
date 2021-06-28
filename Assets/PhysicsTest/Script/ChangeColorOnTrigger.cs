using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Color randomlySelectedColor = GetRandomColor();
        GetComponent<Renderer>().material.color = randomlySelectedColor;
    }

    private Color GetRandomColor()
    {
        return new Color
        (
            r: UnityEngine.Random.Range(0f, 1f),
            g: UnityEngine.Random.Range(0f, 1f),
            b: UnityEngine.Random.Range(0f, 1f),
            a: 0.25f
        );
    }
}
