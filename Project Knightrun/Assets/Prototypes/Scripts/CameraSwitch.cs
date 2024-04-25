using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

public class CameraSwitch : MonoBehaviour
{
    //the game start in 3d so we get all the values
    public Vector3 startPos;
    public Quaternion startRot;
    //we set public the target 2d rotation so you can change the value to point it higher or closer to the target
    public Vector3 targetPosition;
    public Vector3 targetRotation;

	private Coroutine laneChangeCoroutine = null;

    void Start()
    {
        //we get the values
        //startPos = transform.position;
        //startRot = transform.rotation;
    }

    public void CamRotateToEquip()
    {
        StartCoroutine(MoveCameraToEquip());
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

	public void CamSwitchLane(int _laneIndex)
    {
		Vector3 _target;

		_target = transform.position;

		switch (_laneIndex)
		{
			case 0:
				_target.x = -1.5f;
				break;
			case 1:
				_target.x = 0f;
				break;
			case 2:
				_target.x = 1.5f;
				break;
			default:
				break;
		}

		if (laneChangeCoroutine != null)
		{
			StopCoroutine(laneChangeCoroutine);
			laneChangeCoroutine = null;
		}
        laneChangeCoroutine = StartCoroutine(MoveCameraToLane(_target));
    }

	IEnumerator MoveCameraToLane(Vector3 _target)
    {
        //Set the time for T value
        float elapsedTime = 0;
        float moveDuration = 3f;

        while (elapsedTime < moveDuration)
        {
            //using Lerp method to make a smooth animation
            transform.position = Vector3.Lerp(transform.position, _target, elapsedTime / moveDuration);
            //sync to time.deltatime
            elapsedTime += Time.deltaTime;
			//wait next frame
            yield return null;
        }

        transform.position = _target;
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
    IEnumerator MoveCameraToEquip()
    {
        float elapsedTime = 0;
        float moveDuration = 1f;
        Vector3 _startPos = new Vector3(0, 5f, -3.8f);
        Vector3 _endPos = new Vector3(0.38f, 2.85f, -3.55f);
        Vector3 _startRot = new Vector3(26.39f, 0, 0);
        Vector3 _endRot = new Vector3(16.66f, 180f, 0);

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(_endPos, _startPos, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Euler(
                Mathf.LerpAngle(_endRot.x, _startRot.x, elapsedTime / moveDuration),
                Mathf.LerpAngle(_endRot.y, _startRot.y, elapsedTime / moveDuration),
                Mathf.LerpAngle(_endRot.z, _startRot.z, elapsedTime / moveDuration)
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        transform.rotation = startRot;
    }
}