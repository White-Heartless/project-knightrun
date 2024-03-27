using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public CollectibleController collectibleController;

	public enum CoinType
	{
		softcoin,
		premiumcoin
	}

	public CoinType coinType;

	void Start()
	{
		collectibleController = FindObjectOfType<CollectibleController>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			collectibleController.onCoinCollected(this);
			gameObject.SetActive(false);
		}
	}
}
