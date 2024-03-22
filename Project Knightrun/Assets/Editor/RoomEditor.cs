using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Room room = (Room)target;

        if (GUILayout.Button("Snap to Lanes"))
            SnapToLanes(room.gameObject);
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
    	float nearestLaneZPosition = FindNearestLaneZPosition(objectTransform.localPosition.z);
    	Vector3 snappedPositionLocal = new Vector3(objectTransform.localPosition.x, objectTransform.localPosition.y, nearestLaneZPosition);
    	objectTransform.localPosition = snappedPositionLocal;
	}

    float FindNearestLaneZPosition(float localZPosition)
    {
        float[] laneZPositions = new float[] { -2f, 0f, 2f }; //lane Z-positions

        float nearestLaneZPosition = laneZPositions[0];
        float minDistance = Mathf.Abs(localZPosition - laneZPositions[0]);

        foreach (float laneZPosition in laneZPositions)
        {
            float distance = Mathf.Abs(localZPosition - laneZPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestLaneZPosition = laneZPosition;
            }
        }

        return nearestLaneZPosition;
    }
}
