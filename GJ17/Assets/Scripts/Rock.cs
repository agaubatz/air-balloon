using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var rb = GetComponent<Rigidbody2D>();
    rb.AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);
    float scale = Random.Range(0.1f, 0.2f); //Change to match whatever the object scale is.
    transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void BlowUp() {
    Game.Instance.toRemove.Add(gameObject);
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
