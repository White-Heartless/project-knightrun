using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // make the bones rotate on Y axe

    public float rotationSpeed = 50f; // This will be the speed of rotation

    void Update()
    {
        // Rotate the object around the Y axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
