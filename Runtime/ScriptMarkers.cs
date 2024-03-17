using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMarkers : MonoBehaviour, IMarkerCategory
{
    string[] _ProfilerMarkers;
    public string[] ProfilerMarkers 
    {
        get
        {
            return _ProfilerMarkers;
        }
        set
        {
            _ProfilerMarkers = value;
        }
    }
    bool _IsEnabled;
    public bool IsEnabled 
    {
        get
        {
            return _IsEnabled;
        }
        set
        {
            _IsEnabled = value;
        }
    }    
}
