using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public Player player;
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
			//gameController.onObstacleHit();
            if (player.HasEquipment((int)equipmentRequired))
            {
				Debug.Log("OBSTACLE DESTROYED!");
				gameObject.SetActive(false);
            }
            else
            {
				player.OnDeath();
				player.gameObject.SetActive(false);
            }
		}
	}
}
