using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    void Update()
    {
        Vector3 euler = new Vector3(0, 0, -3);
        transform.Rotate(euler);
    }
}
