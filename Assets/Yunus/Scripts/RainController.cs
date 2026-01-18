using DigitalRuby.RainMaker;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] RainScript2D rainController;
    [SerializeField] BirdFlightController birdFlightController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    [ContextMenu("Test")]
    void Test()
    {
        rainController.RainIntensity = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
