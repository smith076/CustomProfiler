// Custom editor script
using UnityEditor;
using UnityEngine;
using ProfilerDumper;

[CustomEditor(typeof(CustomProfiler))]
public class ProfilerScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CustomProfiler script = (CustomProfiler)target;
        script.Precision = (Precision)EditorGUILayout.EnumPopup("Precision", script.Precision);
        for (int i = 0; i < script.markers.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            script.markers[i].name = EditorGUILayout.TextField("Marker Name", script.markers[i].name/*, GUILayout.Height(20)*/); // Increased height
            //EditorGUILayout.Space(-5); // Reduced space
            script.markers[i].type = (MarkerType)EditorGUILayout.EnumPopup("Marker Type", script.markers[i].type);
            script.markers[i].precision = (Precision)EditorGUILayout.EnumPopup("Precision", script.markers[i].precision);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Marker"))
        {
            script.markers.Add(new MarkerData());
        }

        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}
