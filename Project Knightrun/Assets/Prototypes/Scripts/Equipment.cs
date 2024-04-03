using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	public CollectibleController collectibleController;
	private GameController gameController;

	public enum EquipType
	{
		helmet,
		armor,
		pauldrons,
		shoes,
		sword
	}

	public EquipType equipType;

	//to do: turn into private with get set
	public int usesLeft;
	bool isEquipped = false;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		collectibleController = FindObjectOfType<CollectibleController>();
		usesLeft = 2;
	}

	private void Update()
	{
		StopAllCoroutines(); // Prevent overlapping rotations
		if(!isEquipped)
			StartCoroutine(Rotate());
	}

	private IEnumerator Rotate()
	{
		float startRotation = transform.eulerAngles.y;
		float endRotation = startRotation + 360.0f; // 360 degrees (one full rotation)
		float duration = 1.0f; // Time for one full rotation (adjust as needed)
		float t = 0.0f;

		while (t < duration)
		{
			t += Time.deltaTime;
			float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
			transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z));
			yield return null; // Yield to allow frame rendering
		}
	}


	public bool hasUsesLeft()
	{
		if (usesLeft > 0)
			return true;
		return false;
	}

	public void useEquip()
	{
		usesLeft--;
		if (usesLeft == 0)
		{
			gameController.onEquipConsumed((int)equipType);
			gameObject.SetActive(false);
		}
	}

	public void activateEquip()
	{
		gameObject.SetActive(true);
		if (gameController == null)
			gameController = FindObjectOfType<GameController>();
		gameController.onEquipActivated((int)equipType);
		usesLeft = 2;
		isEquipped = true;
		transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			collectibleController.onEquipmentCollected(this);
			gameObject.SetActive(false);
		}
	}
}
