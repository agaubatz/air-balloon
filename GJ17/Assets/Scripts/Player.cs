using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Vector3 DefaultVelocity;
	public float MovementSpeed;
	public LayerMask CollisionMask;

	private BoxCollider2D _collider;

	// Use this for initialization
	void Start () {
		_collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 inputVector = Vector3.zero;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			inputVector += Vector3.left * MovementSpeed;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			inputVector += Vector3.right * MovementSpeed;
		}

		Vector3 newPos = transform.position + (DefaultVelocity + inputVector) * Time.deltaTime;
		Vector2 newPos2D = Util.Make2D(newPos);
		
		if (!Physics2D.OverlapArea(newPos2D - Util.Make2D(_collider.bounds.extents), newPos2D + Util.Make2D(_collider.bounds.extents), CollisionMask))
		{
			transform.position = newPos;
		}
	}
}
