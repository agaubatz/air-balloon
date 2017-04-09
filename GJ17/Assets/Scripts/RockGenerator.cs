using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour {
  private float MinTimeBetweenRocks = 1f;
  private float MaxTimeBetweenRocks = 4.25f;
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
    if(!Game.Instance.IsGameGoing()) {
      return;
    }

    if(!Player.IsDocked() && !Generator.GettingToSellingStationSoon()) {
      _timeToNextRock -= Time.deltaTime;
    }
    if(_timeToNextRock <= 0f) {
      Vector3 rockPosition = new Vector3(0, Player.transform.position.y + 9f + Random.value*3f, 0);
      rockPosition.x = Random.Range(LeftWall.GetObstacleX()+1f, RightWall.GetObstacleX()-1f);
      Game.Instance.CreateRock(rockPosition, transform);
      SetNextRockTime();
    }
	}

  private void SetNextRockTime() {
    if(Game.Instance.TimeSinceGameStart() < 5f) { //First 5 seconds
      _timeToNextRock = 5f;
    } else if(Game.Instance.TimeSinceGameStart() < 125f) { //Next 2 minutes
      _timeToNextRock = Random.Range(MinTimeBetweenRocks, MaxTimeBetweenRocks - (Game.Instance.TimeSinceGameStart() - 5f)/30f);
    } else {
      _timeToNextRock = MinTimeBetweenRocks;
    }
  }
}
