using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DarknessSpreadController : MonoBehaviour
{
    [Header("Tilemaps")]
    public List<Tilemap> tilemaps;

    [Header("Manual Start Cells (Corners)")]
    [Tooltip("Karanlýðýn baþlayacaðý tile hücreleri")]
    public List<Vector3Int> startCells;

    [Header("Spread Settings")]
    public float spreadDelay = 0.05f;
    public float colorLerpSpeed = 4f;

    [Header("Darkness Color")]
    public Color darknessColor = Color.black;

    private HashSet<Vector3Int> infectedTiles = new HashSet<Vector3Int>();
    private Queue<Vector3Int> spreadQueue = new Queue<Vector3Int>();

    void Start()
    {
        StartFromManualCells();
    }

    void StartFromManualCells()
    {
        foreach (Vector3Int cell in startCells)
        {
            if (HasTileInAnyMap(cell))
            {
                StartDarkness(cell);
            }
            else
            {
                Debug.LogWarning($"Start Cell boþ: {cell}");
            }
        }
    }

    public void StartDarkness(Vector3Int startCell)
    {
        if (infectedTiles.Contains(startCell)) return;

        infectedTiles.Add(startCell);
        spreadQueue.Enqueue(startCell);

        StartCoroutine(SpreadRoutine());
    }

    IEnumerator SpreadRoutine()
    {
        while (spreadQueue.Count > 0)
        {
            Vector3Int current = spreadQueue.Dequeue();

            foreach (Tilemap map in tilemaps)
            {
                if (map.HasTile(current))
                    StartCoroutine(LerpTileColor(map, current));
            }

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (infectedTiles.Contains(neighbor)) continue;

                if (HasTileInAnyMap(neighbor))
                {
                    infectedTiles.Add(neighbor);
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
