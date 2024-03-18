using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectibleController
{ 
    void Start()
    {
        collectibleType = collectibles.coin;
    }
}
