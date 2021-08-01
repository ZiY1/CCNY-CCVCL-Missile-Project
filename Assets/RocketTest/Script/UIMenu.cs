using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    
    public GameObject UI;

    bool hidden = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hidden)
            {
                UI.gameObject.SetActive(true);
                hidden = false;
            }
        }
    }

    public void HideUI()
    {
        UI.gameObject.SetActive(false);
        hidden = true;
    }



}
