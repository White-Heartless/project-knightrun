using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private float score = 0;

	public void IncreaseScore()
	{
		Debug.Log("COIN COLLECTED");
		score++;
	}

	public void EquipItem()
    {
		Debug.Log("EQUIPPED!");
    }

	private void onGameStart()
	{
		Debug.Log("Game Start");
	}

	private void onGameOver()
	{
		Debug.Log("Game Over");
	}

	private void onPause()
	{
		Debug.Log("Game Paused");
	}

	private void onResume()
	{
		Debug.Log("Game Resume");
	}

	private void SpawnRoom()
	{
		Debug.Log("Room Spawned");
	}

	private void DespawnRoom()
	{
		Debug.Log("Room Despawned");
	}
}
