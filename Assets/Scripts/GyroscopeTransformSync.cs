using UnityEngine;

public class GyroscopeTransformSync : MonoBehaviour
{
    private void Update()
    {
        Quaternion baseRotation = Quaternion.Euler(90, 0, 0);
        transform.rotation = baseRotation * Input.gyro.attitude.ConvertGyro();
    }
}