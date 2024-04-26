using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;
using System.IO;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

public class GameController : MonoBehaviour
{
	const int MAX_STAGE = 5;
	const float STAGE_DISTANCE = 60f;

	[SerializeField]
	private UIController uiController;
	[SerializeField]
	private CameraSwitch cameraSwitch;
	[SerializeField]
	private InputController inputController;
	[SerializeField]
	private Player player;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private QuestManager questManager;
	[SerializeField]
	private Player[] players;

	public Room startingRoom;
	public Room[] roomArray3D1;
	public Room[] roomArray2D1;
	public Room[] roomArray3D2;
	public Room[] roomArray2D2;
	public Room[] roomArray3D3;
	public Room[] roomArray2D3;
	public Room[] roomArray3D4;
	public Room[] roomArray2D4;
	public Room[] roomArray3D5;
	public Room[] roomArray2D5;
	public int stage = 1;
    public int runSoftCurrency = 0;
	private float distance = 0f;
	private int highScore = 0;
	public int runHardCurrency = 0;
	public int totalSoftCurrency = 0;
	public int totalHardCurrency = 0;
    public float runSpeed = 20;

	public Room lastRoom;
	private Vector3 lastGlobalPos;

	[HideInInspector]
	public bool is2D = false; //false = 3d mode, true = 2d mode

	public void Toggle2D3D(bool _3or2) //false - 3d / true - 2d
	{
		if (_3or2) //switching to 2d
		{
			is2D = true;
			inputController.Adjust();
			cameraSwitch.CamSwitchTo2D();
			player.transform.Rotate(0,90f,0);
			questManager.UpdateQuestProgress(3);
			runSpeed = 10;
		}
		else //switching to 3d
		{
			is2D = false;
			cameraSwitch.CamSwitchTo3D();
			player.transform.Rotate(0,-90f,0);
			runSpeed = 20;
		}
	}

    private void Start()
    {
        player = FindObjectOfType<Player>();
        animator = player.GetComponent<Animator>();
        GameObject startRoom = GameObject.Instantiate(startingRoom.gameObject, new Vector3(0, 0, 10f), Quaternion.identity);
		startRoom.transform.Rotate(0, -90, 0);
		lastRoom = startRoom.GetComponent<Room>();
		Time.timeScale = 0;
    }

    public void IncreaseSoftCurrency()
    {
        runSoftCurrency++;
		uiController.updateSoftCurrency(runSoftCurrency);
		questManager.UpdateQuestProgress(2);
    }

    public void IncreaseHardCurrency()
    {
        runHardCurrency++;
		uiController.updateHardCurrency(runHardCurrency);
    }

	public void DecreaseSoftCurrency()
	{
		totalSoftCurrency--;
		uiController.updateTotalCurrency(totalSoftCurrency, totalHardCurrency);
	}

	public void DecreaseHardCurrency()
	{
		totalHardCurrency++;
		uiController.updateTotalCurrency(totalSoftCurrency, totalHardCurrency);
	}

	public int getDistance()
	{
		return (int)distance;
	}

	void Update()
	{
		distance += Time.deltaTime;
		uiController.updateDistance(distance);
		UpdateRunQuest();
		if(stage < MAX_STAGE && distance >= stage * STAGE_DISTANCE)
			onStageChange();
	}

	void UpdateRunQuest()
    {
		questManager.UpdateQuestProgress(0);
	}

    public void onGameStart()
    {
		distance = 0f;
		uiController.updateDistance(distance);
		Time.timeScale = 1;
    }

