using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

public class SimpleScript : MonoBehaviour
{
    // Define a static readonly ProfilerMarker
    private static readonly ProfilerMarker _perfMarker = new ProfilerMarker("SimpleScript.Update");

    private float _value;

    void Update()
    {
        // Surround the Update method with a using section of the ProfilerMarker.Auto
        using (_perfMarker.Auto())
        {
            // Perform some simple operations
            _value += Time.deltaTime;
            float sinValue = Mathf.Sin(_value);
            float cosValue = Mathf.Cos(_value);
            //Debug.Log($"Sin: {sinValue}, Cos: {cosValue}");
        }
    }
}
