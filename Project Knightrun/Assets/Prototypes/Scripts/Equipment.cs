using JetBrains.Annotations;
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

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
		collectibleController = FindObjectOfType<CollectibleController>();
		ArmorCheck();
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
		//usesLeft = 2;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			collectibleController.onEquipmentCollected(this);
			gameObject.SetActive(false);
		}
	}

	public void ArmorCheck()
	{
		if (transform.parent.name == "PlayerLV1")
		{
			usesLeft = 0;
		}
		else if (transform.parent.name == "PlayerLV2")
		{
			usesLeft = 2;
		}
		else if (transform.parent.name == "PlayerLV3")
		{
			usesLeft = 3;
        }
		else if (transform.parent.name == "PlayerLV4")
		{
			usesLeft = 4;
		}
		else if (transform.parent.name == "PlayerLV5")
		{
			usesLeft = 5;
		}
		else if (transform.parent.name == "PlayerLV6")
		{
			usesLeft = 6;
		}
        gameObject.SetActive(true);
    }
}
