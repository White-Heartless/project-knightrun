using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSwap : MonoBehaviour
{
    private List<Equipment> inventory = new List<Equipment>();

    public void AddItem(Equipment item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(Equipment item)
    {
        inventory.Remove(item);
    }
}
