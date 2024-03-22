using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;
using System.IO;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private UIController uiController;

	public string stageFolderPath;

    public List<Room> RoomPool = new List<Room>();
    public Room CurrentRoom;
	public Room StartingRoom;
	public Room[] roomArray;
    public int runSoftCurrency = 0;
	public int runHardCurrency = 0;
	public int totalSoftCurrency = 0;
	public int totalHardCurrency = 0;
    public int Stage = 1;
    private float GameSpeed = 20;
    public float CurrentSpeed = 0;
	private string[] roomPaths;

    private void Start()
    {
        CurrentSpeed = GameSpeed;
		Time.timeScale = 0;
    }

    public void IncreaseSoftCurrency()
    {
        runSoftCurrency++;
		uiController.updateSoftCurrency(runSoftCurrency);
    }

    public void IncreaseHardCurrency()
    {
        //Debug.Log("PREMIUM COIN COLLECTED");
        runHardCurrency++;
		uiController.updateHardCurrency(runHardCurrency);
    }

	[ProPlayButton]
    public void onGameStart()
    {
		Time.timeScale = 1;
        //Debug.Log("Game Start");
    }

	[ProPlayButton]
    public void onGameOver()
    {
        Time.timeScale = 0;
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

    public void onStageChanged()
    {
        Stage++;
        UpdateRoomPool();
        //Debug.Log("Stage Changed");
    }

    public void UpdateRoomPool()
    {
        //Debug.Log("Room pool updated");
    }

    private GameObject SelectRoom()  //Numbers a, b, x, y, etc yet to decide
    {
      /*  switch (Stage) {

            case 1:
                if (Score < a)
                {
                    Debug.Log("Room Selected");
                    return RoomPool[Random.Range(0, x)].gameObject;
                }
                else if (Score < b)
                {
                    return RoomPool[Random.Range(x, y)].gameObject;
                }
                else
                {
                    return RoomPool[Random.Range(y, RoomPool.Count)].gameObject;
                }

            case 2:
                if (Score < c)
                {
                    Debug.Log("Room Selected");
                    return RoomPool[Random.Range(0, x)].gameObject;
                }
                else if (Score < d)
                {
                    return RoomPool[Random.Range(x, y)].gameObject;
                }
                else
                {
                    return RoomPool[Random.Range(y, RoomPool.Count)].gameObject;
                }

            case 3:
                if (Score < e)
                {
                    Debug.Log("Room Selected");
                    return RoomPool[Random.Range(0, x)].gameObject;
                }
                else if (Score < f)
                {
                    return RoomPool[Random.Range(x, y)].gameObject;
                }
                else
                {
                    return RoomPool[Random.Range(y, RoomPool.Count)].gameObject;
                }

            case 4:
                if (Score < g)
                {
                    Debug.Log("Room Selected");
                    return RoomPool[Random.Range(0, x)].gameObject;
                }
                else if (Score < h)
                {
                    return RoomPool[Random.Range(x, y)].gameObject;
                }
                else
                {
                    return RoomPool[Random.Range(y, RoomPool.Count)].gameObject;
                }

            case 5:
                if (Score < i)
                {
                    Debug.Log("Room Selected");
                    return RoomPool[Random.Range(0, x)].gameObject;
                }
                else if (Score < j)
                {
                    return RoomPool[Random.Range(x, y)].gameObject;
                }
                else
                {
                    return RoomPool[Random.Range(y, RoomPool.Count)].gameObject;
                }
        }*/
        //Debug.Log("Room Selected");


		return roomArray[Random.Range(0,roomArray.Length)].gameObject;
        //return CurrentRoom.gameObject; //Placehoder
    }


    public void SpawnRoom()
    {
        GameObject newRoom = GameObject.Instantiate(SelectRoom(), new Vector3(0, 0, 31.25f), Quaternion.identity);
        newRoom.transform.Rotate(0, -90, 0);
        //Debug.Log("Room Spawned");
    }

    public void DespawnRoom(GameObject _room)
    {
        Destroy(_room);
        //Debug.Log("Room Despawned");
    }
}
