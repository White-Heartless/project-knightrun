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
		shoes,
		sword,
		unbreakable
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
			//tries to use the appropriate equipment if the player has it
            if (player.TryUsingEquipment((int)equipmentRequired)) 
				gameObject.SetActive(false);//will be subbstitued by the explosion function
            else
				gameController.onObstacleHit();
		}
	}
}
