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

	private Coroutine cameraRoutine = null;

	public void ResetCamera()
	{
		if (cameraRoutine != null)
		{
			StopCoroutine(cameraRoutine);
			cameraRoutine = null;
		}
	}

    public void CamRotateToEquip()
    {
		if (cameraRoutine != null)
		{
			StopCoroutine(cameraRoutine);
			cameraRoutine = null;
		}
        cameraRoutine =  StartCoroutine(MoveCameraToEquip());
    }

    public void CamRotateToInitial()
    {
		if (cameraRoutine != null)
		{
			StopCoroutine(cameraRoutine);
			cameraRoutine = null;
		}
        cameraRoutine = StartCoroutine(MoveCameraToInitial());
    }

    [ProButton]
    public void CamSwitchTo2D()
    {
		if (cameraRoutine != null)
		{
			StopCoroutine(cameraRoutine);
			cameraRoutine = null;
		}
        cameraRoutine = StartCoroutine(MoveCameraTo2D());
    }
	
    [ProButton]
    public void CamSwitchTo3D()
    {
		if (cameraRoutine != null)
		{
			StopCoroutine(cameraRoutine);
			cameraRoutine = null;
		}
        cameraRoutine = StartCoroutine(MoveCameraTo3D());
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

		if (cameraRoutine != null)
		{
			StopCoroutine(cameraRoutine);
			cameraRoutine = null;
		}
        cameraRoutine = StartCoroutine(MoveCameraToLane(_target));
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

		startPos = transform.position;
        startRot = transform.rotation;

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
            transform.position = Vector3.Lerp(targetPosition, new Vector3(0,5,-3.8f), elapsedTime / moveDuration);
            transform.rotation = Quaternion.Euler(
                Mathf.LerpAngle(targetRotation.x, 26.39f, elapsedTime / moveDuration),
                Mathf.LerpAngle(targetRotation.y, 0, elapsedTime / moveDuration),
                Mathf.LerpAngle(targetRotation.z, 0, elapsedTime / moveDuration)
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(0,5,-3.8f);
        transform.rotation = Quaternion.Euler(26.39f,0,0);
    }

    IEnumerator MoveCameraToEquip()
    {
        Vector3 _endPos = new Vector3(0f, 1.1f, 3.35f);
        Vector3 _endRot = new Vector3(16.66f, 180f, 0);

        transform.position = _endPos;
        transform.rotation = Quaternion.Euler(_endRot);
        yield return null;
    }

    IEnumerator MoveCameraToInitial()
    {
        Vector3 _startPos = new Vector3(0, 5f, -3.8f);
        Vector3 _startRot = new Vector3(26.39f, 0, 0);

        transform.position = _startPos;
        transform.rotation = Quaternion.Euler(_startRot);
        yield return null;
    }
}