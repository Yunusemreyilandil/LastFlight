using UnityEngine;
using Unity.Cinemachine;

public class CameraSpeedZoom : MonoBehaviour
{
    public Rigidbody2D targetRb;

    public float minZoom = 4.5f;
    public float maxZoom = 7f;

    public float zoomSpeed = 3f;
    public float maxSpeed = 7f;

    CinemachineCamera cam;

    void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
    }

    void LateUpdate()
    {
        if (!targetRb) return;

        float speed = targetRb.linearVelocity.magnitude;
        float t = Mathf.Clamp01(speed / maxSpeed);

        float targetZoom = Mathf.Lerp(minZoom, maxZoom, t);

        cam.Lens.OrthographicSize = Mathf.Lerp(
            cam.Lens.OrthographicSize,
            targetZoom,
            zoomSpeed * Time.deltaTime
        );
    }
}
