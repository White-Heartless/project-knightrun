using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Speed = 30;
    public Vector3 CurrentPosition;
    public GameController GameController;

    private void Awake()
    {
        GameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        transform.position += new Vector3() * Time.deltaTime;
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
