using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	public CollectibleController collectibleController;
	private GameController gameController;
	public EquipSwap equipSwap;

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

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		collectibleController = FindObjectOfType<CollectibleController>();
		usesLeft = 2;
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
            equipSwap.RemoveItem(this);
		}
	}

	public void activateEquip()
	{
        equipSwap.AddItem(this);
		if (gameController == null)
			gameController = FindObjectOfType<GameController>();
		gameController.onEquipActivated((int)equipType);
		usesLeft = 2;
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
