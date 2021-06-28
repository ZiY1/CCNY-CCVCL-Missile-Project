using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public int avgFrameRate;
    public Text display_Text;

    public void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        Debug.Log(fps + " FPS");
    }
}
