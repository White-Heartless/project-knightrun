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

	/*
    public void OnDeath()
    {
        //  [Play death animation here]
    }

    public void OnRevive()
    {
        //  [Play revive animation here]
    }
	*/

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
            GameController.SpawnRoom();
		else if (other.gameObject.CompareTag("3D to 2D"))
            GameController.Toggle2D3D(true); //swap to 2D
		else if (other.gameObject.CompareTag("2D to 3D"))
            GameController.Toggle2D3D(false); //swap to 3D
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            InputController.ResetJump();
    }
}
