using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<string, List<GameObject>> _pools = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, int> _index = new Dictionary<string, int>();

    public PoolingManager()
    {

    }

    public void CreatePool(string name, GameObject prefab, int capacity, string parentName = null)
    {
        List<GameObject> list = new List<GameObject>();
        list.Capacity = capacity;
        GameObject parent = null;

        if (_pools.ContainsKey(name) ) return;
        if (!string.IsNullOrEmpty(parentName))
        {
            parent = GameObject.Find(parentName);
            if (parent == null)
            {
                parent = new GameObject();
                parent.name = parentName;
            }
        }
        for (int i = 0; i < capacity; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent.transform);
            obj.SetActive(false);
            list.Add(obj);
        }
        _pools.Add(name, list);
        _index.Add(name, 0);
    }
    public GameObject SpawnPool(string name, Transform transform)
    {
        for (int i = 0; i < _pools[name].Capacity; i++)
        {
            GameObject obj = _pools[name][_index[name]];
            _index[name] = (_index[name] + 1) % _pools[name].Capacity;
            if (!obj.activeSelf)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    public void DestroyPool(string name)
    {
        foreach (var obj in _pools[name])
            GameObject.Destroy(obj);
        _pools.Remove(name);
        _index.Remove(name);
    }
}
