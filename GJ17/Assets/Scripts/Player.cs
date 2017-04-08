using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Vector3 DefaultVelocity;
	private float VerticalMovementSpeed = 10f;
	private float HorizontalMovementSpeed = 10f;
	private float RotationAmount = 10f;
	public LayerMask CollisionMask;

	private PolygonCollider2D _collider;

	// Use this for initialization
	void Start () {
		_collider = GetComponent<PolygonCollider2D>();
		DefaultVelocity = new Vector3(0, VerticalMovementSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 inputVector = Vector3.zero;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			inputVector += Vector3.left * HorizontalMovementSpeed;
			//transform.Rotate(Vector3.MoveTowards(transform.Rotation));
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			inputVector += Vector3.right * HorizontalMovementSpeed;
			//transform.Rotate(Vector3.forward * 90);
		}

		TryToMove(inputVector * Time.deltaTime);

		TryToMove(DefaultVelocity * Time.deltaTime);
	}

	public void TryToMove(Vector3 amount)
	{
		Vector3 newPos = transform.position + amount;
		Vector2 newPos2D = Util.Make2D(newPos);
		
		if (!Physics2D.OverlapArea(newPos2D - Util.Make2D(_collider.bounds.extents), newPos2D + Util.Make2D(_collider.bounds.extents), CollisionMask))
		{
			transform.position = newPos;
		}
	}
}
