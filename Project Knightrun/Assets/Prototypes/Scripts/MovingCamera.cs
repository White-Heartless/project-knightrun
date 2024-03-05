using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

public class MovingCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float endPoint = 20f;
    public float startPoint = -20f;

	//this isn't an actual distance check to the obstacle, it's just an arbitrary predetermined point in the scene
	//would have to do something like a RayCast to actually get a distance to an obstacle
	public float collisionDistance = -10f;
	private bool hasCollided = false;

	public GameObject doorObject;
	private protoExplosionController doorScript;

	private bool isMoving = false;

	// Toggle automatic movement of the MainCamera
	[ProPlayButton]
	void ToggleMovement() => isMoving = !isMoving;

	void Start() => doorScript = doorObject.GetComponent<protoExplosionController>();

    void Update()
    {
        if (isMoving)
        	transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            
        // Check if the camera has reached the end point, if yes, reset everything
        if (transform.position.z >= endPoint)
        {
			hasCollided = false;
            doorScript.ProtoResetChildren();
            transform.position = new Vector3(transform.position.x, transform.position.y, startPoint);
        }

		// Trigger obstacle explosion when collisionDistance is reached
		if (transform.position.z >= collisionDistance && !hasCollided)
		{
			hasCollided = true;
			doorScript.ProtoExplodeChildren();
		}
    }
}
