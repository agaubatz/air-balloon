using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  public CircleCollider2D StickinessSphere;
  public float StickinessSpeed = 1.0f;
  public float StickinessSleepThreshold = 0.25f;
  public float AttachmentCompensation = 0.2f;
  public ContactFilter2D StickinessFilter;

  private Rigidbody2D rb;
  private Collider2D _collider;

  private Collider2D[] _collisions = new Collider2D[25];

  private Transform _attached;
  private Vector3 _lastAttachmentPosition;
  private string _color;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
    
    _color = Game.Instance.ballColors.GetRandomColor();
    Debug.Log(_color);
    SpriteRenderer r = GetComponent<SpriteRenderer>();
    r.color = Game.Instance.ballColors.ColorMap[_color];
	}
	
	// Update is called once per frame
	void Update () {
	   int numCollisions = StickinessSphere.OverlapCollider(StickinessFilter, _collisions);

     /*for (int i = 0; i < numCollisions; i++)
     {
        var collider = _collisions[i];
        if (collider == null)
          continue;
        var ball = collider.gameObject.GetComponent<Ball>();
        if (ball == null)
          continue;

        //StickToBall(ball);
     }*/

     if (_attached != null)
     {
      Vector3 attachmentDiff = _attached.position - _lastAttachmentPosition;
      attachmentDiff.y = 0; attachmentDiff.z = 0;
      transform.position -= attachmentDiff * AttachmentCompensation;
      _lastAttachmentPosition = _attached.position;

      if(transform.position.y + 10 < _attached.position.y) {
        transform.parent = null;
        _attached = null;
      }
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
    if (other == this)
      return;
    if (rb.velocity.x < StickinessSleepThreshold)
      return;
    Vector3 dir = other.transform.position - transform.position;
    float distance = dir.magnitude;
    
    if (distance < other._collider.bounds.extents.x)
      return; //too close
    float proportion = distance / StickinessSphere.radius * transform.lossyScale.x;

    Vector3 stickVector = proportion * StickinessSpeed * dir * Time.deltaTime;
    Vector3 boatPosition = Game.Instance.boat.transform.position;
    Vector3 directionToBoat = boatPosition - transform.position;

    if (Mathf.Sign(stickVector.x) != Mathf.Sign(directionToBoat.x))
      stickVector.x = 0f;
    if (Mathf.Sign(stickVector.y) != Mathf.Sign(directionToBoat.y))
      stickVector.y = 0f;

    if (stickVector.y < 0)
      stickVector.y = 0;

    transform.position += stickVector;
  }

  public void AttachTo(Transform other)
  {
    if (transform.parent == null)
      transform.parent = other;
    _attached = other;
    _lastAttachmentPosition= other.position;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    var ball = collision.gameObject.GetComponent<Ball>();
    if (ball == null)
      return;
    if (_attached == null)
      return;
    ball.AttachTo(_attached);
  }
}