using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToShapePrior : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToShapePriorScene()
    {
        SceneManager.LoadScene("ShapePriorGenerator");
    }
}
