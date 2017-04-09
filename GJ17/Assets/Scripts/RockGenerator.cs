using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour {
  public float MinTimeBetweenRocks = 1f;
  public float MaxTimeBetweenRocks = 5f;
  private float _timeToNextRock;
  public Wall LeftWall;
  public Wall RightWall;
  public Player Player;
  public ObstacleGenerator Generator;

	// Use this for initialization
	void Start () {
		SetNextRockTime();
	}
	
	// Update is called once per frame
	void Update () {
    if(!Player.IsDocked() && !Generator.GeneratingSellingStationSoon()) {
      _timeToNextRock -= Time.deltaTime;
    }
    if(_timeToNextRock <= 0f) {
      Vector3 rockPosition = new Vector3(0, Player.transform.position.y + 9f + Random.value*3f, 0);
      rockPosition.x = Random.Range(LeftWall.GetObstacleX(), RightWall.GetObstacleX());
      Game.Instance.CreateRock(rockPosition, transform);
      SetNextRockTime();
    }
	}

  private void SetNextRockTime() {
    if(Time.realtimeSinceStartup < 5f) { //First 5 seconds
      _timeToNextRock = 5f;
    } else if(Time.realtimeSinceStartup < 245f) { //Next 4 minutes
      _timeToNextRock = Random.Range(MinTimeBetweenRocks, MaxTimeBetweenRocks - (Time.realtimeSinceStartup - 5f)/60f);
    } else {
      _timeToNextRock = MinTimeBetweenRocks;
    }
  }
}
