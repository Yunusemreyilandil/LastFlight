using UnityEngine;

public class CameraFloat : MonoBehaviour
{
    public float floatAmount = 0.15f;
    public float floatSpeed = 1.5f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void LateUpdate()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.localPosition = startPos + new Vector3(0, yOffset, 0);
    }
}
