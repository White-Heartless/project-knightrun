using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Room> RoomPool = new List<Room>();
    public Room CurrentRoom;
    public int Score = 0;
    public int Stage = 1;
    private float GameSpeed = 20;
    public float CurrentSpeed = 0;

    private void Start()
    {
        CurrentSpeed = GameSpeed;
    }

    public void IncreaseScore()
    {
        Debug.Log("COIN COLLECTED");
        Score++;
    }

    public void EquipItem()
    {
        Debug.Log("EQUIPPED!");
    }

    public void onGameStart()
    {
		CurrentSpeed = GameSpeed;
        Debug.Log("Game Start");
    }

    public void onGameOver()
    {
        CurrentSpeed = 0;
        Debug.Log("Game Over");
    }

    public void onPause()
    {
		CurrentSpeed = 0;
        Debug.Log("Game Paused");
    }

    public void onResume()
    {
        CurrentSpeed = GameSpeed;
        Debug.Log("Game Resume");
    }

    public void onStageChanged()
    {
        Stage++;
        UpdateRoomPool();
        Debug.Log("Stage Changed");
    }

    public void UpdateRoomPool()
    {
        Debug.Log("Room pool updated");
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
		
        return CurrentRoom.gameObject; //Placehoder
    }


    public void SpawnRoom()
    {
        GameObject newRoom = GameObject.Instantiate(SelectRoom(), new Vector3(0, 0, 0), Quaternion.identity);
        newRoom.transform.Rotate(0, -90, 0);
        //Debug.Log("Room Spawned");
    }

    public void DespawnRoom(GameObject _room)
    {
        Destroy(_room);
        //Debug.Log("Room Despawned");
    }
}
