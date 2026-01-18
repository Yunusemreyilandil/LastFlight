using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DarknessSpreadController : MonoBehaviour
{
    [Header("All Tilemaps To Affect")]
    public List<Tilemap> tilemaps;

    [Header("Manual Start Cells")]
    public List<Vector3Int> startCells;

    [Header("Spread Settings")]
    public float spreadDelay = 0.05f;
    public float colorLerpSpeed = 5f;

    [Header("Darkness Color")]
    public Color darknessColor = Color.black;

    private HashSet<Vector3Int> visitedCells = new HashSet<Vector3Int>();
    private Queue<Vector3Int> spreadQueue = new Queue<Vector3Int>();

    void Start()
    {
        foreach (Vector3Int cell in startCells)
        {
            if (HasTileInAnyMap(cell))
            {
                visitedCells.Add(cell);
                spreadQueue.Enqueue(cell);
            }
        }

        StartCoroutine(SpreadRoutine());
    }

    IEnumerator SpreadRoutine()
    {
        while (spreadQueue.Count > 0)
        {
            Vector3Int current = spreadQueue.Dequeue();

            // 🔥 O HÜCREDE TILE OLAN TÜM TILEMAP’LERİ KARART
            foreach (Tilemap map in tilemaps)
            {
                if (map.HasTile(current))
                {
                    StartCoroutine(LerpTileColor(map, current));
                }
            }

            // Komşular
            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (visitedCells.Contains(neighbor)) continue;

                if (HasTileInAnyMap(neighbor))
                {
                    visitedCells.Add(neighbor);
                    spreadQueue.Enqueue(neighbor);
                }
            }

            yield return new WaitForSeconds(spreadDelay);
        }
    }

    IEnumerator LerpTileColor(Tilemap map, Vector3Int cell)
    {
        Color startColor = map.GetColor(cell);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * colorLerpSpeed;
            map.SetColor(cell, Color.Lerp(startColor, darknessColor, t));
            yield return null;
        }

        map.SetColor(cell, darknessColor);
    }

    bool HasTileInAnyMap(Vector3Int cell)
    {
        foreach (Tilemap map in tilemaps)
        {
            if (map.HasTile(cell))
                return true;
        }
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
