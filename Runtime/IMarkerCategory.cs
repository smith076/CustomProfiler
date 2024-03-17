using System.Collections;
using System.Collections.Generic;


public interface IMarkerCategory 
{
    string[] ProfilerMarkers { get; set; }
    bool IsEnabled { get; set; }
}
