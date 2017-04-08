using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {
	public Vector2 Make2D(Vector3 vector)
	{
		return new Vector2(vector.x, vector.y);
	}
}
