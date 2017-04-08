using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
  public Player boat;

	// Use this for initialization
	void Start () {
    //Decide whether we want to hide the cursor?
		//UnityEngine.Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(boat.transform.position.x, boat.transform.position.y, -10);
	}
}
