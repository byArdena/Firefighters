﻿using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IPushable
{
    private readonly Queue<SpawnableObject> _spawnQueue = new Queue<SpawnableObject>();
        
    [SerializeField] private SpawnableObject _spawnableObject;

    public T Pull<T>(Vector3 position) where T: class
    {
        if (_spawnQueue.Count == 0)
            PushOnInitialize(Instantiate(_spawnableObject, position, Quaternion.identity).Initialize(this));
            
        return _spawnQueue.Dequeue().Pull<T>(position);
    }
        
    public T Pull<T>(Vector3 position, Transform parent) where T: class
    {
        if (_spawnQueue.Count == 0)
            PushOnInitialize(Instantiate(_spawnableObject, position, Quaternion.identity, parent).Initialize(this));
            
        return _spawnQueue.Dequeue().Pull<T>(position);
    }

    public T Pull<T>(Transform parent) where T: class
    {
        if (_spawnQueue.Count == 0)
            PushOnInitialize(Instantiate(_spawnableObject, parent).Initialize(this));
            
        return _spawnQueue.Dequeue().Pull<T>();
    }

    public virtual void Push(SpawnableObject spawnableObject)
    {
        PushOnInitialize(spawnableObject);
    }

    private void PushOnInitialize(SpawnableObject spawnableObject)
    {
        spawnableObject.SetActive(false);
        _spawnQueue.Enqueue(spawnableObject);
    }
}