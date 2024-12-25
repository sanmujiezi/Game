using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapControl : MonoBehaviour
{
    public int mapRow;
    public int mapCol;
    public Vector3 startPos;
    [FormerlySerializedAs("mapPrefab")] public GameObject mapTilePrefab;
    private Transform mapRoot;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startPos,1f);
    }

    public void CreateMap()
    {
        BindMapRoot(GameDefine.MAP_ROOT_NAME);
        
        ClearMapTile();
        
        float tlieWidth = mapTilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float tlieHight = mapTilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        
        for (int row = 0; row < mapRow; row++)
        {
            for (int col = 0; col < mapCol; col++)
            {
                Vector3 pos = new Vector3(startPos.x + col * tlieWidth, startPos.y + row * -tlieHight, startPos.z);
                Instantiate(mapTilePrefab, pos, Quaternion.identity, mapRoot.transform);
            }    
        }
    }

    public void ClearMapTile()
    {
        BindMapRoot(GameDefine.MAP_ROOT_NAME);
        
        if (mapRoot.transform.childCount <= 0)
        {
            Debug.LogWarning("地图为空");
            return;
        }

        int clearChildCount = mapRoot.childCount;
        
        for (int i = clearChildCount-1; i >=0 ; i--)
        {
            clearChildCount++;
            DestroyImmediate(mapRoot.GetChild(i).gameObject);
        }
        Debug.Log($"成功清空{clearChildCount}个地图块");
    }

    public void BindMapRoot(string mapName)
    {
        if (mapRoot != null)
        {
            return;
        }
        
        Transform root = GameObject.Find(mapName).transform;
        if (root ==null)
        { 
            Debug.LogError($"没有找到地图根节点 {mapName}");
            return;
        }
        mapRoot = root;
    }

}
