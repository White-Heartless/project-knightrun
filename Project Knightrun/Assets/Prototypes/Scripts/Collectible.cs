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

	public enum EquipLevel
	{
		none,
		levelone = CollectibleType.equip,
		leveltwo = CollectibleType.equip,
		levelthree = CollectibleType.equip,
		levelfour = CollectibleType.equip,
		levelfive = CollectibleType.equip
	}

	public CoinType coinType;
	public EquipType equipType;
	public EquipLevel equipLevel;

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
			else if (equipLevel == EquipLevel.levelone)
			{
                if (equipLevel != EquipLevel.none)
                {
                    int equipLevelInt = (int)equipLevel;

                    // Collect the equipment
                    gameController.CollectEquipment(equipLevelInt);
                    Debug.Log("Equipment collected: " + equipType + " (Level " + equipLevel + ")");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Invalid equipment level: " + equipLevel);
                }
			}
        }
    }
}
