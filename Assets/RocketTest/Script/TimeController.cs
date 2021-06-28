using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SkyboxDefinitionScriptableObject skyboxDefinition;
    [SerializeField] InputField input_hour;
    [SerializeField] InputField input_minutes;

    public int time_hour;
    public int time_minutes;
    public float current_time;

    void Start()
    {
        time_hour = Mathf.Clamp(time_hour, 0, 23);
        time_minutes = Mathf.Clamp(time_minutes, 0, 59);
        current_time = Mathf.Clamp(time_hour + time_minutes / 60f, 0f, 24f);
        skyboxDefinition.timeOfDay = current_time;

        input_hour.text = time_hour.ToString();
        input_minutes.text = time_minutes.ToString();
    }

    public void UpdateTimeOfDay()
    {
        int.TryParse(input_hour.text, out int hour);
        int.TryParse(input_minutes.text, out int minutes);

        time_hour = Mathf.Clamp(hour, 0, 23);
        time_minutes = Mathf.Clamp(minutes, 0, 59);

        skyboxDefinition.timeOfDay = Mathf.Clamp(hour + minutes / 60f, 0f, 24f);
    }
}
