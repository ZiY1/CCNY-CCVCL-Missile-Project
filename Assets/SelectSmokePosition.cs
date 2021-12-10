using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmokePosition : MonoBehaviour
{
    public List<GameObject> missiles;
    public Dropdown positionSelectionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        setNear();
        positionSelectionDropdown.onValueChanged.AddListener(delegate
        {
            handleDropdownInput(positionSelectionDropdown);
        });
    }

    public void setNear()
    {
        foreach(GameObject m in missiles)
        {
            var smoke_trail = m.transform.GetChild(0);
            var near_pos = m.transform.GetChild(2).GetChild(0);
            smoke_trail.localPosition = near_pos.localPosition;
        }
    }

    public void setFar()
    {
        foreach (GameObject m in missiles)
        {
            var smoke_trail = m.transform.GetChild(0);
            var far_pos = m.transform.GetChild(2).GetChild(1);
            smoke_trail.localPosition = far_pos.localPosition;
        }
    }

    public void handleDropdownInput(Dropdown dd)
    {
        if (dd.value == 0) // near
        {
            setNear();
        }
        if (dd.value == 1) // far
        {
            setFar();
        }
    }
}
