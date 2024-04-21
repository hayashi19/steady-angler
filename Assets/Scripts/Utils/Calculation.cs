using UnityEngine;

public class Calculation
{
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static bool IsNear(float value1, float value2, float threshold = 0.1f)
    {
        return Mathf.Abs(value1 - value2) <= threshold;
    }
}
