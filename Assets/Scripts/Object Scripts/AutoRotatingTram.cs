using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotationTram : MonoBehaviour
{
    private float rotationSpeed = 0.1f;
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
