using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    // reference to SkyboxDefinition which controls sky and time of day
    [SerializeField] SkyboxDefinitionScriptableObject skyboxDefinition;

    // UI elements
    [SerializeField] InputField input_hour;
    [SerializeField] InputField input_minutes;
    [SerializeField] Text hour_text;
    [SerializeField] Text minute_text;

    public int time_hour; // hour of the day (24 hour clock)
    public int time_minutes;

    public float current_time;

    void Start()
    {
        // sets the time of day at the beginning
        time_hour = Mathf.Clamp(time_hour, 0, 23);
        time_minutes = Mathf.Clamp(time_minutes, 0, 59);
        current_time = Mathf.Clamp(time_hour + time_minutes / 60f, 0f, 24f);
        skyboxDefinition.timeOfDay = current_time;

        input_hour.text = time_hour.ToString();
        input_minutes.text = time_minutes.ToString();
    }

    // Function to change the time of the day based on the input fields
    public void UpdateTimeOfDay()
    {
        int.TryParse(input_hour.text, out int hour);
        int.TryParse(input_minutes.text, out int minutes);

        // cannot exceed 24 hours
        time_hour = Mathf.Clamp(hour, 0, 23);
        // cannot exceed 60 minutes
        time_minutes = Mathf.Clamp(minutes, 0, 59);

        skyboxDefinition.timeOfDay = Mathf.Clamp(time_hour + time_minutes / 60f, 0f, 24f);

        hour_text.text = time_hour.ToString();
        minute_text.text = time_minutes.ToString();
    }
}
