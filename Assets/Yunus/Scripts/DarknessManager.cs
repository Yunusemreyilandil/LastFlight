using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DarknessSpreadController : MonoBehaviour
{
    [Header("Tilemaps")]
    public List<Tilemap> tilemaps;

    [Header("Start Cells")]
    public List<Vector3Int> startCells;

    [Header("Spread")]
    public float cellsPerSecond = 20f; // yayılma hızı
    public float colorLerpSpeed = 6f;

    public Color darknessColor = Color.black;

    private HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
    private Queue<Vector3Int> queue = new Queue<Vector3Int>();

    private float spreadAccumulator = 0f;

    void Start()
    {
        foreach (var cell in startCells)
        {
            if (HasTile(cell))
            {
                visited.Add(cell);
                queue.Enqueue(cell);
            }
        }
    }

    void Update()
    {
        if (queue.Count == 0) return;

        spreadAccumulator += Time.deltaTime * cellsPerSecond;

        while (spreadAccumulator >= 1f && queue.Count > 0)
        {
            spreadAccumulator -= 1f;
            SpreadOneCell();
        }
    }

    void SpreadOneCell()
    {
        Vector3Int current = queue.Dequeue();

        // Karart
        foreach (Tilemap map in tilemaps)
        {
            if (map.HasTile(current))
                StartCoroutine(LerpTile(map, current));
        }

        // Komşular
        foreach (Vector3Int n in GetNeighbors(current))
        {
            if (visited.Contains(n)) continue;

            if (HasTile(n))
            {
                visited.Add(n);
                queue.Enqueue(n);
            }
        }
    }

    IEnumerator LerpTile(Tilemap map, Vector3Int cell)
    {
        Color start = map.GetColor(cell);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * colorLerpSpeed;
            map.SetColor(cell, Color.Lerp(start, darknessColor, t));
            yield return null;
        }

        map.SetColor(cell, darknessColor);
    }

    bool HasTile(Vector3Int cell)
    {
        foreach (Tilemap map in tilemaps)
            if (map.HasTile(cell)) return true;

        return false;
    }

    List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        return new List<Vector3Int>
        {
            cell + Vector3Int.up,
            cell + Vector3Int.down,
            cell + Vector3Int.left,
            cell + Vector3Int.right
        };
    }
}
