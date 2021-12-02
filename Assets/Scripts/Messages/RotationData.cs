using UnityEngine;

public class RotationData : MonoBehaviour, INetworkMessage, IReadableMessage
{
    public static Quaternion Rotation => Input.gyro.attitude.ConvertGyro();

    // private static Quaternion BlenderRotation
    // {
    //     get
    //     {
    //         Quaternion baseRotation = Quaternion.Euler(-90, 0, 0);
    //         return baseRotation * Rotation;
    //     }
    // }
    
    // INetworkMessage Implementation
    
    public MessageCode MessageCode => MessageCode.Rotation;
    public string NetworkMessage => $"{-Rotation.w},{Rotation.x},{Rotation.y},{-Rotation.z}";

    // IReadableMessage Implementation
    
    public string ReadableMessage => "Rotation: (" +
                                     $"{Rotation.x}, " +
                                     $"{Rotation.y}, " +
                                     $"{Rotation.z}, " +
                                     $"{Rotation.w})";
}   