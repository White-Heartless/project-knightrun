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
		sword
	}

	public EquipType equipType;

	public int usesLeft;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		collectibleController = FindObjectOfType<CollectibleController>();
		usesLeft = 3;
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			collectibleController.onEquipmentCollected(this);
			gameObject.SetActive(false);
		}
	}
}
