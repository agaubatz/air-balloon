using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public static Game Instance { get; private set; }
	private List<GameObject> objectsToDeleteWhenOffscreen = new List<GameObject>();
	public List<GameObject> toRemove = new List<GameObject>();

	public GameObject BallPrefab;
	public GameObject ObstaclePrefab;
	public GameObject SellingStationPrefab;
	public Player boat;

	// Use this for initialization
	void Awake () {
		Instance = this;
		//Decide whether we want to hide the cursor?
		UnityEngine.Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject obj in objectsToDeleteWhenOffscreen) {
			if(obj.transform.position.y + 10f < boat.transform.position.y) {
				toRemove.Add(obj);
			}
		}
		
		
		foreach(GameObject obj in toRemove) {
			objectsToDeleteWhenOffscreen.Remove(obj);
			Destroy(obj);
		}

		toRemove.Clear();
	}

	public Ball CreateBall(Vector3 position, Transform parent)
	{
		var newBall = Instantiate(BallPrefab, position, Quaternion.identity, parent);
		objectsToDeleteWhenOffscreen.Add(newBall);

		return newBall.GetComponent<Ball>();
	}

	public Obstacle CreateObstacle(Vector3 position, Transform transform) {
		var newObstacle = Instantiate(ObstaclePrefab, position, Quaternion.identity, transform); 
		objectsToDeleteWhenOffscreen.Add(newObstacle);

		return newObstacle.GetComponent<Obstacle>();
	}

	public SellingStation CreateSellingStation(Vector3 position, Transform transform) {
		var newSellingStation = Instantiate(SellingStationPrefab, position, Quaternion.identity, transform);
		objectsToDeleteWhenOffscreen.Add(newSellingStation);

		return newSellingStation.GetComponent<SellingStation>();
	}
}
