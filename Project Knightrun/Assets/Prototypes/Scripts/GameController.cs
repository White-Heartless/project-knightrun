using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;
using System.IO;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private UIController uiController;
	[SerializeField]
	private CameraSwitch cameraSwitch;
	[SerializeField]
	private InputController inputController;
	[SerializeField]
	private Player player;

	public Room startingRoom;
	public Room[] roomArray3D;
	public Room[] roomArray2D;
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
			player.transform.Rotate(0,0,90f);
		}
		else //switching to 3d
		{
			is2D = false;
			cameraSwitch.CamSwitchTo3D();
			player.transform.Rotate(0,0,-90f);
		}
	}

    private void Start()
    {
		GameObject startRoom = GameObject.Instantiate(startingRoom.gameObject, new Vector3(0, 0, 10f), Quaternion.identity);
		startRoom.transform.Rotate(0, -90, 0);
		lastRoom = startRoom.GetComponent<Room>();
		Time.timeScale = 0;
    }

    public void IncreaseSoftCurrency()
    {
        runSoftCurrency++;
		uiController.updateSoftCurrency(runSoftCurrency);
    }

    public void IncreaseHardCurrency()
    {
        runHardCurrency++;
		uiController.updateHardCurrency(runHardCurrency);
    }

	void Update()
	{
		distance += Time.deltaTime;
		uiController.updateDistance(distance);
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
		player.transform.position = new Vector3(0,0.1f,0);
		//inputcontroller.currentlaneindex=1;
		if (is2D)
			Toggle2D3D(false);
    }

	//only called if obstacle could not be destroyed
	public void onObstacleHit()
	{
		Time.timeScale = 0;
		uiController.promptRevive();
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
			roomToSpawn = roomArray3D[Random.Range(0,roomArray3D.Length)].gameObject;
		else
			roomToSpawn = roomArray2D[Random.Range(0,roomArray2D.Length)].gameObject;
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
}
