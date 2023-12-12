using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotating : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 0.1f, 0);
    }
}
