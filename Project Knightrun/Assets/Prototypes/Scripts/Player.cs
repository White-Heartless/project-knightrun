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
        //GameController.onGameOver(); reduntand: this is already called somewhere else
        Debug.Log("Player Dead");
    }

    public void OnRevive()
    {
        //  [Play revive animation here]
        GameController.onResume(); // Perhaps another procedure would be more appropriate
        Debug.Log("Player Revived");
    }

    public bool HasEquipment(int _e)
    {
        foreach(Equipment e in CurrentEquip)
        {
            if ((int)e.equipType == _e && e.hasUsesLeft())
            {
				e.useEquip();
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            GameController.SpawnRoom();
        }
		else if (other.gameObject.CompareTag("3D to 2D"))
        {
            GameController.Toggle2D3D();
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
