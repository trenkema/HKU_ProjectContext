using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("r: " + GetComponent<Light>().color.r);
        Debug.Log("g: " + GetComponent<Light>().color.g);
        Debug.Log("b " + GetComponent<Light>().color.b);
    }
}
