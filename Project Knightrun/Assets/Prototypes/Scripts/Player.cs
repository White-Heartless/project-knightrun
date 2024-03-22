using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController GameController;
    public InputController InputController;
    public List<Equipment> CurrentEquip = new List<Equipment>();

    private void Awake()
    {
        GameController = FindObjectOfType<GameController>();
        InputController = FindObjectOfType<InputController>();
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            InputController.ResetJump();
        }
    }
}
