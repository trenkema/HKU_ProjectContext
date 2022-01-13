using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnterCheck : MonoBehaviour
{
    public Camera camera;

    private void Start()
    {
        
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            Debug.Log(objectHit.name);
            // Do something with the object that was hit by the raycast.
        }
    }
}
