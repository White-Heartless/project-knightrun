using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Room room = (Room)target;

        // Check if toggle_show is true
		EditorGUILayout.PropertyField(serializedObject.FindProperty("enableEditMode"));
        if (room.enableEditMode)
        {
			EditorGUILayout.PropertyField(serializedObject.FindProperty("roomType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("roomLength"));
			if (GUILayout.Button("Generate Room"))
			{
				EmptyRoom(room);
				EmptyRoom(room);
				EmptyRoom(room);
				EmptyRoom(room); //not sure why but if i dont call it multiple times it wont destroy everything
				GenerateRoom(room);
			}

			if (GUILayout.Button("Snap to Lanes"))
            SnapToLanes(room.gameObject);
        }
        
		serializedObject.ApplyModifiedProperties();
		Repaint();
    }

	private void EmptyRoom(Room room)
	{
		foreach (Transform child in room.transform)
        {
			if (child.name == "Floor" || child.name == "LeftWall" || child.name == "RightWall" 
			 || child.name == "GenerationTrigger" || child.name == "RoomEnd3D" || child.name == "Roof"
			 || child.name == "LeftPlatform" || child.name == "RightPlatform")
            	DestroyImmediate(child.gameObject, true);
        }
	}

    private void GenerateRoom(Room room)
    {
        // Create floor
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
		floor.name = "Floor";
        floor.transform.SetParent(room.transform);
		floor.transform.localRotation = Quaternion.Euler(0f,90f,0f);
        floor.transform.localScale = new Vector3(room.roomLength, 1f, 1.15f);
        floor.transform.localPosition = new Vector3(0,0,5 * room.roomLength);
		floor.tag = "Ground";

        // Create walls
        CreateWall(room, new Vector3(-6.5f, 3f, 5 * room.roomLength), Quaternion.Euler(0f, -90f, 0f), "LeftWall");
        CreateWall(room, new Vector3(6.5f, 3f, 5 * room.roomLength), Quaternion.Euler(0f, 90f, 0f), "RightWall");

		/*
		// Create side platforms
		CreatePlatform(room, new Vector3(-4.5f, 0.4754f, 5*room.roomLength) , Quaternion.Euler(0f, -90f, 0f), "LeftPlatform");
		CreatePlatform(room, new Vector3(4.5f, 0.4754f, 5*room.roomLength) , Quaternion.Euler(0f, 90f, 0f), "RightPlatform");
		*/

		GameObject roof = GameObject.CreatePrimitive(PrimitiveType.Cube);
		roof.transform.SetParent(room.transform);
		roof.name = "Roof";
        roof.transform.localScale = new Vector3(14.5f, 1f, 10 * room.roomLength);
        roof.transform.localPosition = new Vector3(0,6f, 5 * room.roomLength);

		// Create room end trigger
		GameObject roomEnd = GameObject.CreatePrimitive(PrimitiveType.Plane);
		roomEnd.name = "RoomEnd3D";
		roomEnd.transform.SetParent(room.transform);
		roomEnd.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        roomEnd.transform.localPosition = new Vector3(0,0,10 * room.roomLength);
		roomEnd.GetComponent<MeshRenderer>().enabled = false;
		roomEnd.GetComponent<MeshCollider>().enabled = false;

		//create generation trigger
		GameObject generation = GameObject.CreatePrimitive(PrimitiveType.Cube);
		generation.transform.SetParent(room.transform);
		generation.name = "GenerationTrigger";
        generation.transform.localScale = new Vector3(12f, 6f, 0.01f);
        generation.transform.localPosition = new Vector3(0,3f, 5f);
		generation.GetComponent<MeshRenderer>().enabled = false;
		generation.GetComponent<BoxCollider>().isTrigger = true;
		generation.tag = "Trigger";
    }

    private void CreateWall(Room room, Vector3 position, Quaternion rotation, string wallName)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.SetParent(room.transform);
		wall.name = wallName;
        wall.transform.localScale = new Vector3(room.roomLength * 10, 6f, 1.5f);
        wall.transform.localPosition = position;
        wall.transform.localRotation = rotation;
    }

	private void CreatePlatform(Room room, Vector3 position, Quaternion rotation, string platformName)
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform.transform.SetParent(room.transform);
		platform.name = platformName;
        platform.transform.localScale = new Vector3(room.roomLength * 10, 0.95f, 3f);
        platform.transform.localPosition = position;
        platform.transform.localRotation = rotation;
    }

	void SnapToLanes(GameObject roomObject)
    {
        Coin[] coins = roomObject.GetComponentsInChildren<Coin>();
		Obstacle[] obstacles = roomObject.GetComponentsInChildren<Obstacle>();
		Equipment[] equipments = roomObject.GetComponentsInChildren<Equipment>();

        foreach (Coin coin in coins)
            SnapObjectToLane(coin.transform);
		
		foreach (Obstacle obstacle in obstacles)
            SnapObjectToLane(obstacle.transform);

		foreach (Equipment equipment in equipments)
            SnapObjectToLane(equipment.transform);
    }

	void SnapObjectToLane(Transform objectTransform)
	{
    	float nearestLaneXPosition = FindNearestLaneXPosition(objectTransform.localPosition.x);
    	Vector3 snappedPositionLocal = new Vector3(nearestLaneXPosition, objectTransform.localPosition.y, objectTransform.localPosition.z);
    	objectTransform.localPosition = snappedPositionLocal;
	}

    float FindNearestLaneXPosition(float localXPosition)
    {
        float[] laneXPositions = new float[] { -1.75f, -0.865f, 0f, 0.865f, 1.75f }; //lane Z-positions

        float nearestLaneXPosition = laneXPositions[0];
        float minDistance = Mathf.Abs(localXPosition - laneXPositions[0]);

        foreach (float laneXPosition in laneXPositions)
        {
            float distance = Mathf.Abs(localXPosition - laneXPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestLaneXPosition = laneXPosition;
            }
        }

        return nearestLaneXPosition;
    }
}
