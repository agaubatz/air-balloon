using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public float ObstacleInset = .5f;
	private Bounds _bounds;

	// Use this for initialization
	void Awake () {
		_bounds = GetComponentInChildren<SpriteRenderer>().bounds;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetObstacleX()
	{
		if (transform.position.x < 0)
		{
			return transform.position.x + _bounds.extents.x - ObstacleInset;
		}
		else
		{
			return transform.position.x - _bounds.extents.x + ObstacleInset;
		}
	}
}
