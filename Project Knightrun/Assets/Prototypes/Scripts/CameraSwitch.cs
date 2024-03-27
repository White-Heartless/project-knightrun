using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

public class CameraSwitch : MonoBehaviour
{
    //the game start in 3d so we get all the values
    private Vector3 startPos;
    private Quaternion startRot;
    //we set public the target 2d rotation so you can change the value to point it higher or closer to the target
    public Vector3 targetPosition;
    public Vector3 targetRotation;

    void Start()
    {
        //we get the values
        startPos = transform.position;
        startRot = transform.rotation;
    }

    [ProButton]
    public void CamSwitchTo2D()
    {
        StartCoroutine(MoveCameraTo2D());
    }
	
    [ProButton]
    public void CamSwitchTo3D()
    {
        StartCoroutine(MoveCameraTo3D());
    }

    IEnumerator MoveCameraTo2D()
    {
        //Set the time for T value
        float elapsedTime = 0;
        float moveDuration = 1f;

        while (elapsedTime < moveDuration)
        {
            //using Lerp method to make a smooth animation
            transform.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / moveDuration);
            //Lerp every value (x,y,z) to have a clean movement and deleting all the extra rotations
            transform.rotation = Quaternion.Euler(
                Mathf.LerpAngle(startRot.eulerAngles.x, targetRotation.x, elapsedTime / moveDuration),
                Mathf.LerpAngle(startRot.eulerAngles.y, targetRotation.y, elapsedTime / moveDuration),
                Mathf.LerpAngle(startRot.eulerAngles.z, targetRotation.z, elapsedTime / moveDuration)
            );
            //sync to time.deltatime
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotation);
    }

    //same thing as MoveCameraTo2D changing the target values to the start values
    IEnumerator MoveCameraTo3D()
    {
        float elapsedTime = 0;
        float moveDuration = 1f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, startPos, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Euler(
                Mathf.LerpAngle(targetRotation.x, startRot.eulerAngles.x, elapsedTime / moveDuration),
                Mathf.LerpAngle(targetRotation.y, startRot.eulerAngles.y, elapsedTime / moveDuration),
                Mathf.LerpAngle(targetRotation.z, startRot.eulerAngles.z, elapsedTime / moveDuration)
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        transform.rotation = startRot;
    }
}