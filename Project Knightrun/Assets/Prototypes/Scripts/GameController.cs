using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Room> RoomPool = new List<Room>();
    public List<Room> ActiveRooms = new List<Room>();
    public Room CurrentRoom;
    public int Score = 0;
    public int Stage = 1;
    public float GameSpeed = 30;

    void Update()
    {
        
    }

    public void onGameStart()
    {
        Debug.Log("Game Start");
    }

    public void onGameOver()
    {
        for (int i = ActiveRooms.Count - 1; i >= 0; i--)
        {
            ActiveRooms[i].Speed = 0;
        }
        Debug.Log("Game Over");
    }

    public void onPause()
    {
        Debug.Log("Game Paused");
    }

    public void onResume()
    {
        for (int i = ActiveRooms.Count - 1; i >= 0; i--)
        {
            ActiveRooms[i].Speed = GameSpeed;
        }
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
        Debug.Log("Room Selected");
        return CurrentRoom.gameObject; //Placehoder
    }


    public void SpawnRoom()
    {
        GameObject newRoom = GameObject.Instantiate(SelectRoom(), new Vector3(), Quaternion.identity);
        newRoom.GetComponent<Room>().Speed = GameSpeed;
        ActiveRooms.Add(newRoom.GetComponent<Room>());
        Debug.Log("Room Spawned");
    }

    public void DespawnRoom(GameObject _room)
    {
        for (int i = ActiveRooms.Count - 1; i >= 0; i--)
        {
            if (ActiveRooms[i].Equals(_room)) ActiveRooms.RemoveAt(i);
        }
        Destroy(_room);
        Debug.Log("Room Despawned");
    }
}
