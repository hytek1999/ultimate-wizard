using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public struct TilePosition {
    public const float TileOriginX = -19.5f;
    public const float TileOriginY = -10.5f;
    public const int TileWidth = 40;
    public const int TileHeight = 25;

    public int x;
    public int y;

    public TilePosition(int x, int y)
    {
        this.x = x;
        this.y = y; 
    }

    public static Vector3 ToScreenCoords(int x, int y)
    {
        return new Vector3(x + TileOriginX, y + TileOriginY, 0);
    }

    public static Vector3 ToScreenCoords(TilePosition pos)
    {
        return ToScreenCoords(pos.x, pos.y);
    }

    public Vector3 ToScreenCoords()
    {
        return ToScreenCoords(this.x, this.y);
    }

    public static TilePosition FromScreenCoords(float x, float y)
    {
        float fX = x - TileOriginX;
        float fY = y - TileOriginY;

        return new TilePosition(Mathf.RoundToInt(fX), Mathf.RoundToInt(fY));
    }

    public static TilePosition FromScreenCoords(Vector3 pos)
    {
        return FromScreenCoords(pos.x, pos.y);
    }
}
