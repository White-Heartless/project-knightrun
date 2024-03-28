using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField]
    private GameController GameController;

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
		if (!GameController.is2D)
        	transform.position += new Vector3(0, 0, -1) * GameController.runSpeed * Time.deltaTime;
		else
			transform.position += new Vector3(-1, 0, 0) * GameController.runSpeed * Time.deltaTime;
    }

	//when the room collides with one of the 2 "DestructionTrigger" objects in the scene it is immediately destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy")) 
            GameController.DespawnRoom(gameObject);
    }
}
