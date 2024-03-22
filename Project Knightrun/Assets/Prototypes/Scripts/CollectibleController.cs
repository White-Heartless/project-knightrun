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
        {
            gameController.IncreaseScore();
        }
        else if (_coin.coinType == Coin.CoinType.premiumcoin)
        {
            gameController.IncreaseRetries();
        }
    }

    public void onEquipmentCollected(Equipment _equip)
    {
        bool repeat = false;

        foreach(Equipment e in player.CurrentEquip)
        {
            if(_equip.equipType == e.equipType)
            {
                repeat = true;
            }
        }

        if (repeat)
        {
            if (_equip.equipType == Equipment.EquipType.helmet)
            {
                Debug.Log("HELMET ALREADY COLLECTED!");
            }
            else if (_equip.equipType == Equipment.EquipType.armor)
            {
                Debug.Log("ARMOR ALREADY COLLECTED!");
            }
            else if (_equip.equipType == Equipment.EquipType.pauldrons)
            {
                Debug.Log("PAULDRONS ALREADY COLLECTED!");
            }
            else if (_equip.equipType == Equipment.EquipType.sword)
            {
                Debug.Log("SWORD ALREADY COLLECTED!");
            }
        }
        else
        {
            player.CurrentEquip.Add(_equip);

            if (_equip.equipType == Equipment.EquipType.helmet)
            {
                Debug.Log("HELMET COLLECTED!");
            }
            else if (_equip.equipType == Equipment.EquipType.armor)
            {
                Debug.Log("ARMOR COLLECTED!");
            }
            else if (_equip.equipType == Equipment.EquipType.pauldrons)
            {
                Debug.Log("PAULDRONS COLLECTED!");
            }
            else if (_equip.equipType == Equipment.EquipType.sword)
            {
                Debug.Log("SWORD COLLECTED!");
            }
        }
    }
}
