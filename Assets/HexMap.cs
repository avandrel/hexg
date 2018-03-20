using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GenerateMap();
    }

    public GameObject HexPrefab;

    public Sprite ForestSprite;
    public Sprite WaterSprite;
    public Sprite PlainsSprite;
    public Sprite DeserSprite;
    public Sprite MountainSprite;

    public int NumRows = 30;
    public int NumColumns = 60;

    public void GenerateMap()
    {
        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                Hex h = new Hex(column, row);

                var hexGO = (GameObject)Instantiate(HexPrefab, h.PositionFromCamera(Camera.main.transform.position, NumRows, NumColumns), Quaternion.identity, this.transform);
                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;

                hexGO.GetComponentInChildren<TextMesh>().text = string.Concat(column, ',', row);

                SpriteRenderer sr = hexGO.GetComponentInChildren<SpriteRenderer>();
                sr.sprite = WaterSprite;
            }
        }
    }
}
