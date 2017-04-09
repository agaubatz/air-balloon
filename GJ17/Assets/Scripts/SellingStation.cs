using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingStation : MonoBehaviour {
  public GameObject BucketPrefab;
  private static string[] colorList = new string[]{"Yellow", "Green", "Purple", "Blue"};
  public Sprite YellowSprite;
  public Sprite GreenSprite;
  public Sprite PurpleSprite;
  public Sprite BlueSprite;

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
