using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapControl))]
public class MapControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapControl mapControl = (MapControl)target;
        GUILayout.BeginVertical();
        
        if (GUILayout.Button("绑定地图根节点"))
        {
            mapControl.BindMapRoot(GameDefine.MAP_ROOT_NAME);
        }
        
        if (GUILayout.Button("创建地图"))
        {
            mapControl.CreateMap();
        }

        if (GUILayout.Button("清空地图"))
        {
            mapControl.ClearMapTile();
        }
        
        GUILayout.EndVertical();
    }
}
