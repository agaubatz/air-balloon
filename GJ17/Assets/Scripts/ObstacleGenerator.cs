using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
	public float InitialObstacleHeight = 20f;
	public float ObstacleSpacing = 5f;
	public float CreateAheadDistance = 20f;
	public GameObject ObstaclePrefab;
	public Wall LeftWall;
	public Wall RightWall;

	private List<Obstacle> _obstacles = new List<Obstacle>();
	private float _lastObstacleCreatedAt;
	private float _nextObstacleHeight;

	// Use this for initialization
	void Start () {
		_nextObstacleHeight = InitialObstacleHeight;
	}
	
	// Update is called once per frame
	void Update () {
		var mainCamera = Camera.main;

		float camHeight = mainCamera.transform.position.y;
		GenerateObstaclesUpTo(camHeight + CreateAheadDistance);
	}

	void GenerateObstaclesUpTo(float height) {
		if (_nextObstacleHeight > height)
			return;

		float side = (Random.value > .5) ? -1f : 1f;

		Vector3 obstaclePosition = new Vector3(0, _nextObstacleHeight, 0);
		obstaclePosition.x = (side == -1) ? LeftWall.GetObstacleX() : RightWall.GetObstacleX();

		var newObstacle = Instantiate(ObstaclePrefab, obstaclePosition, Quaternion.identity, transform).GetComponent<Obstacle>();

		newObstacle.SetSize(Random.value, side == 1);
		_obstacles.Add(newObstacle);

		_lastObstacleCreatedAt = obstaclePosition.y;
		_nextObstacleHeight = _lastObstacleCreatedAt + ObstacleSpacing + Random.value * ObstacleSpacing;
		
		//Debug.Break();
	}
}
