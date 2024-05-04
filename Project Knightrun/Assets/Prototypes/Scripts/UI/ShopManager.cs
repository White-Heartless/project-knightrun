using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameController gameController;
    [SerializeField]
    UIController uiController;
    [SerializeField]
    private int softCurrency;
    [SerializeField]
    private int itemLv1, itemLv2, itemLv3, itemLv4, itemLv5;
    [SerializeField]
    private int costLv1, costLv2, costLv3, costLv4, costLv5;



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

        if (softCurrency < costLv1)
        {
            uiController.PriceTagsOFF();
        }
        if (softCurrency >= costLv1 && gameController.equipmentCounts >= itemLv1)
        {
            uiController.PriceTagsON(0);
        }
        if (softCurrency >= costLv2 && gameController.equipmentCounts >= itemLv2)
        {
            uiController.PriceTagsON(1);
        }
        if (softCurrency >= costLv3 && gameController.equipmentCounts >= itemLv3)
        {
            uiController.PriceTagsON(2);
        }
        if (softCurrency >= costLv4 && gameController.equipmentCounts >= itemLv4)
        {
            uiController.PriceTagsON(3);
        }
        if (softCurrency >= costLv5 && gameController.equipmentCounts >= itemLv5)
        {
            uiController.PriceTagsON(4);
        }
    }
}
