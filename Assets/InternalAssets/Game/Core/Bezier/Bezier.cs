using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public static class Bezier
{

    public static Vector2 GetPoint(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
    {
        float line = 1 - t;
        Vector2 p = Mathf.Pow(line, 3) * p1 + 3f * Mathf.Pow(line, 2) * t * p2 + 3f * line * Mathf.Pow(t, 2) * p3 + Mathf.Pow(t, 3) * p4;
        return p;
    }

    public static Quaternion GetRotationPoint(Vector2 point)
    {
        float z = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;

        return Quaternion.Euler(0, 0, z);

        //// Quaternion q0 = Quaternion.Euler(p0.x, p0.y, t);
        // Quaternion p01 = Quaternion.Euler(p0.x, p1, t);
        // Quaternion p12 = Quaternion.Lerp(p1, p2, t);
        // Quaternion p23 = Quaternion.Lerp(p2, p3, t);

        // Quaternion p012 = Quaternion.Lerp(p01, p12, t);
        // Quaternion p123 = Quaternion.Lerp(p12, p23, t);

        // Quaternion end = Quaternion.Lerp(p012, p123, t);
        // return end;
    }
}