    public void onGameOver()
    {
        Time.timeScale = 0;
		if ((int)distance > highScore)
		{
			highScore = (int)distance;
			uiController.updateHighScore(highScore);
		}
		distance = 0f;
		uiController.updateDistance(distance);
		totalSoftCurrency += runSoftCurrency;
		totalHardCurrency += runHardCurrency;
		uiController.updateTotalCurrency(totalSoftCurrency,totalHardCurrency);
		runSoftCurrency = 0;
		runHardCurrency = 0;
		stage = 1;
		questManager.ResetQuests();
		uiController.updateSoftCurrency(runSoftCurrency);
		uiController.updateHardCurrency(runHardCurrency);
		GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects)
		{
			Room roomComponent = obj.GetComponent<Room>();

			if (roomComponent != null)
				Destroy(obj);
		}
		GameObject startRoom = GameObject.Instantiate(startingRoom.gameObject, new Vector3(0, 0, 10f), Quaternion.identity);
        startRoom.transform.Rotate(0, -90, 0);
		lastRoom = startRoom.GetComponent<Room>();
		inputController.ResetPlayer();
		player.transform.position = new Vector3(0,0,0);
		player.ArmorCheck();
		if (is2D)
			Toggle2D3D(false);
		cameraSwitch.CamSwitchLane(1);
		cameraSwitch.ResetCamera();
		cameraSwitch.gameObject.transform.rotation = Quaternion.Euler(26.39f,0,0);
		cameraSwitch.gameObject.transform.position = new Vector3(0,5,-3.8f);	
    }

	//only called if obstacle could not be destroyed
	public void onObstacleHit()
	{
		//animator.SetTrigger("Die");
		Time.timeScale = 0;
		uiController.promptRevive();
	}

	public void onEquipMenuEnter()
	{
		cameraSwitch.CamRotateToEquip();
	}

	public void onEquipMenuExit()
	{
		cameraSwitch.CamRotateToInitial();
	}

	public void PlayerSwap(int index)
	{
		for (int i = 0; i < players.Length; i++)
		{
			players[i].gameObject.SetActive(false);
		}

		players[index].gameObject.SetActive(true);
        player = players[index];
        animator = player.GetComponent<Animator>();
		inputController.UpdatePlayer(player, animator);
    }

	//to do: add the rest of equipments
	public void onEquipConsumed(int _equipType)
	{
		switch (_equipType)
		{
			case 0: //helmet
				uiController.EquipAlpha(_equipType, false);
				break;
			default:
				break;
		}
	}

	//to do: add the rest of equipments
	public void onEquipActivated(int _equipType)
	{
		switch (_equipType)
		{
			case 0: //helmet
				uiController.EquipAlpha(_equipType, true);
				break;
			default:
				break;
		}
	}

    public void onPause()
    {
		Time.timeScale = 0;
    }

    public void onResume()
    {
        Time.timeScale = 1;
    }

	public bool onHardCurrencyReviveAttempt()
	{
		if (totalHardCurrency == 0 && runHardCurrency == 0)
		{
			return false;
		}
		else if (totalHardCurrency > 0)
			totalHardCurrency--;
		else
		{
			runHardCurrency--;
			uiController.updateHardCurrency(runHardCurrency);
		}
		return true;
	}

	public void Revive()
	{
		GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects)
		{
			Obstacle obstacleComponent = obj.GetComponent<Obstacle>();

			if (obstacleComponent != null)
				Destroy(obj);
		}
		Time.timeScale = 1;
	}

    private GameObject SelectRoom()
	{
		GameObject roomToSpawn;

		if (!is2D)
		{
			switch (stage)
			{
				case 1:
					roomToSpawn = roomArray3D1[Random.Range(0, roomArray3D1.Length)].gameObject;
					break;

				case 2:
					roomToSpawn = roomArray3D2[Random.Range(0, roomArray3D2.Length)].gameObject;
					break;

				case 3:
					roomToSpawn = roomArray3D3[Random.Range(0, roomArray3D3.Length)].gameObject;
					break;

				case 4:
					roomToSpawn = roomArray3D4[Random.Range(0, roomArray3D4.Length)].gameObject;
					break;

				case 5:
					roomToSpawn = roomArray3D5[Random.Range(0, roomArray3D5.Length)].gameObject;
					break;

				default:
					roomToSpawn = roomArray3D1[Random.Range(0, roomArray3D1.Length)].gameObject;
					break;
			}
		}
		else
		{
			switch (stage)
			{
				case 1:
					roomToSpawn = roomArray2D1[Random.Range(0, roomArray2D1.Length)].gameObject;
					break;

				case 2:
					roomToSpawn = roomArray2D2[Random.Range(0, roomArray2D2.Length)].gameObject;
					break;

				case 3:
					roomToSpawn = roomArray2D3[Random.Range(0, roomArray2D3.Length)].gameObject;
					break;

				case 4:
					roomToSpawn = roomArray2D4[Random.Range(0, roomArray2D4.Length)].gameObject;
					break;

				case 5:
					roomToSpawn = roomArray2D5[Random.Range(0, roomArray2D5.Length)].gameObject;
					break;

				default:
					roomToSpawn = roomArray2D1[Random.Range(0, roomArray2D1.Length)].gameObject;
					break;
			}
		}
		return roomToSpawn;
	}

    public void SpawnRoom()
    {
		if (!is2D)
		{
			lastGlobalPos = lastRoom.transform.Find("RoomEnd3D").position;
			Quaternion rotation = Quaternion.Euler(0, 0, 0);
			GameObject newRoom = GameObject.Instantiate(SelectRoom(), new Vector3(-999f,-999f,-999f), rotation);
			newRoom.transform.position = lastGlobalPos;
			lastGlobalPos = newRoom.transform.Find("RoomEnd3D").position;
			lastRoom = newRoom.GetComponent<Room>();
		}
		else
		{
			lastGlobalPos = lastRoom.transform.Find("RoomEnd2D").position;
			Quaternion rotation = Quaternion.Euler(0, 0, 0);
			GameObject newRoom = GameObject.Instantiate(SelectRoom(), new Vector3(-999f,-999f,-999f), rotation);
			newRoom.transform.position = lastGlobalPos;
			lastGlobalPos = newRoom.transform.Find("RoomEnd2D").position;
			lastRoom = newRoom.GetComponent<Room>();
		}
    }

    public void DespawnRoom(GameObject _room)
    {
        Destroy(_room);
    }

	public void onLaneChange(int _laneIndex)
	{
		cameraSwitch.CamSwitchLane(_laneIndex);
	}

	public void onStageChange()
    {
		stage++;
    }
}
