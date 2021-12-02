using UnityEngine;

public class AccelerationData : MonoBehaviour, INetworkMessage, IReadableMessage
{
    public static Vector3 Acceleration => Input.gyro.userAcceleration;
    
    // INetworkMessage Implementation
    
    public MessageCode MessageCode => MessageCode.Acceleration;
    public string NetworkMessage => $"{Acceleration.x},{Acceleration.y},{Acceleration.z}";

    // IReadableMessage Implementation
    
    public string ReadableMessage => "Acceleration: (" +
                                     $"{Acceleration.x}, " +
                                     $"{Acceleration.y}, " +
                                     $"{Acceleration.z})";
}