using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Speed = 20;
    public Vector3 CurrentPosition;
    public GameController GameController;

    private void Awake()
    {
        GameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        Speed = GameController.CurrentSpeed;
		if (!GameController.is2D)
        	transform.position += new Vector3(0, 0, -1) * Speed * Time.deltaTime;
		else
			transform.position += new Vector3(-1, 0, 0) * Speed * Time.deltaTime;
        CurrentPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            GameController.DespawnRoom(gameObject);
        }
    }
}
