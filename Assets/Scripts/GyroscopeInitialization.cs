using UnityEngine;

public class GyroscopeInitialization : MonoBehaviour
{
    [SerializeField] private float updateIntervalSeconds = 0.06f;
    
    private void Awake()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = updateIntervalSeconds;
    }
}