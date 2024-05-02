using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameController gameController;
    [SerializeField]
    UIController uiController;
    [SerializeField]
    private int softCurrency;
    [SerializeField]
    private int itemLv1;
    [SerializeField]
    private int itemLv2;
    [SerializeField]
    private int itemLv3;
    [SerializeField]
    private int itemLv4;
    [SerializeField]
    private int itemLv5;

    //launche the script anytime the gameobject is activated
    void OnEnable()
    {
        //Get the GC + set currency variable
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
        softCurrency = gameController.totalSoftCurrency;
        //Check what can you afford
        CheckCurrencyAmount();
    }
    
    //The switch statement help us to keep track of the currency threshold
    public void CheckCurrencyAmount()
    {
        switch (softCurrency)
        {
            case < 1000:
                uiController.PriceTagsOFF();
                Debug.Log("YOUR ARE POOR!");
                break;
            case < 2000:
                if (gameController.GetEquipmentCount(1) > itemLv1)
                {
                    uiController.PriceTagsON(1);
                }
                break;
            case < 3000:
                if (gameController.GetEquipmentCount(2) > itemLv2)
                {
                    uiController.PriceTagsON(2);
                }
                break;
            case < 4000:
                if (gameController.GetEquipmentCount(2) > itemLv3)
                {
                    uiController.PriceTagsON(3);
                }
                break;
            case < 5000:
                if (gameController.GetEquipmentCount(2) > itemLv4)
                {
                    uiController.PriceTagsON(4);
                }
                break;
            case >= 5000:
                if (gameController.GetEquipmentCount(5) > itemLv5)
                {
                    uiController.PriceTagsON(5);
                }
                break;
        }
    }
}
