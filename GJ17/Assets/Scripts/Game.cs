using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	public static Game Instance { get; private set; }

	private bool _gameStarted = false;
	private bool _gameOver = false;

	private float _scoreTimer = 60f;
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
	public Text gameOverInstructionsText;
	public Text gameTitle;
	public Text gameInstructions;

	// Use this for initialization
	void Awake () {
		Instance = this;
		//Decide whether we want to hide the cursor?
		UnityEngine.Cursor.visible = false;
		totalTimeText.enabled = false;
		timeRemainingText.enabled = false;
		gameOverText.enabled = false;
		gameOverTimeText.enabled = false;
		gameOverInstructionsText.enabled = false;
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

		if(!boat.IsDocked() && IsGameGoing()) {
			_totalTime += Time.deltaTime;
			_scoreTimer -= Time.deltaTime;

			totalTimeText.text = "Total Seconds: " + (int)_totalTime;
			timeRemainingText.text = "Seconds Remaining: " + (int)_scoreTimer;

			if(_scoreTimer < 0f) {
				_scoreTimer = 0;
				EndGame();
			}
		}

		if(!_gameStarted && Input.GetKey(KeyCode.Space)) {
			gameTitle.enabled = false;
			gameInstructions.enabled = false;
			totalTimeText.enabled = true;
			timeRemainingText.enabled = true;
			_gameStarted = true;
		}

		if(_gameOver && Input.GetKey(KeyCode.Space)) {
			SceneManager.LoadScene("MainScene");
		}

		toRemove.Clear();
	}

	public bool IsGameGoing() {
		return _gameStarted && !_gameOver;
	}

	public float TimeSinceGameStart() {
		return _totalTime;
	}

	public void AddScore(int score) {
		_scoreTimer += score;
	}

	public void EndGame() {
		_gameOver = true;
		totalTimeText.enabled = false;
		timeRemainingText.enabled = false;
		gameOverText.enabled = true;
		gameOverTimeText.text = "Total Seconds: " + (int)_totalTime;
		gameOverTimeText.enabled = true;
		gameOverInstructionsText.enabled = true;
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
