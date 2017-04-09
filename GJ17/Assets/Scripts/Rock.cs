using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
  public GameObject RockHitVFX;

	// Use this for initialization
	void Start () {
		var rb = GetComponent<Rigidbody2D>();
    rb.AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);
    float scale = Random.Range(0.2f, 0.6f); //Change to match whatever the object scale is.
    transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void BlowUp() {
    Game.Instance.toRemove.Add(gameObject);
    var vfx = Instantiate(RockHitVFX, transform.position, Quaternion.identity);
    Destroy(vfx, 2.0f);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    var obstacle = collision.gameObject.GetComponent<Obstacle>();
    if(obstacle) {
      BlowUp();
    }
    var bucket = collision.gameObject.GetComponent<PhysicalBucket>();
    if(bucket) {
      BlowUp();
    }

    var wall = collision.gameObject.GetComponent<PhysicalWall>();
    if(wall) {
      BlowUp();
    }
  }
}
