using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public static Game Instance { get; private set; }
	private List<GameObject> ballsAdded = new List<GameObject>();
	private List<GameObject> toRemove = new List<GameObject>();

	public GameObject BallPrefab;
	public Player boat;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		toRemove.Clear();
		foreach(GameObject ball in ballsAdded) {
			//Clearly a hack! But it should work so long as the balls only go down.
			//Possibly make a collision box instead.
			if(ball.transform.position.y + 10f < boat.transform.position.y) {
				toRemove.Add(ball);
			}
		}
		/*
		foreach(GameObject ball in toRemove) {
			ballsAdded.Remove(ball);
			Destroy(ball);
		}*/
	}

	public Ball CreateBall(Vector3 position, Transform parent)
	{
		var newBall = Instantiate(BallPrefab, position, Quaternion.identity, parent);
		ballsAdded.Add(newBall);

		return newBall.GetComponent<Ball>();
	}
}
