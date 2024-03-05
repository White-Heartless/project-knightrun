using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoObstacleDemo : MonoBehaviour
{
	private protoExplosionController parentScript;
	public float minVelocity = 2.5f;
	public float maxVelocity = 5f;
	public float maxAngularSpeed = 360f;

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	private Rigidbody rb;

	public void ProtoExplode()
	{
		Reset();

		minVelocity = parentScript.minVelocity;
		maxVelocity = parentScript.maxVelocity;
		maxAngularSpeed = parentScript.maxAngularSpeed;

		rb.useGravity = true;

		// Generate random velocity
		Vector3 randomVelocity = new Vector3(Random.Range(0f, 0f), 0f, Random.Range(0, 1f)).normalized * Random.Range(minVelocity, maxVelocity);
		rb.velocity = randomVelocity;

		// Generate random rotation
		Vector3 randomRotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		float randomAngularSpeed = Random.Range(180f, maxAngularSpeed);
		rb.angularVelocity = randomRotationAxis * Mathf.Deg2Rad * randomAngularSpeed;

	}

	// Reset position / inertia / gravity of the object
	public void Reset()
	{
		rb.useGravity = false;

		transform.position = initialPosition;
        transform.rotation = initialRotation;

		rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
	}

	void Start()
	{

		initialPosition = transform.position;
        initialRotation = transform.rotation;

		parentScript = GetComponentInParent<protoExplosionController>();
		rb = GetComponent<Rigidbody>();
		minVelocity = parentScript.minVelocity;
		maxVelocity = parentScript.maxVelocity;
		maxAngularSpeed = parentScript.maxAngularSpeed;
	}
}
