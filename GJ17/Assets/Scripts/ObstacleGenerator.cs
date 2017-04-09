using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
	public float InitialObstacleHeight = 20f;
	public float ObstacleSpacing = 5f;
	public float CreateAheadDistance = 20f;
	public Wall LeftWall;
	public Wall RightWall;
	public int InitialObstaclesUntilStation;

	//private List<Obstacle> _obstacles = new List<Obstacle>();
	private float _lastObstacleCreatedAt;
	private float _nextObstacleHeight;
	private int _obstaclesUntilStation;

	// Use this for initialization
	void Start () {
		_nextObstacleHeight = InitialObstacleHeight;
		_obstaclesUntilStation = InitialObstaclesUntilStation;
	}
	
	// Update is called once per frame
	void Update () {
		var mainCamera = Camera.main;

		float camHeight = mainCamera.transform.position.y;
		GenerateObstaclesUpTo(camHeight + CreateAheadDistance);
	}

	void GenerateObstaclesUpTo(float height) {
		if (_nextObstacleHeight > height) {
			return;
		}

		if(_obstaclesUntilStation == 0) {
				Vector3 sellingPosition = new Vector3(0, _nextObstacleHeight, 0);
				sellingPosition.x = Random.Range(LeftWall.GetObstacleX() + Obstacle.MinSize, RightWall.GetObstacleX() - Obstacle.MinSize);
				var proportion = (sellingPosition.x -LeftWall.GetObstacleX() - Obstacle.MinSize)/(RightWall.GetObstacleX() - LeftWall.GetObstacleX() - 2f*Obstacle.MinSize);

				var newSellingStation = Game.Instance.CreateSellingStation(sellingPosition, transform);

				//Generate obstacles on either side of the selling station
				Vector3 leftObstaclePosition = sellingPosition;
				Vector3 rightObstaclePosition = sellingPosition;
				leftObstaclePosition.x = LeftWall.GetObstacleX();
				rightObstaclePosition.x = RightWall.GetObstacleX();

				//TODO: there's a good chance this introduced a scale change, but revisit after the art is done.
				var leftObstacle = Game.Instance.CreateObstacle(leftObstaclePosition, null);
				var rightObstacle = Game.Instance.CreateObstacle(rightObstaclePosition, null);

				leftObstacle.DontSpawnBalls();
				rightObstacle.DontSpawnBalls();
				
				leftObstacle.SetSize(proportion, false);
				rightObstacle.SetSize(1f-proportion, true);

				_lastObstacleCreatedAt = sellingPosition.y;
				_nextObstacleHeight = _lastObstacleCreatedAt + ObstacleSpacing + Random.value * ObstacleSpacing + InitialObstacleHeight;

				_obstaclesUntilStation = (int)Mathf.Min((Random.value * (InitialObstaclesUntilStation + (Time.realtimeSinceStartup / 15f))) + 1, 20);
			} else {
				float side = (Random.value > .5) ? -1f : 1f;

				Vector3 obstaclePosition = new Vector3(0, _nextObstacleHeight, 0);
				obstaclePosition.x = (side == -1) ? LeftWall.GetObstacleX() : RightWall.GetObstacleX();

				var newObstacle = Game.Instance.CreateObstacle(obstaclePosition, transform);

				newObstacle.SetSize(Random.value, side == 1);

				_lastObstacleCreatedAt = obstaclePosition.y;
				_nextObstacleHeight = _lastObstacleCreatedAt + ObstacleSpacing + Random.value * ObstacleSpacing;
				
				//Debug.Break();
				_obstaclesUntilStation--;
			}

		
	}
}
