using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
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
    rb.AddForce(velocity*2.5f, ForceMode2D.Impulse);
  }
}
