using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  public CircleCollider2D StickinessSphere;
  public float StickinessSpeed = 1.0f;
  public ContactFilter2D StickinessFilter;

  private Rigidbody2D rb;
  private Collider2D _collider;

  private Collider2D[] _collisions = new Collider2D[25];

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	   int numCollisions = StickinessSphere.OverlapCollider(StickinessFilter, _collisions);

     for (int i = 0; i < numCollisions; i++)
     {
        var collider = _collisions[i];
        if (collider == null)
          continue;
        var ball = collider.gameObject.GetComponent<Ball>();
        if (ball == null)
          continue;

        StickToBall(ball);

     }
	}

  public void PickedUpByBird() {
    rb.isKinematic = true;
    Debug.Log("picked up");
  }

  public void DroppedByBird(Vector2 velocity) {
    rb.isKinematic = false;
    Debug.Log("Dropped " + velocity);
    //rb.AddForce(velocity, ForceMode2D.Impulse);
  }

  public Vector3 GetSize()
  {
    return _collider.bounds.size;
  }

  public void StickToBall(Ball other)
  { 
    Vector3 dir = other.transform.position - transform.position;
    float distance = dir.magnitude;
    
    if (distance < other._collider.bounds.extents.x)
      return; //too close
    float proportion = distance / StickinessSphere.radius;

    transform.position += proportion * StickinessSpeed * dir * Time.deltaTime;
  }
}
