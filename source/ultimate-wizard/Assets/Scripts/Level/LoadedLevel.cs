using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadedLevel : MonoBehaviour {
    public GameObject[,] data = new GameObject[TilePosition.TileWidth, TilePosition.TileHeight];

    public void AddTile(GameObject tile, int x, int y)
    {
        if (data[x, y] != null)
            Destroy(data[x, y]);

        data[x,y] = (GameObject)Instantiate(tile, TilePosition.ToScreenCoords(x, y), Quaternion.identity);
    }

    public void AddTile(GameObject tile, TilePosition pos)
    {
        AddTile(tile, pos.x, pos.y);
    }
}
