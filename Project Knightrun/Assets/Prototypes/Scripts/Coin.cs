using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectibleController
{
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        collectibleType = collectibles.coin;
    }
}

