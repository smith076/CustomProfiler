using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;

namespace ProfilerDumper
{
    [System.Serializable]
    public class MarkerData
    {
        public string name;
        public MarkerType type;
        public Precision precision;
    }
    public enum Precision
    {
        half,
        full,
    }
    public enum MarkerType
    {
        time,
        memory,
        count
    }
    public class CustomProfiler : MonoBehaviour
    {
        public Precision Precision;
        public string FileName = "file.csv";
        string timePrecision;
        string memoryPrecision;
        public bool ScriptMarkers;
        public List<MarkerData> markers = new List<MarkerData>();
        public List<string> CustomMarkers = new List<string>();
        List<string> keys;
        private Dictionary<string, ProfilerRecorder> recorders = new Dictionary<string, ProfilerRecorder>();
        static readonly ProfilerMarker marker = new ProfilerMarker("Profiler.Main");

        StringBuilder StringBuilder = new StringBuilder();
        private void Awake()
        {
            timePrecision = Precision == Precision.half ? "F3" : "F6";
            memoryPrecision = Precision == Precision.half ? "F3" : "F6";
        }
        void Start()
        {
            // Initialize the ProfilerRecorders for each marker
            for (int i = 0; i < CustomMarkers.Count; i++)
            {
                var recorder = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, CustomMarkers[i]);
                recorders.Add(CustomMarkers[i], recorder);
            }
            keys = new List<string>(recorders.Keys);

            // Append the marker names as the first row in the CSV
            StringBuilder.AppendLine(string.Join(",", keys));
        }
        List<string> values = new List<string>();
        void Update()
        {
            using (marker.Auto())
            {
                values.Clear();
                //List<string> values = new List<string>();
                for (int i = 0; i < keys.Count; i++)
                {
                    // Log the last recorded value along with the recorder name
                    // Add the last recorded value to the values list
                    // check which marker type is selected and if time is selected then divide the value by 1000000
                    // if memory is selected then convert bytes to MB
                    if (markers[i].type == MarkerType.time)
                        values.Add((recorders[keys[i]].LastValue / 1000000.0f).ToString(timePrecision));
                    else if (markers[i].type == MarkerType.memory)
                        values.Add((recorders[keys[i]].LastValue / 1024.0f / 1024.0f).ToString(memoryPrecision));
                    else if (markers[i].type == MarkerType.count)
                        values.Add((recorders[keys[i]].LastValue).ToString());
                }
                // Append the values to the StringBuilder in the same order as the keys
                StringBuilder.AppendLine(string.Join(",", values));
            }
        }
        /// <summary>
        /// Call this method to dump the contents of the StringBuilder to a CSV file
        /// </summary>
        public void DumpData()
        {
            // Write the contents of the StringBuilder to a CSV file
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + FileName, StringBuilder.ToString());
            // Clear the StringBuilder
            StringBuilder.Clear();
        }
        void OnDestroy()
        {
            // Dispose of the ProfilerRecorders when they're no longer needed
            var keys = new List<string>(recorders.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                recorders[keys[i]].Dispose();
            }
            DumpData();
        }
    }
}

