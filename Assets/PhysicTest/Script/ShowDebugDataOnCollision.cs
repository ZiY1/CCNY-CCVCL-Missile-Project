using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDebugDataOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider to access collision's data
        Debug.Log(message: "Impacted at " + collision.contacts[0].point);

        float rayDrawDistance = 5f;

        Debug.DrawRay
            (
                start: collision.contacts[0].point,
                dir: collision.contacts[0].normal * rayDrawDistance,
                Color.red,
                duration: 1f
            );
    }
}
