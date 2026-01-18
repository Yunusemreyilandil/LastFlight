using UnityEngine;

public class PulseScale : MonoBehaviour
{
    public float minScale = 0.85f;
    public float maxScale = 1.1f;
    public float speed = 2f;

    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f;
        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = baseScale * scale;
    }
}
