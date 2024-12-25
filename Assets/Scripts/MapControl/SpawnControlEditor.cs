using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnControl))]
public class SpawnControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnControl spawnControl = (SpawnControl)target;
        if (GUILayout.Button("生成营养"))
        {
            spawnControl.SpawnObject();
        }

        if (GUILayout.Button("清除营养"))
        {
            spawnControl.ClearObject();
        }
        
    }
}