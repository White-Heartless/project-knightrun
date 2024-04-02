using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;


//the protoexplodechildren function could probably be used in the obstacle class, depending on how the parts are set up
public class protoExplosionController : MonoBehaviour
{

	public float minVelocity = 1f;
	public float maxVelocity = 10f;
	public float maxAngularSpeed = 100f;

	[ProPlayButton]
	public void ProtoExplodeChildren()
	{
		//takes EVERY child and "explodes" it
		foreach (Transform child in transform)
		{
			protoObstacleDemo script = child.GetComponent<protoObstacleDemo>();
            if (script != null)
                script.ProtoExplode();
		}
	}

	[ProPlayButton]
	public void ProtoResetChildren()
	{
		foreach (Transform child in transform)
		{
			protoObstacleDemo script = child.GetComponent<protoObstacleDemo>();
            if (script != null)
                script.Reset();
		}
	}
}
