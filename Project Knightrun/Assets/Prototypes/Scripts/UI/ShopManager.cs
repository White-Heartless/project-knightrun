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

    //launche the script anytime the gameobject is activated
    void OnEnable()
    {
        //Get the GC + set currency variable
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
        //Check what can you afford
        CheckCurrencyAmount();
    }
    
    //The switch statement help us to keep track of the currency threshold
    public void CheckCurrencyAmount()
    {
        softCurrency = gameController.totalSoftCurrency;
        switch (softCurrency)
        {
            case < 1000:
                uiController.PriceTagsOFF();
                Debug.Log("YOUR ARE POOR!");
                break;
            case < 2000:
                if (gameController.GetEquipmentCount(1) > 5)
                {
                    uiController.PriceTagsON(1);
                }
                break;
            case < 3000:
                if (gameController.GetEquipmentCount(2) > 7)
                {
                    uiController.PriceTagsON(2);
                }
                break;
            case < 4000:
                if (gameController.GetEquipmentCount(2) > 10)
                {
                    uiController.PriceTagsON(3);
                }
                break;
            case < 5000:
                if (gameController.GetEquipmentCount(2) > 12)
                {
                    uiController.PriceTagsON(4);
                }
                break;
            case >= 5000:
                if (gameController.GetEquipmentCount(5) > 15)
                {
                    uiController.PriceTagsON(5);
                }
                break;
        }
    }
}
