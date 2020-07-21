using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolManager", menuName = "Managers/PoolManager")]
public class PoolManager : BaseInjectable, IGlobal
{
    private static Dictionary<Type, Pool> _pools = new Dictionary<Type, Pool>();
    private GameObject _poolsGO;

    public void AddPool(Type type, int poolSize = 0)
    {
        if (_poolsGO == null) _poolsGO = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");

        if (!_pools.ContainsKey(type))
        {
            var poolGO = new GameObject("Pool: " + type.ToString().ToUpper());
            poolGO.transform.SetParent(_poolsGO.transform);
            var pool = poolGO.AddComponent<Pool>();
            pool.InitPool(poolGO.transform, poolSize);
            _pools.Add(type, pool);
        }
    }

    public Pool GetPool<T>() where T : MonoBehaviour, IPoolable
    {
        if (_pools.TryGetValue(typeof(T), out var pool))
        {
            return pool;
        }

        return null;
    }

    public T GetOrCreate<T>(Transform parent, Vector3 localPosition = default, Vector3 localRotation = default)
        where T : MonoBehaviour, IPoolable
    {
        AddPool(typeof(T));

        Pool pool = GetPool<T>();
        var poolCount = pool.GetCount();
        if (poolCount == 0) return pool.Spawn<T>(parent);
        return GetPool<T>().Activate<T>(localPosition, localRotation);
    }

    public T GetOrCreate<T>(GameObject prefab, Transform parent, Vector3 localPosition = default,
        Vector3 localRotation = default, int poolSize = 0) where T : MonoBehaviour, IPoolable
    {
        AddPool(typeof(T), poolSize);

        Pool pool = GetPool<T>();
        var poolCount = pool.GetCount();
        if (poolCount == 0) return pool.Spawn<T>(prefab, parent, localPosition, localRotation);
        return GetPool<T>().Activate<T>(localPosition, localRotation);
    }

    public void PreLoad<T>(int count) where T : MonoBehaviour, IPoolable
    {
        var prefab = Resources.Load<GameObject>($"Prefabs/SceneItems/{typeof(T)}/{typeof(T)}");
        var parent = GameObject.Find(typeof(T) + "s").transform;
        var poolables = new List<T>();

        for (int i = 0; i < count; i++)
        {
            poolables.Add(GetOrCreate<T>(prefab, parent));
        }

        foreach (var poolable in poolables)
        {
            GetPool<T>().Deactivate(poolable);
        }
    }

    public void Release<T>(T poolable) where T : MonoBehaviour, IPoolable
    {
        GetPool<T>().Deactivate(poolable);
    }

    public void ClearPools()
    {
        _pools.Clear();
    }
}