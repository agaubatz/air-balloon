using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  private static string[] colorList = new string[]{"Yellow", "Green", "Purple", "Blue"};
  public CircleCollider2D StickinessSphere;
  public float StickinessSpeed = 1.0f;
  public float StickinessSleepThreshold = 0.25f;
  public float AttachmentCompensation = 0.2f;
  public ContactFilter2D StickinessFilter;
  public Sprite YellowSprite;
  public Sprite GreenSprite;
  public Sprite PurpleSprite;
  public Sprite BlueSprite;

  public GameObject SellParticlePrefab;

  public bool IsSold { get; private set;}
  public bool IsBeingCarried { get; private set; }

  private Rigidbody2D rb;
  private Collider2D _collider;

  private Collider2D[] _collisions = new Collider2D[25];

  private Transform _attached;
  private Vector3 _lastAttachmentPosition;
  private string _myColor;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
    _myColor = colorList[Random.Range(0, colorList.Length)];

    SpriteRenderer r = GetComponent<SpriteRenderer>();
    if(_myColor == "Yellow") {
      r.sprite = YellowSprite;
    } else if(_myColor == "Green") {
      r.sprite = GreenSprite;
    } else if(_myColor == "Purple") {
      r.sprite = PurpleSprite;
    } else {
      r.sprite = BlueSprite;
    }
	}
	
	// Update is called once per frame
	void Update () {
	   //int numCollisions = StickinessSphere.OverlapCollider(StickinessFilter, _collisions);

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
        Detach();
      }
     }
	}

  public void PickedUpByBird() {
    rb.isKinematic = true;
    IsBeingCarried = true;
    Detach();
  }

  public void DroppedByBird(Vector2 velocity) {
    rb.isKinematic = false;
    IsBeingCarried = false;
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
    if (IsSold)
      return;
    if (IsBeingCarried)
      return;
    if (transform.parent == null)
      transform.parent = other;
    _attached = other;
    _lastAttachmentPosition= other.position;
  }

  public string GetColor() {
    return _myColor;
  }

  void OnCollisionEnter2D(Collision2D collision) {
    var ball = collision.gameObject.GetComponent<Ball>();
    if (ball == null)
    {
      var rock = collision.gameObject.GetComponent<Rock>();
      if (rock != null && _attached != null) 
      {
        rock.BlowUp();
        Game.Instance.boat.Flip();
      }
      return;
    }
    if (_attached == null)
      return;
    ball.AttachTo(_attached);
  }

  public void MarkSold() {
    IsSold = true;
    Detach();

    StartCoroutine(SellCoroutine());
  }

  public void Detach()
  {
    transform.parent = null;
    _attached = null;
  }

  IEnumerator SellCoroutine() {
    var vfx = Instantiate(SellParticlePrefab, transform.position, Quaternion.identity, transform);
    Destroy(vfx, 3.0f);
    yield return new WaitForSeconds(1.0f);

    Game.Instance.toRemove.Add(gameObject);
    vfx.transform.parent = null;
  }
}