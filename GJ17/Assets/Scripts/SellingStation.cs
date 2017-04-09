using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingStation : MonoBehaviour {
  public GameObject BucketPrefab;
  private static string[] colorList = new string[]{"Yellow", "Green", "Purple", "Blue"};
  private Dictionary<string, int> prices = new Dictionary<string, int>();
  public Sprite YellowSprite;
  public Sprite GreenSprite;
  public Sprite PurpleSprite;
  public Sprite BlueSprite;
  public Text YellowText;
  public Text GreenText;
  public Text PurpleText;
  public Text BlueText;

	// Use this for initialization
	void Start () {
    foreach(string color in colorList) {
      int price = (int)((Random.value * 10)) + 1;
      prices.Add(color, price);
    }
    YellowText.text = prices["Yellow"].ToString();
    GreenText.text = prices["Green"].ToString();
    PurpleText.text = prices["Purple"].ToString();
    BlueText.text = prices["Blue"].ToString();
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
