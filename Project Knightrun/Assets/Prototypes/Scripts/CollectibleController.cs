using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    public collectibles collectibleType;

    public enum collectibles
    {
        coin,
        equip
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameController == null)
            {
                Debug.Log("SUCA");
            }

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
