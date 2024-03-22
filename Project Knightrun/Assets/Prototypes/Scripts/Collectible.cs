using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	private GameController gameController;
	private enum CollectibleType
    {
        coin,
        equip
    }

	public enum CoinType
    {
		none,
		softcoin = CollectibleType.coin,
		premiumcoin = CollectibleType.coin
    }

	public enum EquipType
    {
		none,
		helmet = CollectibleType.equip,
		armor = CollectibleType.equip,
		pauldrons = CollectibleType.equip,
		sword = CollectibleType.equip
    }

	public CoinType coinType;
	public EquipType equipType;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			if (coinType != CoinType.none)
			{
				Debug.Log("Coin");
				Destroy(gameObject);
			}
			else if (equipType != EquipType.none)
			{
				Debug.Log("Equip");
				Destroy(gameObject);
			}
        }
    }
}
