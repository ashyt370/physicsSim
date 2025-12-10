using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelGenerator : MonoBehaviour
{

    public Transform player;
    public List<GameObject> tilePrefabs;

    public float tileSize = 50f;

    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();

    private Vector2Int currentTile;

    private void Update()
    {
        Vector2Int newTile = new Vector2Int(Mathf.FloorToInt(player.position.x/tileSize), Mathf.FloorToInt(player.position.z/tileSize));

        if(newTile != currentTile)
        {
            currentTile = newTile;
            UpdateTiles();
            Debug.Log("upodate");
        }
    }

    private void UpdateTiles()
    {
        List<Vector2Int> neededTile = new List<Vector2Int>();

        for (int x = -1;x <= 1;x++)
        {
            for(int z = -1; z <= 1; z++)
            {
                Vector2Int coordination = new Vector2Int(currentTile.x + x, currentTile.y + z);
                neededTile.Add(coordination);

                if(!tiles.ContainsKey(coordination))
                {
                    Vector3 position = new Vector3(coordination.x * tileSize, 0, coordination.y * tileSize);
                    int rand = Random.Range(0, tilePrefabs.Count);
                    tiles[coordination] = Instantiate(tilePrefabs[rand], position, Quaternion.identity);
                }
            }
        }
        // Delete
        List<Vector2Int> removeList = new List<Vector2Int>();
        foreach(var v in tiles)
        {
            if(!neededTile.Contains(v.Key))
            {
                Destroy(v.Value);
                removeList.Add(v.Key);
            }
        }

        foreach(var r in removeList)
        {
            tiles.Remove(r);
        }
    }
}
