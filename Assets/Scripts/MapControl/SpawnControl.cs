using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnControl : MonoBehaviour
{
    public int objectCount;
    public SpriteRenderer map;
    public List<GameObject> objectPrefab;
    public float safeArea = 0.5f;

    private Transform objectRoot;
    private List<Transform> objects;
    private Random random;
    
    public void SpawnObject()
    {
        BindingObjectRoot(GameDefine.OBJECT_ROOT_NAME);

        if (objectCount <= 0)
        {
            Debug.LogWarning($"数量：{objectCount} >>>生成数量需要大于 0");
            return;
        }

        if (objectPrefab.Count < 1)
        {
            Debug.LogWarning($"物体预制数量：{objectPrefab.Count} >>>预制数量需要大于等于 1");
            return;
        }

        Vector3 _mapArea = GetSpawnArea();
        Vector3 mapArea = new Vector3(_mapArea.x-safeArea, _mapArea.y - safeArea, _mapArea.z);
        
        float startX = -mapArea.x;
        float startY = -mapArea.y;
        float endX = mapArea.x;
        float endY = mapArea.y;

        Vector3 spawnPos;
        random = new Random();

        int count = 0;
        for (int i = 0; i < objectCount; i++)
        {
            count++;
            spawnPos = new Vector3(random.Next((int)startX, (int)endX),
                                    random.Next((int)startY, (int)endY), 0);
            var objectPrefab = GetRandomObject();
            var objectInstance = Instantiate(objectPrefab, spawnPos, Quaternion.identity, objectRoot);
            objects.Add(objectInstance);
        }
        Debug.Log($"加载完成，共生成 {count} 个物体");
    }

    public void ClearObject()
    {
        BindingObjectRoot(GameDefine.OBJECT_ROOT_NAME);

        if (objectRoot.transform.childCount <=0)
        {
            return;
        }
        int count = 0;
        for (int i = objectRoot.transform.childCount - 1; i >= 0; i--)
        {
            count++;
            Transform deleteObj = objectRoot.transform.GetChild(i);
            objectPrefab.Remove(deleteObj.gameObject);
            DestroyImmediate(deleteObj.gameObject);
            
        }
        Debug.Log($"删除完成，共删除 {count} 个物体");
    }

    public void BindingObjectRoot(string rootName)
    {
        if (objectRoot != null)
        {
            return;
        }

        objectRoot = GameObject.Find(rootName).transform;
    }

    private Vector3 GetSpawnArea()
    {
        return new Vector3(map.bounds.size.x / 2, map.bounds.size.y / 2, 0);
    }

    private Transform GetRandomObject()
    {
        if (objectPrefab.Count == 1)
        {
            return objectPrefab[0].transform;
        }
        
        random = new Random();
        return objectPrefab[random.Next(0, objectPrefab.Count)].transform;
    }
}