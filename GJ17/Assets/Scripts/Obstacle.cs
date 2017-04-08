using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	public float MinSize = 3f;
	public float MaxSize = 7f;

	private SpriteRenderer _sprite;

	// Use this for initialization
	void Awake () {
		_sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSize(float proportion)
	{
		_sprite.size = new Vector2(MinSize + proportion * (MaxSize - MinSize), _sprite.size.y);
	}
}
