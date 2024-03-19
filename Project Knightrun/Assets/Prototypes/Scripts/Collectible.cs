using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	private GameController gameController;
	public enum CollectibleType
    {
        coin,
        equip
    }

	public CollectibleType collectibleType;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			if (collectibleType == CollectibleType.coin)
			{
				Debug.Log("Coin");
				Destroy(gameObject);
			}
			else if (collectibleType == CollectibleType.equip)
			{
				Debug.Log("Equip");
				Destroy(gameObject);
			}
        }
    }
}
