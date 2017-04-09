using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	public static Game Instance { get; private set; }
	private float _highScore;

	private bool _gameStarted = false;
	private bool _gameOver = false;

	private float _scoreTimer = 10f;
	private float _totalTime = 1f; //Start at 1 for rounding

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
	public Text highScore;
	public Text bigCountdown;

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
		highScore.enabled = false;
		_highScore = PlayerPrefs.GetFloat("highScore", 0f);
		highScore.text = "High Score: " + (int)_highScore;
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

		bigCountdown.gameObject.SetActive(!boat.IsDocked() && IsGameGoing() && _scoreTimer <= 10);
		bigCountdown.text = ((int)_scoreTimer).ToString();

		if(!boat.IsDocked() && IsGameGoing()) {
			_totalTime += Time.deltaTime;
			_scoreTimer -= Time.deltaTime;
			if(_totalTime > _highScore) {
				_highScore = _totalTime;
				highScore.text = "High Score: " + (int)_highScore;
				PlayerPrefs.SetFloat("highScore", _highScore);
			}

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
			boat.Show();
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
		timeRemainingText.text = "Seconds Remaining: " + (int)_scoreTimer;
	}

	public void EndGame() {
		_gameOver = true;
		totalTimeText.enabled = false;
		timeRemainingText.enabled = false;
		highScore.enabled = true;
		gameOverText.enabled = true;
		gameOverTimeText.text = "Total Seconds: " + (int)_totalTime;
		gameOverTimeText.enabled = true;
		gameOverInstructionsText.enabled = true;

		boat.Die();
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
