using UnityEngine;

public class LaunchOnClick : MonoBehaviour
{
    // TODO: Add more missiles when ready
    public GameObject missile1;
    public GameObject missile2;
    public GameObject missile3;
    public GameObject missile4;
    public GameObject missile5;
    public GameObject missile6;
    public GameObject missile7;
    public GameObject missile8;
    public GameObject missile9;

    private Projectile2 projectile;

    public StartPerception startPerception;

    public GameObject messageBox;
    public GameObject UI; 


    public void OnClick()
    {
        if (missile1.activeSelf)
        {
            projectile = (Projectile2)missile1.GetComponent(typeof(Projectile2));

        }
        else if (missile2.activeSelf)
        {
            projectile = (Projectile2)missile2.GetComponent(typeof(Projectile2));

        }
        else if (missile3.activeSelf)
        {
            projectile = (Projectile2)missile3.GetComponent(typeof(Projectile2));

        }
        else if (missile4.activeSelf)
        {
            projectile = (Projectile2)missile4.GetComponent(typeof(Projectile2));

        }
        else if (missile5.activeSelf)
        {
            projectile = (Projectile2)missile5.GetComponent(typeof(Projectile2));

        }
        else if (missile6.activeSelf)
        {
            projectile = (Projectile2)missile6.GetComponent(typeof(Projectile2));

        }
        else if (missile7.activeSelf)
        {
            projectile = (Projectile2)missile7.GetComponent(typeof(Projectile2));

        }
        else if (missile8.activeSelf)
        {
            projectile = (Projectile2)missile8.GetComponent(typeof(Projectile2));

        }
        else if (missile9.activeSelf)
        {
            projectile = (Projectile2)missile9.GetComponent(typeof(Projectile2));

        }
        projectile.Launch();
        if (projectile.getIsLaunchAngleValid())
        {
            startPerception.BeginCapture();
        }
        else
        {
            //Display message box
            messageBox.SetActive(true);
            UI.SetActive(false);

        }
    }
}