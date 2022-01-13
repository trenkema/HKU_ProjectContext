using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public Color beginColor;
    public Color EndColor;
    public float timeToChange;
    private Light lamp;

    private void Start()
    {
        lamp = GetComponent<Light>();
        lamp.color = beginColor;
    }

    // Update is called once per frame
    void Update()
    {
        lamp.color = Color.Lerp(beginColor, EndColor, Mathf.PingPong(Time.deltaTime / timeToChange, 1));
    }
}
