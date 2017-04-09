using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	public const float MinSize = 7f;
	public const float MaxSize = 9f;

	private SpriteRenderer _sprite;
	private Collider2D _collider;
	private bool _flipX;

	// Use this for initialization
	void Awake () {
		_sprite = GetComponent<SpriteRenderer>();
		_collider = GetComponent<Collider2D>();
	}

	void Start() {
		int numBalls = Random.Range(0, 4);

		float width = _sprite.bounds.size.x;
		float ballMinSpacing = width / (numBalls * 4);
		float ballMaxSpacing = width / (numBalls * 2);

		Vector3 offset = Vector3.zero;
		for (int i = 0; i < numBalls; i++)
		{
			offset = new Vector3(offset.x + ballMinSpacing + ballMaxSpacing * Random.value, 0, 0);
			Vector3 position = transform.position + new Vector3(0, _collider.bounds.extents.y, 0);
			position += _flipX ? -offset : offset;

			var ball = Game.Instance.CreateBall(position, null);
			ball.transform.position += new Vector3(0f, ball.GetSize().y / 2.0f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSize(float proportion, bool flipX)
	{
		_flipX = flipX;
		_sprite.size = new Vector2(MinSize + proportion * (MaxSize - MinSize), _sprite.size.y);
		transform.localScale = flipX ? new Vector3(-1.0f, 1, 1) : Vector3.one;
	}
}
