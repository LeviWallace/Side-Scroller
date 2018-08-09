using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float batteryLife;
    public float batteryLifeSet;

    void Start()
    {
        Camera.main.layerCullSpherical = true;
        batteryLifeSet = batteryLife;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.rect = new Rect(new Vector2(0f + batteryLife, 0f + batteryLife), new Vector2(0f + batteryLife, batteryLife));
        batteryLife -= 0.001f;
    }
}
