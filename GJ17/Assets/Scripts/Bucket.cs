using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {
  private SellingStation sellingStation;

	// Use this for initialization
	void Start () {
		sellingStation = transform.GetComponent<SellingStation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D collider) {
    Debug.Log(collider.transform.gameObject.name);
    var ball = collider.gameObject.GetComponent<Ball>();
    if(ball) {

      Game.Instance.toRemove.Add(collider.transform.gameObject);
    }
  }
}
