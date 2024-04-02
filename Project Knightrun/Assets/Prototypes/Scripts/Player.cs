using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController GameController;
    private InputController InputController;
    private Animator animator;

    public List<Equipment> CurrentEquip = new List<Equipment>();

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
        InputController = FindObjectOfType<InputController>();
        animator = GetComponent<Animator>();
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

	//returns true if equipment was used, or false if it couldn't
    public bool TryUsingEquipment(int equipmentRequired)
    {
        foreach(Equipment e in CurrentEquip)
        {
            if ((int)e.equipType == equipmentRequired && e.hasUsesLeft())
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
            GameController.SpawnRoom(); //spawn a new room when player hits a generation trigger
        else if (other.gameObject.CompareTag("3D to 2D"))
            GameController.Toggle2D3D(true); //swap to 2D
        else if (other.gameObject.CompareTag("2D to 3D"))
            GameController.Toggle2D3D(false); //swap to 3D
        else if (other.gameObject.CompareTag("Sword"))
            animator.SetTrigger("Sword");
        else if (other.gameObject.CompareTag("Shield"))
            animator.SetTrigger("Shield");
        else if (other.gameObject.CompareTag("Shoulder"))
            animator.SetTrigger("Shoulder");

    }

	//this prevents multiple jumps in the air
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            InputController.ResetJump();
    }
}
