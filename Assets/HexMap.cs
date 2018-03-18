using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GenerateMap();
    }

    public GameObject HexPrefab;
    public int NumRows = 20;
    public int NumColumns = 40;

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
            }
        }
    }
}
