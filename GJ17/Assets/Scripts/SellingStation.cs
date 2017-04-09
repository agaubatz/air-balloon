using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingStation : MonoBehaviour {
  public GameObject BucketPrefab;

	// Use this for initialization
	void Start () {
    Vector3 bucketPosition = transform.position;
    bucketPosition.y += 1f;
		if(bucketPosition.x > 0) {
      bucketPosition.x -= 2f;
    } else {
      bucketPosition.x += 2f;
    }
    Instantiate(BucketPrefab, bucketPosition, Quaternion.identity, transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D collider) {
    if(collider.transform.gameObject.name == "boat") {
      var boat = collider.gameObject.GetComponent<Player>();
      boat.DockAtStation();
    }
  }
}
