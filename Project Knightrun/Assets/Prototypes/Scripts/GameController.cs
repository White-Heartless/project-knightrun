using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
