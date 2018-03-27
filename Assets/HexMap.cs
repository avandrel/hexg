using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GenerateMap();
    }

    public GameObject HexPrefab;

    public Sprite ForestSprite;
    public Sprite WaterSprite;
    public Sprite PlainsSprite;
    public Sprite DesertSprite;
    public Sprite MountainSprite;

    public float MountainHeight = 1f;
    public float ForestHeight = 0.6f;
    public float PlainsHeight = 0.0f;

    public static int NumRows = 30;
    public static int NumColumns = 60;

    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> hexToGOMap = new Dictionary<Hex, GameObject>();

    public virtual void GenerateMap()
    {
        hexes = new Hex[NumColumns, NumRows];

        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                Hex h = new Hex(this, column, row) { Elavation = -0.5f };

                hexes[column, row] = h;

                var hexGO = (GameObject)Instantiate(HexPrefab, h.PositionFromCamera(Camera.main.transform.position, NumRows, NumColumns), Quaternion.identity, this.transform);
                hexToGOMap[h] = hexGO;
                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;

                hexGO.GetComponentInChildren<TextMesh>().text = string.Concat(column, ',', row);
            }
        }

        UpdateHexVisuals();
    }

    public Hex GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            Debug.LogError("Not instantiated");
        }

        x = x % NumColumns;

        if (x < 0) x += NumColumns;

        return hexes[x, y];
    }

    public void UpdateHexVisuals()
    {
        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                var h = hexes[column, row];
                var hexGO = hexToGOMap[h];

                SpriteRenderer sr = hexGO.GetComponentInChildren<SpriteRenderer>();
                if (h.Elavation >= MountainHeight)
                {
                    sr.sprite = MountainSprite;
                }
                else if (h.Elavation >= ForestHeight)
                {
                    sr.sprite = ForestSprite;
                }
                else if (h.Elavation >= PlainsHeight)
                {
                    sr.sprite = PlainsSprite;
                }
                else
                {
                    sr.sprite = WaterSprite;
                }
            }
        }
    }

    public List<Hex> GetHexesWithinRangeOf(Hex hex, int range)
    {
        var result = new List<Hex>();

        for (int dx = -range; dx < range - 1; dx++)
        {
            for (int dy = Mathf.Max(-range + 1, -dx-range); dy < Mathf.Min(range, -dx+range - 1); dy++)
            {
                result.Add(GetHexAt(hex.Q + dx, hex.R + dy));
            }
        }

        return result;
    }
}
