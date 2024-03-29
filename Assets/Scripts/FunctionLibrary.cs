using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary {

    public delegate Vector3 Function (float u, float v, float t);

    public enum FunctionName {Wave, MultiWave, Ripple, Sphere, Torus}

    static Function[] functions = {Wave, MultiWave, Ripple, Sphere, Torus};

    public static Function GetFunction (FunctionName name) => functions[(int)name];

    public static int FunctionCount => functions.Length;

    public static FunctionName GetNextFunctionName (FunctionName name) => 
        (int)name < functions.Length - 1 ? name + 1 : 0;

    public static FunctionName GetRandomNewFunctionName (FunctionName name) {
        var choice = (FunctionName)Random.Range(1, functions.Length);
        return choice == name ? 0 : choice;
    }

    public static Vector3 Morph (float u, float v, float t, Function from, Function to, float progress) {
        return Vector3.LerpUnclamped(from(u,v,t), to(u,v,t), SmoothStep(0f, 1f, progress));
    }

    public static Vector3 Wave (float u, float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static Vector3 MultiWave(float u, float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = 0.5f * Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(PI * (v + 0.5f * t));
        p.y += 0.5f * Sin(PI * (u + v + 0.25f * t)) * (1f/2.5f);
        p.z = v;
        return p;
    }

    public static Vector3 Ripple(float u, float v, float t) {
        Vector3 p;
        float d = Sqrt(u * u + v * v);
        p.x = u;
        p.y = Sin(PI* (4f * d - t)) / (1f + 10f * d);
        p.z = v;
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t) {
        Vector3 p;
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 Torus(float u, float v, float t) {
        Vector3 p;
        float r1 = 1f;
        float r2 = 0.25f + 0.1f * Sin(PI * (6f * u + t));
        float s = r1 + r2 * Cos(PI * v);
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
