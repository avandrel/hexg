using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex {

    public readonly int Q;
    public readonly int R;
    public readonly int S;
    private static HexMap HexMap;
    public float Elavation;

    private static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    private readonly float radius = 1f;

    public Hex(HexMap hexMap, int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q - r);
        HexMap = hexMap;
    }

    public Vector3 Position => 
        new Vector3(HexHotizontalSpacing * (this.Q + this.R/2f), HexVerticalSpacing * this.R, 0);

    public float HexHeight => this.radius * 2;
    public float HexWidht => WIDTH_MULTIPLIER * HexHeight;
    public float HexVerticalSpacing => HexHeight * 0.75f;
    public float HexHotizontalSpacing => HexWidht;

    public Vector3 PositionFromCamera(Vector3 cameraPosition, int numRows, int numColumns)
    {
        float mapHeihgt = numRows * HexVerticalSpacing;
        float mapWidth = numColumns * HexHotizontalSpacing;

        float howManyWithsFromCamera = (Position.x - cameraPosition.x) / mapWidth;

        if (Mathf.Abs(howManyWithsFromCamera) <= 0.5f)
        {
            return Position;
        }

        howManyWithsFromCamera = howManyWithsFromCamera > 0 ? howManyWithsFromCamera + 0.5f : howManyWithsFromCamera - 0.5f;

        int howManyWidthtoFix = (int)howManyWithsFromCamera;

        var position = Position;
        position.x -= howManyWidthtoFix * mapWidth;

        return position;
    }

    public static float Distance(Hex a, Hex b)
    {
        int dQ = Mathf.Abs(a.Q - b.Q);
        if (dQ > HexMap.NumColumns / 2) dQ = HexMap.NumColumns = dQ;

        int dR = Mathf.Abs(a.R - b.R);

        return Mathf.Max(dQ, dR, Mathf.Abs(a.S - b.S));
    }
    
}
