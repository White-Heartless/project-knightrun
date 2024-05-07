using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AudioController audioController;
    public Player player;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioController = FindObjectOfType<AudioController>();
        player = FindObjectOfType<Player>();
    }

    public void onCoinCollected(Coin _coin)
    {
        if(_coin.coinType == Coin.CoinType.softcoin)
        {
            gameController.IncreaseSoftCurrency();
            audioController.AudioCrunchPlay();
        }
        else if (_coin.coinType == Coin.CoinType.premiumcoin)
        {
            gameController.IncreaseHardCurrency();
            audioController.AudioSpecialPlay();
        }
    }

    public void onEquipmentCollected(Equipment _equip)
    {
		gameController.equipmentCounts++;
        audioController.AudioSpecialPlay();
		/*
        foreach(Equipment e in player.CurrentEquip)
        {
            if(_equip.equipType == e.equipType)
				e.activateEquip();
        }
		*/
    }
}
