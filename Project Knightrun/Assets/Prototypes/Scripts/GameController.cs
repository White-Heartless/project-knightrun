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

	public Room StartingRoom;
	public Room[] roomArray;
    public int runSoftCurrency = 0;
	private float distance = 0f;
	private int highScore = 0;
	public int runHardCurrency = 0;
	public int totalSoftCurrency = 0;
	public int totalHardCurrency = 0;
    public float CurrentSpeed = 20;

	public bool is2D = false; //false = 3d mode, true = 2d mode

	[ProPlayButton]
	public void Toggle2D3D()
	{
		is2D = !is2D;
		if (is2D)
			cameraSwitch.CamSwitchTo2D();
		else
			cameraSwitch.CamSwitchTo3D();
	}

    private void Start()
    {
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

	[ProPlayButton]
    public void onGameStart()
    {
		distance = 0f;
		uiController.updateDistance(distance);
		Time.timeScale = 1;
    }

	[ProPlayButton]
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
		GameObject startRoom = GameObject.Instantiate(StartingRoom.gameObject, new Vector3(0, 0, 10f), Quaternion.identity);
        startRoom.transform.Rotate(0, -90, 0);
        //Debug.Log("Game Over");
    }

	public void onObstacleHit()
	{
		Time.timeScale = 0;
		uiController.promptRevive();
	}

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

	[ProPlayButton]
    public void onPause()
    {
		Time.timeScale = 0;
        //Debug.Log("Game Paused");
    }

	[ProPlayButton]
    public void onResume()
    {
        Time.timeScale = 1;
        //Debug.Log("Game Resume");
    }

    private GameObject SelectRoom()
    {
		return roomArray[Random.Range(0,roomArray.Length)].gameObject;
    }

    public void SpawnRoom()
    {
		Quaternion rotation = Quaternion.Euler(0, -90, 0);

        GameObject newRoom = GameObject.Instantiate(SelectRoom(), new Vector3(0, 0, 24f), rotation);
    }

    public void DespawnRoom(GameObject _room)
    {
        Destroy(_room);
    }
}
