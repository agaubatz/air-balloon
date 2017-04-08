using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Vector3 DefaultVelocity;
	private float VerticalMovementSpeed = 10f;
	private float HorizontalMovementSpeed = 10f;
	private float RotationAmount = 30f;
	private float RotationSpeed = 5f;
	private float RotationCorrectionSpeed = 10f;
	public LayerMask CollisionMask;

	private Bounds _bounds;
	private BoxCollider2D _boxCollider;

	private const int _maxCollisionAttempts = 10;

	// Use this for initialization
	void Start () {
		_boxCollider = GetComponent<BoxCollider2D>();
		_bounds = _boxCollider.bounds;
		DefaultVelocity = new Vector3(0, VerticalMovementSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 inputVector = Vector3.zero;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			inputVector += Vector3.left * HorizontalMovementSpeed;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(RotationAmount, Vector3.forward), RotationSpeed*Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			inputVector += Vector3.right * HorizontalMovementSpeed;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(-RotationAmount, Vector3.forward), RotationSpeed*Time.deltaTime);
		} else {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(0, Vector3.forward), RotationCorrectionSpeed*Time.deltaTime);
		}

		TryToMove(inputVector * Time.deltaTime);

		TryToMove(DefaultVelocity * Time.deltaTime);
	}

	public void TryToMove(Vector3 amount, int attempts = 0)
	{
		if (attempts >= _maxCollisionAttempts)
			return;

		Vector3 newPos = transform.position + amount;
		Vector2 newPos2D = Util.Make2D(newPos + Vector3.Scale(new Vector3(_boxCollider.offset.x, _boxCollider.offset.y, 0), transform.lossyScale));

		if (!Physics2D.OverlapArea(newPos2D - Util.Make2D(_bounds.extents), newPos2D + Util.Make2D(_bounds.extents), CollisionMask))
		{
			transform.position = newPos;
		}
		else
		{
			attempts++;
			TryToMove(amount - (amount / _maxCollisionAttempts) * attempts, attempts);
		}
	}
}
