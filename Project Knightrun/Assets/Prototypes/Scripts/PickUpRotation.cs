using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotation : MonoBehaviour
{
    // make the picku[p rotate on Y axe

    public float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
