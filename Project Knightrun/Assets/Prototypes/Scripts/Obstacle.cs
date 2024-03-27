using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private Player player;
	private GameController gameController;

	public enum EquipmentRequired
	{
		helmet,
		armor,
		pauldrons,
		sword
	}

	public EquipmentRequired equipmentRequired;

	void Start()
	{
		player = FindObjectOfType<Player>();
		gameController = FindObjectOfType<GameController>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
            if (player.HasEquipment((int)equipmentRequired))
				gameObject.SetActive(false);
            else
				gameController.onObstacleHit();
		}
	}
}
