using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {
  private SellingStation sellingStation;

	// Use this for initialization
	void Start () {
		sellingStation = transform.parent.GetComponent<SellingStation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D collider) {
    var ball = collider.gameObject.GetComponent<Ball>();
    if(ball) {
      Game.Instance.AddScore(sellingStation.GetPrice(ball.GetColor()));
      Game.Instance.toRemove.Add(collider.transform.gameObject);
    }
  }
}
