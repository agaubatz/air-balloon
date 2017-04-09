using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	public static Game Instance { get; private set; }

	public bool GameOver = false;

	private float _scoreTimer = 120f;
	private float _totalTime = 0f;

	private List<GameObject> objectsToDeleteWhenOffscreen = new List<GameObject>();
	public List<GameObject> toRemove = new List<GameObject>();

	public GameObject BallPrefab;
	public GameObject ObstaclePrefab;
	public GameObject SellingStationPrefab;
	public GameObject RockPrefab;
	public Player boat;
	public Text totalTimeText;
	public Text timeRemainingText;
	public Text gameOverText;
	public Text gameOverTimeText;

	// Use this for initialization
	void Awake () {
		Instance = this;
		//Decide whether we want to hide the cursor?
		UnityEngine.Cursor.visible = false;
		gameOverText.enabled = false;
		gameOverTimeText.enabled = false;
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

		if(!boat.IsDocked() && !GameOver) {
			_totalTime += Time.deltaTime;
			_scoreTimer -= Time.deltaTime;

			totalTimeText.text = "Total Time: " + (int)_totalTime;
			timeRemainingText.text = "Time Remaining: " + (int)_scoreTimer;

			if(_scoreTimer < 0f) {
				_scoreTimer = 0;
				EndGame();
			}
		}

		toRemove.Clear();
	}

	public void AddScore(int score) {
		_scoreTimer += score;
	}

	public void EndGame() {
		GameOver = true;
		totalTimeText.enabled = false;
		timeRemainingText.enabled = false;
		gameOverText.enabled = true;
		gameOverTimeText.text = "Total Time: " + (int)_totalTime;
		gameOverTimeText.enabled = true;
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

	public Rock CreateRock(Vector3 position, Transform transform) {
		var newRock = Instantiate(RockPrefab, position, Quaternion.identity, transform);
		objectsToDeleteWhenOffscreen.Add(newRock);

		return newRock.GetComponent<Rock>();
	}
}
