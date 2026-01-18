using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCDarknessVisual : MonoBehaviour
{
    public Tilemap darknessTilemap;
    public Color darkColor = new Color(0.3f, 0.3f, 0.3f, 1f);

    private SpriteRenderer sr;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        Vector3Int cell =
            darknessTilemap.WorldToCell(transform.position);

        if (darknessTilemap.GetColor(cell) == Color.black)
        {
            sr.color = darkColor;
        }
        else
        {
            sr.color = originalColor;
        }
    }
}
