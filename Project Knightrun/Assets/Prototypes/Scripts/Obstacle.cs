using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public Player player;

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
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
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
