using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public GameController gameController;
    public Player player;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<Player>();
    }

    public void onCoinCollected(Coin _coin)
    {
        if(_coin.coinType == Coin.CoinType.softcoin)
            gameController.IncreaseSoftCurrency();
        else if (_coin.coinType == Coin.CoinType.premiumcoin)
            gameController.IncreaseHardCurrency();
    }

    public void onEquipmentCollected(Equipment _equip)
    {
        foreach(Equipment e in player.CurrentEquip)
        {
            if(_equip.equipType == e.equipType)
				e.activateEquip();
        }
    }
}
