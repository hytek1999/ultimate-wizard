using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorController : MonoBehaviour {
    public TileTypes TileTypes;
    public CursorController cursor;
    public LoadedLevel level; 


	// Use this for initialization
	void Start () {
        level = gameObject.AddComponent<LoadedLevel>() as LoadedLevel; 
        CreateNewLevel();	
	}

    void OnEnable()
    {
        this.AddObserver(OnFireEvent, InputController.FireNotification);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnFireEvent, InputController.FireNotification);
    }

    public void CreateNewLevel()
    {
        for (int x = 0; x <= TilePosition.TileWidth - 1; x++)
            level.AddTile(TileTypes.Tiles[0], x, 0);
    }

    private void OnFireEvent(object sender, object e)
    {
        int button = (int)e;
        if (button == 0)
        {
            level.AddTile(TileTypes.Tiles[0], cursor.tilePosition);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
