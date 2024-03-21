using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	public CollectibleController collectibleController;

	public enum EquipType
	{
		helmet,
		armor,
		pauldrons,
		sword
	}

	public EquipType equipType;

	void Start()
	{
		collectibleController = FindObjectOfType<CollectibleController>();
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
