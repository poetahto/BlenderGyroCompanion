using UnityEngine;

// TODO: is this data really useful? maybe dead-reckoning is more what we want

public class LocationData : MonoBehaviour, INetworkMessage, IReadableMessage
{
    public static bool IsLocationAvailable => Input.location.status == LocationServiceStatus.Running;
    public static float Latitude => Input.location.lastData.latitude;
    public static float Longitude => Input.location.lastData.longitude;
    
    // INetworkMessage Implementation
    
    public MessageCode MessageCode => MessageCode.Location;
    public string NetworkMessage => IsLocationAvailable ? $"{Latitude},{Longitude}" : "";

    // IReadableMessage Implementation
    
    public string ReadableMessage =>
        IsLocationAvailable 
            ? $"Latitude: {Latitude}, Longitude: {Longitude}" 
            : "Location: Offline";
}