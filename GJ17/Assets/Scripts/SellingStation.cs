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
  public GameObject Instructions;

  private bool _isActive = true;

	// Use this for initialization
	void Start () {
    foreach(string color in colorList) {
      int price = (int)((Random.value * 10)) + 1;
      prices.Add(color, price);
    }
    YellowText.text = prices["Yellow"].ToString() + 's';
    GreenText.text = prices["Green"].ToString() + 's';
    PurpleText.text = prices["Purple"].ToString() + 's';
    BlueText.text = prices["Blue"].ToString() + 's';
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D collider) {
    if (!_isActive)
      return;

    var boat = collider.gameObject.GetComponent<Player>();
    if (boat == null && collider.transform.parent != null)
      boat = collider.transform.parent.GetComponent<Player>();
    if(boat != null) {
      boat.DockAtStation(this);
      ShowText();
    }
  }

  public int GetPrice(string color) {
    return prices[color];
  }

  public void Deactivate() {
    _isActive = false;
    HideText();
  }

  public void ShowText() {
    Instructions.SetActive(true);
  }

  public void HideText() {
    Instructions.SetActive(false);
  }
}
