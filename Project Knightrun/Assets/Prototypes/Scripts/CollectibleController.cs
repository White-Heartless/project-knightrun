using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    GameController gameController;
    public collectibles collectibleType;

    public enum collectibles
    {
        coin,
        equip
    }

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected!");
            if (collectibleType == collectibles.coin)
            {
                gameController.IncreaseScore();
            }

            else if (collectibleType == collectibles.equip)
            {
                gameController.EquipItem();
            }
        }
    }
}
