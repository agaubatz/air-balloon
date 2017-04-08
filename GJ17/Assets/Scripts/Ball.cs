using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  private Rigidbody2D rb;
  private Collider2D _collider;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void PickedUpByBird() {
    rb.isKinematic = true;
    Debug.Log("picked up");
  }

  public void DroppedByBird(Vector2 velocity) {
    rb.isKinematic = false;
    Debug.Log("Dropped " + velocity);
    rb.AddForce(velocity, ForceMode2D.Impulse);
  }

  public Vector3 GetSize()
  {
    return _collider.bounds.size;
  }
}
