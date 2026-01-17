using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DarknessManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int startCell;

    Dictionary<Vector3Int, float> darkness = new();

    void Start()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            darkness[pos] = 0f;
            tilemap.SetColor(pos, Color.white);
        }

        if (darkness.ContainsKey(startCell))
        {
            darkness[startCell] = 1f;
            tilemap.SetColor(startCell, Color.black);
        }
        else
        {
            Debug.LogError("START CELL TILE YOK!");
        }
    }

    void Update()
    {
        List<Vector3Int> keys = new(darkness.Keys);

        foreach (var pos in keys)
        {
            if (darkness[pos] < 1f) continue;

            SpreadTo(pos + Vector3Int.up);
            SpreadTo(pos + Vector3Int.down);
            SpreadTo(pos + Vector3Int.left);
            SpreadTo(pos + Vector3Int.right);
        }

        foreach (var pos in keys)
        {
            darkness[pos] = Mathf.MoveTowards(darkness[pos], 1f, Time.deltaTime);
            tilemap.SetColor(pos, Color.Lerp(Color.white, Color.black, darkness[pos]));
        }
    }

    void SpreadTo(Vector3Int pos)
    {
        if (!darkness.ContainsKey(pos)) return;
        if (darkness[pos] > 0f) return;

        darkness[pos] = 0.01f;
    }
}
