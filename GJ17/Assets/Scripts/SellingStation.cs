using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingStation : MonoBehaviour {
  public GameObject BucketPrefab;
  private static string[] colorList = new string[]{"Yellow", "Green", "Purple", "Blue"};
  private Dictionary<string, int> prices = new Dictionary<string, int>();
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

    foreach(string color in colorList) {
      int price = (int)((Random.value * 500));
      price -= price%10;
      prices.Add(color, price);
    }
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

  public int GetPrice(string color) {
    return prices[color];
  }
}
