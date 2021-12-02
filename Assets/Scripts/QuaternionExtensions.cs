using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion ConvertGyro(this Quaternion quaternion)
    {
        return new Quaternion(quaternion.x, quaternion.y, -quaternion.z, -quaternion.w);
    }
}