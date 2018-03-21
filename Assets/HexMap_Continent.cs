using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap {

	public override void GenerateMap()
    {
        base.GenerateMap();

        int numContinents = 3;
        int continentSpacing = NumColumns / numContinents;

        for (int c = 1; c < numContinents + 1; c++)
        {
            int numSplats = Random.Range(4, 8);

            for (int i = 0; i < numSplats; i++)
            {
                int range = Random.Range(5, 8);
                int y = Random.Range(range, NumRows - range);
                int x = Random.Range(30, 40) - y / 2 + (c * continentSpacing);

                ElevateArea(x, y, range);
            }
        }

        float noiseResolution = 0.1f;
        Vector2 noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        float noiseScale = 2f;
        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                var h = GetHexAt(column, row);
                h.Elavation += (Mathf.PerlinNoise(
                                ((float)column / Mathf.Max(NumColumns, NumRows) / noiseResolution) + noiseOffset.x,
                                ((float)row / Mathf.Max(NumColumns, NumRows) / noiseResolution) + noiseOffset.y) - 0.5f) * noiseScale;
            }

        }
                UpdateHexVisuals();
    }

    void ElevateArea(int q, int r, int range, float centerHeight = 0.9f)
    {
        var centerHex = GetHexAt(q, r);

        foreach (var hex in GetHexesWithinRangeOf(centerHex, range))
        {
            if (hex.Elavation < 0)
            {
                hex.Elavation = 0;
            }

            hex.Elavation = centerHeight * Mathf.Lerp(centerHeight, 0.25f, Mathf.Pow(Hex.Distance(centerHex, hex) / range, 2f));
        }
    }
}
