using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherController : MonoBehaviour
{
    // reference to SkyboxDefinition which controls sky and time of day
    [SerializeField] SkyboxDefinitionScriptableObject skyboxDefinition;

    // dropdown menu to select weather setting
    [SerializeField] Dropdown dropdown;

    public bool cloudy = false;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            changeWeather(dropdown);
        });
        if (!cloudy)
        {
            setClearsky(skyboxDefinition);
        }
        else
        {
            setCloudySky(skyboxDefinition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make clouds darker at night
        if(skyboxDefinition.timeOfDay >= 19.1 || skyboxDefinition.timeOfDay <= 6.1)
        {
            skyboxDefinition.cloudColor = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        }
        else
        {
            skyboxDefinition.cloudColor = Color.white;
        }
    }

    // Function to change weather based on dropdown menu
    public void changeWeather(Dropdown dd)
    {
        if(dd.value == 0)
        {
            setClearsky(skyboxDefinition);
        }
        if(dd.value == 1)
        {
            setCloudySky(skyboxDefinition);
        }
    }

    // Makes sky clear
    void setClearsky(SkyboxDefinitionScriptableObject sd)
    {
        sd.cloudOpacity = 0.7f;
        sd.cloudColor = Color.white;
        sd.cloudShadingSharpness = 0.35f;
        sd.cloudShadingStrength = 0.375f;
        sd.cloudThresholdRange = new Vector2(0.2f, 0.3f);
    }

    // Makes sky cloudy
    void setCloudySky(SkyboxDefinitionScriptableObject sd)
    {
        sd.cloudOpacity = 1f;
        sd.cloudColor = Color.gray;
        sd.cloudShadingSharpness = 1f;
        sd.cloudShadingStrength = 1f;
        sd.cloudThresholdRange = new Vector2(0f, 0f);
    }
}
