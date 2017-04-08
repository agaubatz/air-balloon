using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiler : MonoBehaviour {
	public int NumTiles = 10;
	public GameObject TilePlaceholder;
	public Vector3 TileDirection = Vector3.up;

	private Vector3 _inverseTileDirection;
	private Vector3 _tileSize;
	private Vector3 _tileOrigin;
	private List<GameObject> _tiles = new List<GameObject>();

	// Use this for initialization
	void Start () {
		_tileOrigin = transform.position;
		_inverseTileDirection = new Vector3(1.0f - TileDirection.x, 1.0f - TileDirection.y, 1.0f - TileDirection.z);

		var sprite = TilePlaceholder.GetComponent<SpriteRenderer>();
		_tileSize = sprite.bounds.size;

		Vector3 tilePos = _tileOrigin - InTileDirection(_tileSize * NumTiles / 2.0f);
		for (int i = 0; i < NumTiles; i++)
		{
			var tile = Instantiate(TilePlaceholder, tilePos, Quaternion.identity, transform);
			_tiles.Add(tile);

			tilePos += Vector3.Scale(TileDirection, _tileSize);
		}

		Destroy(TilePlaceholder);
	}
	
	// Update is called once per frame
	void Update () {
		var mainCamera = Camera.main;
		Vector3 camPos = mainCamera.transform.position;
		Vector3 camPosScaled = new Vector3(camPos.x / _tileSize.x, camPos.y / _tileSize.y, camPos.z / _tileSize.z);

		_tileOrigin = Vector3.Scale(InTileDirection(new Vector3(Mathf.Floor(camPosScaled.x), Mathf.Floor(camPosScaled.y), Mathf.Floor(camPosScaled.z))), _tileSize);
		ArrangeTiles();
	}

	void ArrangeTiles() {
		Vector3 tilePos = _tileOrigin - InTileDirection(_tileSize * NumTiles / 2.0f);
		for (int i = 0; i < NumTiles; i++)
		{
			_tiles[i].transform.position = InTileDirection(tilePos) + Vector3.Scale(_inverseTileDirection, _tiles[i].transform.position);

			tilePos += Vector3.Scale(TileDirection, _tileSize);
		}
	}

	Vector3 InTileDirection(Vector3 other) {
		return Vector3.Scale(TileDirection, other);
	}
}
