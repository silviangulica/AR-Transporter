using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get system time
        DateTime time = DateTime.Now;
        // Get the hour
        int hour = time.Hour;
        // Get the minute
        int minute = time.Minute;

        // Modify time on the TextMesh Pro
        GetComponent<TMPro.TextMeshPro>().text = hour.ToString() + ":" + minute.ToString();
    }
}
