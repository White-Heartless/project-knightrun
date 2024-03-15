using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController GameController;
    private int CurrentRail = 1;
    //public List<Equipment> CurrentEquip = new List<Equipment>();

    private void Awake()
    {
        GameController = FindObjectOfType<GameController>();
    }

    public void OnDeath()
    {
        //  [Play death animation here]
        GameController.onGameOver();
        Debug.Log("Player Dead");
    }

    public void OnRevive()
    {
        //  [Play revive animation here]
        GameController.onResume(); // Perhaps another procedure would be more appropriate
        Debug.Log("Player Revived");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            GameController.SpawnRoom();
        }
    }
}
