using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {
  private SellingStation sellingStation;
  private bool _isActive = true;

	// Use this for initialization
	void Start () {
		sellingStation = transform.parent.GetComponent<SellingStation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerStay2D(Collider2D collider) {
    var ball = collider.gameObject.GetComponent<Ball>();
   	if(ball && !ball.IsBeingCarried && !ball.IsSold) {
      Game.Instance.AddScore(sellingStation.GetPrice(ball.GetColor()));
      ball.MarkSold();
    }
  }
}
